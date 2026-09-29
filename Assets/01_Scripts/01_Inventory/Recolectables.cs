using Deforestation.Interaction;
using UnityEngine;

namespace Deforestation.Inventory
{
    public enum RecolectableType
    {
        Iron,
        Tin,
        Copper,
    }

    public class Recolectables : MonoBehaviour, IInteractable
    {
        #region Properties
        [field: SerializeField] public int MineralValue { get; private set; }
        [field:SerializeField] public RecolectableType MineralType { get; private set; }
        #endregion

        #region Fields
        [SerializeField] private InteractableInfo _interactableInfo;
        #endregion

        #region Unity Callbacks
        void Start()
        {
    
        }
    
        void Update()
        {
    
        }
        #endregion
    
        #region Public Methods
        public InteractableInfo GetInfo()
        {
            _interactableInfo.Type = MineralType.ToString();
            return _interactableInfo;
        }

        public void Interact()
        {
            Destroy(gameObject);
        }
        #endregion
    
        #region Private Methods
        #endregion
    }
}
