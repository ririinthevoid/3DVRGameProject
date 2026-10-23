using UnityEngine;
using TMPro;
using Deforestation.Inventory;
using UnityEngine.UI;
using UnityEngine.Audio;
namespace Deforestation.UI
{

	public class UIGameController : MonoBehaviour
	{
        #region Properties
		public bool IsPaused;
		public bool IsGameOver;
        #endregion

        #region Fields
        [Header("Controllers")]
        [System.Obsolete]
		private PlayerInventory _playerInventory => GameController.Instance.PlayerInventory;
		
		[Header("Pause")]
		[SerializeField] private GameObject _pauseUI;
		[SerializeField] private GameObject _activeUI;
		[SerializeField] private Button _startButton;
		[SerializeField] private Button _audioSettingButton;
		[SerializeField] private Button _gameExitButton;

        [Header("ResourceUI")]
        [SerializeField] private TextMeshProUGUI _ironText;
		[SerializeField] private TextMeshProUGUI _tinText;
		[SerializeField] private TextMeshProUGUI _copperText;

		[Header("HealthUI")]
		[SerializeField] private Slider _playerHealth;
		[SerializeField] private Slider _machineHealth;

        [Header("InteractionUI")]
        [SerializeField] private UIInteractionPannel _interactionPanel;

		[Header("AudioUI")]
		[SerializeField] private GameObject _audioUIPanel;
		private bool _audioUIOpen;
		[SerializeField] private Button _closeAudioUIButton;
		[SerializeField] private Slider _masterVolume;
		[SerializeField] private Slider _ambientVolume;
		[SerializeField] private Slider _sfxVolume;
		[SerializeField] private AudioMixer _audioMixer;

		[Header("AmmoUI")]
		[SerializeField] private TextMeshProUGUI _ammoDisplay;
		[SerializeField] private int _currentAmmo;
		[SerializeField] private int _maxAmmo;

		[Header("GameOver")]
		[SerializeField] private GameObject _gameOverUI;
		[SerializeField] private Button _quitGame;
        #endregion

        #region Unity Callbacks
        [System.Obsolete]
        void Start()
		{
            _playerInventory.OnInventoryUpdated += UpdateInventoryUI;
            IsPaused = true;

			_masterVolume.onValueChanged.AddListener(MasterVolumeChanged);
            _ambientVolume.onValueChanged.AddListener(AmbientVolumeChanged);
            _sfxVolume.onValueChanged.AddListener(SfxVolumeChanged);
        }

		[System.Obsolete]
		void Update()
        {
            _pauseUI.SetActive(IsPaused);
			_activeUI.SetActive(!IsPaused);

			if (Input.GetKeyUp(KeyCode.P))
			{
				IsPaused = !IsPaused;
			}

			if(IsPaused || GameController.Instance.Machine)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}

			if(IsGameOver || GameController.Instance.Machine)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}

			if(!IsPaused && !GameController.Instance.Machine)
			{
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
		}
        #endregion

        #region Public Methods
        #region Interaction
        public void ShowInteraction(string message)
        {
			_interactionPanel.Show(message);
        }

		public void HideInteraction()
		{
            _interactionPanel.Hide();
        }

        #endregion

        #region Health
		public void UpdatePlayerHealth(float value)
		{
            _playerHealth.value = value;
        }

		public void UpdateMachineHealth(float value)
		{
            _machineHealth.value = value;
        }

        #endregion

        #region GameState
        public void StartGame()
        {
            IsPaused = false;
        }

		public void ExitGame()
		{
			Application.Quit();
		}

		public void GameOver()
		{
			IsPaused = false;
            IsGameOver = true;
            _gameOverUI.SetActive(true);
		}
        #endregion

        #region Ammo UI
		public void AmmoReloaded(int ammo)
		{
			_currentAmmo = ammo;
            _ammoDisplay.text = "Ammo: " + _currentAmmo + "/" + _maxAmmo;
		}

		public void MaxAmmoUpgraded(int ammount)
		{
			_maxAmmo += ammount;
			_ammoDisplay.text = "Ammo: " + _currentAmmo + "/" + _maxAmmo;
		}
        #endregion
        #endregion

        #region Private Methods
        [System.Obsolete]
        private void UpdateInventoryUI()
		{
			if (_playerInventory.InventoryCount.ContainsKey(RecolectableType.Iron))
				_ironText.text = _playerInventory.InventoryCount[RecolectableType.Iron].ToString();
			else
				_ironText.text = "0";

			if (_playerInventory.InventoryCount.ContainsKey(RecolectableType.Tin))
				_tinText.text = _playerInventory.InventoryCount[RecolectableType.Tin].ToString();
			else 
				_tinText.text = "0";
			
			if (_playerInventory.InventoryCount.ContainsKey(RecolectableType.Copper))
				_copperText.text = _playerInventory.InventoryCount[RecolectableType.Copper].ToString();
			else
				_copperText.text = "0";
		}

        #region Volumes
        private void MasterVolumeChanged(float value)
		{
			_audioMixer.SetFloat("MasterVol", Mathf.Log10(value) * 20);
        }

		private void AmbientVolumeChanged(float value)
		{
			_audioMixer.SetFloat("AmbientVol", Mathf.Log10(value) * 20);
        }

        private void SfxVolumeChanged(float value)
		{
			_audioMixer.SetFloat("SFXVol", Mathf.Log10(value) * 20);
        }
        #endregion
        #endregion
    }
}