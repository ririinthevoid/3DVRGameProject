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
        private void Start()
        {
            Destroy(gameObject, 5f);
        }
        private void OnTriggerEnter(Collider other)
        {
            HealthSystem health = other.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.OnDamaged(_damage);
            }
            var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion.gameObject, 3);
            Destroy(gameObject, 5);
            GetComponent<Collider>().enabled = false;
        }

        private void Update()
        {
            transform.position += transform.forward * _flySpeed * Time.deltaTime;
        }
        #endregion
        }
}