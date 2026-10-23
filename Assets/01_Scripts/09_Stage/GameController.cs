using Deforestation.Interaction;
using Deforestation.Inventory;
using Deforestation.Machine;
using Deforestation.Machine.Weapon;
using Deforestation.UI;
using StarterAssets;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
namespace Deforestation
{

    public class GameController : Singleton<GameController>
    {
        #region Properties
        public GameObject MachineCamera => _machineCamera;
        public MachineController MachineController => _machineController;
        public WeaponController WeaponController => _weaponController;
        public PlayerInventory PlayerInventory => _playerInventory;
        public InteractionSystem InteractionSystem => _interactionSystem;
        public TerrainManager TerrainManager => _terrainManager;
        public UIGameController UIGameController => _uiGameController;

        public bool Machine;
        #endregion

        #region Fields
        [Header("Player")]
        [SerializeField] private CharacterController _playerCharacterController;
        [SerializeField] private HealthSystem _playerHealth;
        [SerializeField] private GameObject _playerCamera;
        [SerializeField] private CinemachineInputAxisController _cameraPanAxis;
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private InteractionSystem _interactionSystem;
        [SerializeField] private ThirdPersonController _thirdPersonController;

        [Header("Machine")]
        [SerializeField] private MachineController _machineController;
        [SerializeField] private HealthSystem _machineHealth;
        [SerializeField] private GameObject _machineCamera;
        [SerializeField] private WeaponController _weaponController;
        [SerializeField] private Transform _exitPosition;
        [SerializeField] private UpgradeManager _upgradeManager;
        [SerializeField] private MachineMovement _machineMovement;

        [Header("Terrain")]
        [SerializeField] private TerrainManager _terrainManager;
        [SerializeField] private Water _water;

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

            _weaponController.OnAmmoReloaded += _uiGameController.AmmoReloaded;

            _upgradeManager.OnMaxAmmoUpgraded += _weaponController.MaxAmmoUpgraded;
            _upgradeManager.OnMaxAmmoUpgraded += _uiGameController.MaxAmmoUpgraded;
            _upgradeManager.OnHealthRegenUpgraded += _machineController.HealthRegenUpdated;

            _upgradeManager.OnReloadSpeedUpgraded += _weaponController.ReloadSpeedUpgraded;

            _weaponController.OnAmmoReloaded += _uiGameController.AmmoReloaded;

            _water.PlayerDrown += PlayerDrowned;

            _machineHealth.OnDeath += _uiGameController.GameOver;
            _playerHealth.OnDeath += _uiGameController.GameOver;
        }

        void Update()
        {
            //Movement Controller Activations
            if(_uiGameController.IsPaused || _uiGameController.IsGameOver)
            {
                _cameraPanAxis.enabled = false;
                _thirdPersonController.enabled = false;
            }
            if(!_uiGameController.IsPaused && !_uiGameController.IsGameOver)
            {
                _cameraPanAxis.enabled = true;
                _thirdPersonController.enabled = true;
            }
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
            if(!machine)
            {
                _playerCharacterController.gameObject.transform.position = _exitPosition.position;
                StartCoroutine(MachineWindDown());
            }
            else if (machine)
            {
                Machine = true;
                MachineCamera.gameObject.SetActive(true);

                _playerCharacterController.gameObject.SetActive(false);
                _playerCamera.gameObject.SetActive(false);

                _machineController.StartDriving(true);
                _uiGameController.HideInteraction();
            }
        }
        #endregion

        #region Private Methods
        private IEnumerator MachineWindDown()
        {
            yield return new WaitForSeconds(6.5f);

            Machine = false;
            MachineCamera.gameObject.SetActive(false);

            _playerCharacterController.gameObject.SetActive(true);
            _playerCamera.gameObject.SetActive(true);

            _machineController.StartDriving(false);
            _uiGameController.HideInteraction();
        }

        private void PlayerDrowned()
        {
            _playerHealth.OnDamaged(250);
        }
        #endregion
    }
}
