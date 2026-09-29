using Deforestation.Inventory;
using Deforestation.UI;
using UnityEngine;
namespace Deforestation.Interaction
{

	public class InteractionSystem : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		[SerializeField] UIGameController _uiController;
		[SerializeField] PlayerInventory _inventory;
        [SerializeField] bool _interactableDetected = false;
        IInteractable _currentInteraction;

        #endregion

        #region Unity Callbacks
        [System.Obsolete]
        private void Update()
        {
            if(_interactableDetected & Input.GetKeyUp(KeyCode.E))
			{
				if(_currentInteraction is Recolectables recolectables)
				{
                    _inventory.AddRecolectable(recolectables.MineralType, recolectables.MineralValue);
					recolectables.Interact();
                }
				if(_currentInteraction is MachineInteraction machineInteraction)
				{
                    machineInteraction.Interact();
                }
            }
        }

        void FixedUpdate()
		{
			RaycastHit hit;
			if (Physics.SphereCast(Camera.main.transform.position, .2f,Camera.main.transform.forward, out hit, 10))
			{
				IInteractable interactaction = hit.collider.GetComponent<IInteractable>();
				if(interactaction != null )
				{
					InteractableInfo info = interactaction.GetInfo();
					_uiController.ShowInteraction(info.Action + " " + info.Type);
					Debug.Log(info.Action + "" + info.Type);
					_interactableDetected = true;
					_currentInteraction = interactaction;
                    return;
                }
			}
			_uiController.HideInteraction();
			_interactableDetected = false;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        private void OnDrawGizmos()
        {
			Gizmos.DrawWireSphere(Camera.main.transform.position, .2f);
			Vector3 B = Camera.main.transform.forward;

            Gizmos.DrawWireSphere(Camera.main.transform.position + B*10, .2f);
        }
    }
}