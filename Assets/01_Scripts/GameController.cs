using Deforestation.Interaction;
using Deforestation.Inventory;
using Deforestation.Machine;
using Deforestation.UI;
using UnityEngine;
namespace Deforestation
{

	public class GameController : Singleton<GameController>
	{
        #region Properties
        public GameObject MachineCamera => _machineCamera;
        public MachineController MachineController => _machineController;
        public PlayerInventory PlayerInventory => _playerInventory;
        public InteractionSystem InteractionSystem => _interactionSystem;
        #endregion

        #region Fields
        [Header("Player")]
        
        [SerializeField] private CharacterController _playerCharacterController;
        [SerializeField] private HealthSystem _playerHealth;
        [SerializeField] private GameObject _playerCamera;
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private InteractionSystem _interactionSystem;

        [Header("Machine")]
        [SerializeField] private MachineController _machineController;
        [SerializeField] private HealthSystem _machineHealth;
        [SerializeField] private GameObject _machineCamera;

        [Header("UI")]
        [SerializeField] private UIGameController _uiGameController;
        #endregion

        #region Unity Callbacks
        private void Awake()
		{
            _machineHealth = _machineController.HealthSystem;
		}

        private void Start()
        {
            _playerHealth.OnHealthChanged += _uiGameController.UpdatePlayerHealth;
            _machineHealth.OnHealthChanged += _uiGameController.UpdateMachineHealth;
        }

        void Update()
		{

		}
		#endregion

		#region Public Methods
		public void PlayerTeleport(Vector3 target)
		{
			_playerCharacterController.enabled = false;
			_playerCharacterController.transform.position = target;
			_playerCharacterController.enabled = true;
		}

		public void MachineMode(bool machine)
		{
            MachineCamera.gameObject.SetActive(machine);

            _playerCharacterController.gameObject.SetActive(!machine);
			_playerCamera.gameObject.SetActive(!machine);

            _machineController.StartDriving(machine);

            if (machine)
            {
                _uiGameController.HideInteraction();
                Cursor.lockState = CursorLockMode.None;
            }
            if (!machine)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            Cursor.visible = machine;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
