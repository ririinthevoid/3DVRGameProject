using System.Collections;
using UnityEngine;
using UnityEngine.AI;
namespace Deforestation.Dinosaurs
{

	public class DinosaurBase : MonoBehaviour
	{
        #region Properties
        #endregion

        #region Field
        protected HealthSystem _healthSystem;
        protected Animator _animator;
        protected NavMeshAgent _agent;
		#endregion

		#region Unity Callbacks
		void Awake()
		{
			_healthSystem = GetComponent<HealthSystem>();
			_animator = GetComponent<Animator>();
			_agent = GetComponent<NavMeshAgent>();

			_healthSystem.OnDeath += Die;
		}

		void Update()
		{

		}
		#endregion

		#region Public Methods
		private void Die()
		{
			Destroy(_healthSystem);
			_animator.SetTrigger("death");
            Destroy(_agent);
			Destroy(this);
		}
        #endregion

        #region Private Methods
        #endregion
    }
}