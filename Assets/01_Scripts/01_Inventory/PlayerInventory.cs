using System;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        #region Properties
        public Dictionary<RecolectableType, int> InventoryCount = new Dictionary<RecolectableType, int>();
        public Action OnInventoryUpdated;
        #endregion

        #region Fields
        #endregion

        #region Unity Callbacks
        #endregion

        #region Public Methods
        public void AddRecolectable(RecolectableType type, int count)
        {
            if (InventoryCount.ContainsKey(type))
            {
                InventoryCount[type] += count;
            }
            else
            {
                InventoryCount.Add(type, count);
            }
                OnInventoryUpdated?.Invoke();
        }

        public bool UseResource(RecolectableType type, int count = 1)
        {
            if(InventoryCount[type] >= count)
            {
                InventoryCount[type] -= count;
                OnInventoryUpdated?.Invoke();
                return true;
            }
            return false;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
