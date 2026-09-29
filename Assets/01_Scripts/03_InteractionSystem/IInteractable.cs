using UnityEngine;
namespace Deforestation.Interaction
{

    public interface IInteractable
    {
        public void Interact();

        public InteractableInfo GetInfo();

    }

    //Move To Other File
    [System.Serializable]
    public class InteractableInfo
    {
        public string Action;
        public string Type;
    }
}