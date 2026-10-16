using UnityEngine;
namespace Deforestation.Machine.Weapon
{

	public class Explosion : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		[SerializeField] private float _blastRadius;
		[SerializeField] private float _explosionForce;
		[SerializeField] private float _maxDamage;
		[SerializeField] private float _torqueForce;
		#endregion

		#region Unity Callbacks
		void Start()
		{
			Explode();
		}

		void Update()
		{

		}
		#endregion

		#region Public Methods
		[System.Obsolete]
		public void Explode()
		{
			TreeInstance[] _trees = GameController.Instance.TerrainManager.Trees;
            GameObject _treePrefab = GameController.Instance.TerrainManager.TreePrefab;

            for (int i = 0; i < _trees.Length; ++i)
            {
                TreeInstance tree = _trees[i];
                Vector3 treeWorldPoss = GameController.Instance.TerrainManager.TreeToWorldPosition(tree);

                if (Vector3.Distance(transform.position, treeWorldPoss) <= _blastRadius)
                {
					GameController.Instance.TerrainManager.DestroyTree(i, treeWorldPoss);
                }
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, _blastRadius);

			foreach (Collider hit in colliders)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();
				if (rb != null)
				{
					rb.AddExplosionForce(_explosionForce, transform.position, _blastRadius);
					Vector3 torque = Random.insideUnitSphere * _torqueForce;
					rb.AddTorque(torque);
				}

				HealthSystem health = hit.GetComponentInParent<HealthSystem>();
				if (health != null)
				{
					float distance = Vector3.Distance(hit.transform.position, transform.position);
					float damage = Mathf.Lerp(_maxDamage, 0, distance / _blastRadius);
					health.OnDamaged(damage);
				}
			}
		}
		#endregion

		#region Private Methods
		
        #endregion
    }
}