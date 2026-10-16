using System.Collections.Generic;
using UnityEngine;
namespace Deforestation
{

	public class TerrainManager : MonoBehaviour
	{
        #region Properties
        public TreeInstance[] Trees => _trees;
        public GameObject TreePrefab => _treePrefab;
        #endregion

        #region Fields
        private TreeInstance[] _trees;
        private Terrain _terrain;
        [SerializeField] private GameObject _treePrefab;
        [SerializeField] private GameObject _treeDetectionPrefab;
        [SerializeField] private GameObject _treeGroup;
        #endregion

        #region Unity Callbacks
		[System.Obsolete]
        void Start()
        {
            _terrain = Terrain.activeTerrain;
			_trees = _terrain.terrainData.treeInstances;

			InitializeTrees();
		}

		void OnDestroy()
		{
			Terrain _terrain = Terrain.activeTerrain;
            _terrain.terrainData.treeInstances = _trees;
		}
        #endregion

        #region Public Methods
        public Vector3 TreeToWorldPosition(TreeInstance tree)
        {
            return Vector3.Scale(tree.position, _terrain.terrainData.size + _terrain.transform.position);
        }

        public void RemoveTreeFromTerrain(int index)
        {
            List<TreeInstance> trees = new List<TreeInstance>(_terrain.terrainData.treeInstances);
            trees.RemoveAt(index);
            _terrain.terrainData.treeInstances = trees.ToArray();
        }

        [System.Obsolete]
        public void DestroyTree(int i, Vector3 treeWorldPoss)
        {
            Instantiate(_treePrefab, treeWorldPoss, Quaternion.identity);
            RemoveTreeFromTerrain(i);
        }
        #endregion

        #region Private Methods
        [System.Obsolete]
        private void InitializeTrees()
        {
            for (int i = 0; i < _trees.Length; i++)
            {
                TreeInstance tree = _trees[i];
                Vector3 treeWorldPos = TreeToWorldPosition(tree);

                // Spawn the detector, NOT the actual tree.
                GameObject detectorObject = Instantiate(_treeDetectionPrefab,treeWorldPos,Quaternion.identity,_treeGroup.transform);

                // Assign the corresponding Terrain tree index.
                TreeIndex detector = detectorObject.GetComponent<TreeIndex>();

                if (detector != null)
                {
                    detector.Index = i;
                }
                else
                {
                    Debug.LogError("TreeDetection prefab is missing the TreeIndex component!"
                    );
                }
            }
        }
        #endregion
    }
}