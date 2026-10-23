using System;
using System.Collections;
using UnityEngine;
namespace Deforestation.Machine.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        #region Properties
        public event Action OnWeaponShot;
        public event Action<int> OnAmmoReloaded;
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
        [SerializeField] private float _reloadSpeed;
        private bool _reloading;
        #endregion

        #region Unity Callbacks
        void Start()
        {
            _shootSide = 0;
        }

        [System.Obsolete]
        void Update()
        {
            if (GameController.Instance.MachineController.IsDriving && !GameController.Instance.UIGameController.IsPaused && !GameController.Instance.UIGameController.IsGameOver)
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

                if (Input.GetMouseButtonUp(0) && _currentAmmo > 0)
                {
                    _currentAmmo--;
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

            if(_currentAmmo < _maxAmmo & !_reloading)
            {
                StartCoroutine(AmmoReloading(_reloadSpeed));
            }
        }
        #endregion

        #region Public Methods
        public void ReloadSpeedUpgraded(float reloadSpeed)
        {
            if (!_reloading)
            {
                _reloadSpeed = reloadSpeed;
            }
        }
        public void MaxAmmoUpgraded(int maxAmmo)
        {
            _maxAmmo = maxAmmo;
        }
        #endregion

        #region Private Methods
        private IEnumerator AmmoReloading(float speed)
        {
            _reloading = true;
            yield return new WaitForSeconds(speed);
            _currentAmmo += 1;
            OnAmmoReloaded?.Invoke(_currentAmmo);
            _reloading = false;
        }
        #endregion
    }
}