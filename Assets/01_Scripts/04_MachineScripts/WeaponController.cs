using System;
using UnityEngine;
namespace Deforestation.Machine.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        #region Properties
        public event Action OnWeaponShot;
        #endregion

        #region Fields
        [Header("Turret")]
        [SerializeField] private Transform _turretTransform;
    	[SerializeField] private Transform _spawnPoinLeft;
    	[SerializeField] private Transform _spawnPoinRight;
    	[SerializeField] private float  _rotationSpd;

        [Header("Shot")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private GameObject _smokeRight;
        [SerializeField] private GameObject _smokeLeft;
        [SerializeField] private int  _shootSide; //0 = left : 1 = right

        [Header("Ammo Managing")]
        [SerializeField] private int _maxAmmo;
        [SerializeField] private int _currentAmmo;
        [SerializeField] private int _reloadSpeed;
        #endregion

        #region Unity Callbacks
        void Start()
        {
            _shootSide = 0;
        }

        [System.Obsolete]
        void Update()
        {
            if (GameController.Instance.MachineController.IsDriving)
            {
                Ray ray = GameController.Instance.MachineCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 direction = hit.point - transform.position;
                    direction.y = 0f;

                    Quaternion rotation = Quaternion.LookRotation(direction);
                    _turretTransform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpd * Time.deltaTime);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    OnWeaponShot?.Invoke();
                    transform.LookAt(hit.point);
                    if (_shootSide == 0)
                    {
                        _smokeLeft.SetActive(true);
                        _smokeRight.SetActive(false);
                        _shootSide = 1;
                        Instantiate(_bulletPrefab,_spawnPoinLeft.position,_spawnPoinLeft.rotation);
                    }
                    else if (_shootSide == 1)
                    {
                        _smokeLeft.SetActive(false);
                        _smokeRight.SetActive(true);
                        _shootSide = 0;
                        Instantiate(_bulletPrefab,_spawnPoinRight.position,_spawnPoinRight.rotation);
                    }
                }
            }
        }
    	#endregion
    }
}