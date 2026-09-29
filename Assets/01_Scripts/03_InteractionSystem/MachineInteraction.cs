using UnityEngine;

namespace Deforestation.Interaction
{
    public enum MachineInteractionType
    {
    	Stair,
    	Door,
        Machine
    }

    public class MachineInteraction : MonoBehaviour, IInteractable
    {
    	#region Properties
    	#endregion

    	#region Fields
    	[SerializeField] private MachineInteractionType _type;
    	[SerializeField] private Transform _target;

        [SerializeField] private InteractableInfo _interactableInfo;

        public InteractableInfo GetInfo()
        {
            _interactableInfo.Type = _type.ToString();
            return _interactableInfo;
        }

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
        [System.Obsolete]
        public void Interact()
        {
            if(_type == MachineInteractionType.Door)
            {
                transform.position = _target.position;
            }
            if(_type == MachineInteractionType.Stair)
            {
                GameController.Instance.PlayerTeleport(_target.position);
            }
            if (_type == MachineInteractionType.Machine)
            {
                GameController.Instance.MachineMode(true);
            }
        }
        #endregion

        #region Private Methods
        #endregion
    }
    }