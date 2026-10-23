using System;
using System.Collections;
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
        public int RegenAmount;
        public bool Cooldown;
        #endregion

        #region Fields
        private HealthSystem _healthSystem;
        private MachineMovement _machineMovement;
        private Animator _anim;

        [SerializeField] private bool _isDriving;
        private bool _machineRegen;
        private bool _machineDOT;

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
            if (Input.GetKeyUp(KeyCode.Escape) && _isDriving)
            {
                StopDriving();
                _isDriving = false;
            }

            if(!_machineRegen)
            StartCoroutine(HealthRegen(RegenAmount));
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.CompareTag("Water"))
            {
                if(!_machineDOT)
                {
                    StartCoroutine(DOT());
                }
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
            _isDriving = false;
            _machineMovement.enabled = false;
            _anim.SetBool("Move", false);
            OnMachineDriveChange?.Invoke(false);
            StartCoroutine(DriveCooldown());
        }

        public void HealthRegenUpdated(int amount)
        {
            RegenAmount = amount;
        }
        #endregion

        #region Private Methods
        private IEnumerator HealthRegen (int amount)
        {
            _machineRegen = true;
            _healthSystem.OnHealed(amount);
            yield return new WaitForSeconds(5);
            _machineRegen = false;
        }

        private IEnumerator DOT()
        {
            _machineDOT = true;
            _healthSystem.OnDamaged(25);
            yield return new WaitForSeconds(1);
            _machineDOT = false;
        }

        private IEnumerator DriveCooldown()
        {
            Cooldown = true;
            yield return new WaitForSeconds(3);
            Cooldown = false;
        }
        #endregion
    }
}