using System;
using UnityEngine;

public class Water : MonoBehaviour
{
    #region Properties
    public event Action PlayerDrown;
    #endregion

    #region Fields
    [SerializeField] private MeshCollider m_Collider;
    private bool _DOTRunning;
    #endregion

    #region Unity Callbacks
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if ((other.transform.position.y + 1) < gameObject.transform.position.y)
            {
                PlayerDrown?.Invoke();
            }
        }
        #endregion
    }
}