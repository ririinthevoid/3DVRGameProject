using UnityEngine;
namespace Deforestation.Machine
{
	public class MachineMovement : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		[SerializeField] private float _speedForce = 75000;
		[SerializeField] private float _speedRotation = 25;
		private Rigidbody _rb;
		private Vector3 _movementDirection;
		[SerializeField] private Camera _machineCamera;
		#endregion

		#region Unity Callbacks
		void Awake()
		{
			_rb = GetComponent<Rigidbody>();
		}

        [System.Obsolete]
		void Update()
        {
			if(!GameController.Instance.UIGameController.IsPaused)
			{
                _movementDirection = new Vector3(Input.GetAxis("Vertical") * 2, 0, 0);
                transform.Rotate(Vector3.up * _speedRotation * Time.deltaTime * Input.GetAxis("Horizontal"));
            }
		}

		void FixedUpdate()
		{
			_rb.AddRelativeForce(_movementDirection.normalized * _speedForce, ForceMode.Impulse);
		}

        [System.Obsolete]
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Tree")
			{
				int index = other.GetComponent<TreeIndex>().Index;
				GameController.Instance.TerrainManager.DestroyTree(index, other.transform.position);
			}
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}