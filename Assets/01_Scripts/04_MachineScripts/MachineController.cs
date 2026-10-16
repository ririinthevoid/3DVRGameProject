using System;
using UnityEngine;
namespace Deforestation.Machine
{
    [RequireComponent(typeof(HealthSystem))]

    public class MachineController : MonoBehaviour
    {
        #region Properties
        public bool IsDriving => _isDriving;
        public HealthSystem HealthSystem => _healthSystem;
        public MachineMovement MachineMovement => _machineMovement;
        public Action<bool> OnMachineDriveChange;

        #endregion

        #region Fields
        private HealthSystem _healthSystem;
        private MachineMovement _machineMovement;
        private Animator _anim;

        [SerializeField] private bool _isDriving;
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            _healthSystem = GetComponent<HealthSystem>();
            _machineMovement = GetComponent<MachineMovement>();
            _anim = GetComponent<Animator>();
        }
        void Start()
        {
            _machineMovement.enabled = false;
        }

		[System.Obsolete]
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape) && IsDriving)
            {
                StopDriving();
            _isDriving = false;
            }
        }
        #endregion

        #region Public Methods
        public void StartDriving(bool machine)
        {
            _isDriving = true;
            enabled = machine;
            _machineMovement.enabled = machine;
            _anim.SetTrigger("WakeUp");
            _anim.SetBool("Move", machine);
            OnMachineDriveChange?.Invoke(true);
        }

        [System.Obsolete]
        public void StopDriving()
        {
            GameController.Instance.MachineMode(false);
            _machineMovement.enabled = false;
            _anim.SetBool("Move", false);
            OnMachineDriveChange?.Invoke(false);

            #endregion
        }
    }
}