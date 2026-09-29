using UnityEngine;
namespace Deforestation.Machine.Weapon
{
    public class Bullet : MonoBehaviour
    {
        #region Properties
        #endregion

        #region Fields
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private float _flySpeed = 80;
        [SerializeField] private float _damage = 10;
        #endregion

        #region Unity Callbacks
        private void OnTriggerEnter(Collider other)
        {
            HealthSystem health = GetComponent<HealthSystem>();
            if (health != null)
            {
                health.OnDamaged(_damage);
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 1);
            GetComponent<Collider>().enabled = false;
        }

        private void Update()
        {
            transform.Translate(transform.forward * -1 * _flySpeed * Time.deltaTime, Space.Self);
        }
        #endregion
    }
}