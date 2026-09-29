using UnityEngine;
namespace Deforestation.Machine
{
    [RequireComponent(typeof(HealthSystem))]

    public class MachineController : MonoBehaviour
	{
		#region Properties
		public bool IsDriving {get; private set;}
		public HealthSystem HealthSystem => _healthSystem;
		#endregion

		#region Fields
		private HealthSystem _healthSystem;
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            _healthSystem = GetComponent<HealthSystem>();
        }
        void Start()
		{
			IsDriving = false;

        }

        [System.Obsolete]
        void Update()
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				IsDriving = false;
				GameController.Instance.MachineMode(false);
			}
		}
		#endregion

		#region Public Methods
		public void StartDriving(bool machine)
		{
            IsDriving = true;
            enabled = machine;
        }
		#endregion

		#region Private Methods
		#endregion
	}
}