using Deforestation;
using System.Collections.Generic;
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

		[SerializeField] private GameObject _treePrefab;
		#endregion

		#region Unity Callbacks
		void Start()
		{
			Explode();
			Destroy(gameObject, 2.5f);
		}

		void Update()
		{

		}
		#endregion

		#region Public Methods
		public void Explode()
		{
            Terrain terrain = Terrain.activeTerrain;
            TreeInstance[] trees = terrain.terrainData.treeInstances;

            for (int i = 0; i < trees.Length; ++i)
            {
                TreeInstance tree = trees[i];
                Vector3 treeWorldPoss = TreeToWorldPosition(tree, terrain);

                if (Vector3.Distance(transform.position, treeWorldPoss) <= _blastRadius)
                {
                    Instantiate(_treePrefab, treeWorldPoss, Quaternion.identity);
                    RemoveTreeFromTerrain(i, terrain);
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
		private Vector3 TreeToWorldPosition(TreeInstance tree, Terrain terrain)
		{
			return Vector3.Scale(tree.position, terrain.terrainData.size + terrain.transform.position);
		}

		private void RemoveTreeFromTerrain(int index,Terrain terrain)
		{
			List<TreeInstance> trees = new List<TreeInstance>(terrain.terrainData.treeInstances);
			trees.RemoveAt(index);
			terrain.terrainData.treeInstances = trees.ToArray();
		}
        #endregion
    }
}