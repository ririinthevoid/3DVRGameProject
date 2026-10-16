using UnityEngine;
using TMPro;
using Deforestation.Inventory;
using UnityEngine.UI;
using Unity.VisualScripting;
namespace Deforestation.UI
{

	public class UIGameController : MonoBehaviour
	{
        #region Properties
		public bool IsPaused;
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
        #endregion

        #region Unity Callbacks
        [System.Obsolete]
        void Start()
		{
            _playerInventory.OnInventoryUpdated += UpdateInventoryUI;
            IsPaused = true;
        }

		[System.Obsolete]
		void Update()
        {
            _pauseUI.SetActive(IsPaused);
			_activeUI.SetActive(!IsPaused);

			if (!IsPaused && (Input.GetKeyUp(KeyCode.Escape) && !GameController.Instance.MachineController.IsDriving) || Input.GetKeyUp(KeyCode.P))
			{
				IsPaused = !IsPaused;
			}

			if(IsPaused || GameController.Instance.Machine)
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

        #endregion
    }
}