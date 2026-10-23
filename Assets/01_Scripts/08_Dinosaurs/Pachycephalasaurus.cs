using UnityEngine;
using UnityEngine.AI;
namespace Deforestation.Dinosaurs
{

	public class Pachycephalasaurus : DinosaurBase
	{
		#region Properties
		#endregion

		#region Fields
		[SerializeField] private float _detectionRadius = 50;
		[SerializeField] private float _attackRadius = 5;
		[System.Obsolete] 	private Vector3 _machinePosition => GameController.Instance.MachineController.transform.position;

		private bool _chase;
		private bool _attack;
        [SerializeField] private float _attackTimer = 2f;
        [SerializeField] private float _attackDamage = 5f;
		private float _attackCooldown;
		#endregion

        #region Unity Callbacks
        void Start()
		{
            _attackCooldown = _attackTimer;
		}

        [System.Obsolete]
        void Update()
		{
			if(!GameController.Instance.UIGameController.IsPaused && !GameController.Instance.UIGameController.IsGameOver)
			{
                if (Vector3.Distance(transform.position, _machinePosition) < _detectionRadius && !_chase && !_attack)
                {
                    ChaseMachine();
                }

                if (Vector3.Distance(transform.position, _machinePosition) > _detectionRadius && _chase || _attack)
                {
                    Idle();
                }

                if (Vector3.Distance(transform.position, _machinePosition) < _attackRadius && _chase)
                {
                    AttackMachine();
                }

                if (Vector3.Distance(transform.position, _machinePosition) > _attackRadius && _attack)
                {
                    ChaseMachine();
                }

                if (_attack)
                {
                    _attackCooldown -= Time.deltaTime;
                    if (_attackCooldown <= 0)
                    {
                        _attackCooldown = _attackTimer;
                        GameController.Instance.MachineController.HealthSystem.OnDamaged(_attackDamage);
                    }
                }

                if (_chase)
                {
                    _agent.SetDestination(_machinePosition);
                }
            }
        }
        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(gameObject.transform.position, _detectionRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, _attackRadius);
        }

        #region Public Methods
        #endregion

        #region Private Methods
        [System.Obsolete]
		private void ChaseMachine()
        {
			_animator.SetBool("Run", true);
            _animator.SetBool("Attack", false);
			_agent.SetDestination(_machinePosition);
            _chase = true;
            _attack = false;

        }

		private void AttackMachine()
		{
			_animator.SetBool("Run", false);
            _animator.SetBool("Attack", true);
			_agent.isStopped = true;
            _chase = false;
            _attack = true;
        }

		private void Idle()
		{
            _animator.SetBool("Run", false);
            _animator.SetBool("Attack", false);
            _agent.isStopped = true;
            _chase = false;
            _attack = false;
        }
		#endregion
	}
}