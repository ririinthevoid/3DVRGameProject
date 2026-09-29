using UnityEngine;
namespace Deforestation
{

	public class TerrainManager : MonoBehaviour
	{
		#region Fields
		private TreeInstance[] _trees;
		#endregion

		#region Unity Callbacks
		void Start()
		{
			Terrain terrain = Terrain.activeTerrain;
			_trees = terrain.terrainData.treeInstances;
		}

		void OnDestroy()
		{
			Terrain terrain = Terrain.activeTerrain;
			terrain.terrainData.treeInstances = _trees;
		}
		#endregion
	}
}