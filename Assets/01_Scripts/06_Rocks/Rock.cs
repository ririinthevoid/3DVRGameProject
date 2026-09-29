using UnityEngine;
namespace Deforestation.Enviroment
{

	[RequireComponent(typeof(HealthSystem))]
	public class Rock : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		[Header("Prefabs")]
        [SerializeField] private GameObject [] _pebblePrefabs;

		[Header("Config")]
		private HealthSystem _health;
        [SerializeField] private int _minSpawn = 0;
        [SerializeField] private int _maxSpawn = 3;
        [SerializeField] private float _radiusSpawn;
        #endregion

        #region Unity Callbacks
        void Awake()
		{
			_health = GetComponent<HealthSystem>();
			_health.OnDeath += DestroyRock;
		}
        #endregion

        #region Private Methods
        private void DestroyRock()
		{
			SpawnMineral();
            Destroy(gameObject);
		}

		private void SpawnMineral()
		{
			int SpawnCount = Random.Range(_minSpawn, _maxSpawn);

			for (int i = 0; i < SpawnCount; i++)
			{
				Instantiate(_pebblePrefabs[Random.Range(0, _pebblePrefabs.Length +1)], RandomPosition(), Random.rotation);
			}
		}

		private Vector3 RandomPosition()
		{
			Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection.y = Mathf.Abs(randomDirection.y);
			return transform.position + randomDirection * Random.Range(.5f, _radiusSpawn);
        }
        #endregion
    }
}