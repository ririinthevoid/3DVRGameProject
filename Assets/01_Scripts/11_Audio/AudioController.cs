using UnityEngine;

namespace Deforestation.Audio
{

	public class AudioController : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		[SerializeField] private AudioSource _ambientSound;
		[SerializeField] private AudioSource _machineTurnOn;
		[SerializeField] private AudioSource _machineTurnOff;
		[SerializeField] private AudioSource _machineShoot;
		#endregion

		#region Unity Callbacks
		[System.Obsolete]
		void Start()
		{
			_ambientSound.Play();

			GameController.Instance.MachineController.OnMachineDriveChange += MachineState;
			GameController.Instance.WeaponController.OnWeaponShot += WeaponShot;
        }

		void Update()
		{

		}
        #endregion

        #region Public Methods

        public void WeaponShot()
		{
			_machineShoot.Play();
		}
        #endregion

        #region Private 
        private void MachineState(bool mode)
        {
            if (mode)
            {
                _machineTurnOn.Play();
            }
            if (!mode)
            {
                _machineTurnOff.Play();
            }
        }
        #endregion
    }
}