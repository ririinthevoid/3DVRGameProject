using System.Collections;
using UnityEngine;
using UnityEngine.AI;
namespace Deforestation.Dinosaurs
{

	public class Stegosaurus : DinosaurBase
	{
		#region Properties
		#endregion

		#region Fields
		[SerializeField] private float _radius = 100f;
		#endregion

		#region Unity Callbacks
		void Start()
		{
            _healthSystem.OnHealthChanged += Damaged;

        }

        [System.Obsolete]
		void Update()
		{
            if(!GameController.Instance.UIGameController.IsPaused)
            {
                if (_animator.GetBool("flee") == false)
                {
                    StartCoroutine(IdleLoop());
                    Debug.Log("Stego Fleeing");
                }

                if (_agent.pathPending)
                {
                    if (_agent.remainingDistance <= _agent.stoppingDistance)
                    {
                        if (_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                        {
                            _animator.SetBool("flee", false);
                        }
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Method
        private void RandomMovement()
        { 
            Vector3 ToGo = Random.insideUnitSphere * _radius;
            ToGo += transform.position;
            NavMeshHit hit;
            if(NavMesh.SamplePosition(ToGo, out hit, _radius, 1))
            {
                _agent.SetDestination(hit.position);
            }

            _animator.SetBool("flee", true);
        }

        private void Damaged(float health)
        {
            RandomMovement();
        }

        private IEnumerator IdleLoop()
        {
            var idle = Random.Range(0, 4);

            switch (idle)
            {
                case 0:
                    _animator.SetTrigger("idle.idle"); break;
                case 1:
                    _animator.SetTrigger("idle.drink"); break;
                case 2:
                    _animator.SetTrigger("idle.eat"); break;
                case 3:
                    _animator.SetTrigger("idle.roar"); break;
            }
            yield return new WaitForSeconds(5f);
        }

        #endregion
    }
}