using UnityEngine;
using TMPro;
using Deforestation.Inventory;
using Deforestation.Interaction;
using UnityEngine.UI;

namespace Deforestation.UI
{

	public class UIGameController : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		[Header("Controllers")]
        [System.Obsolete]
		private PlayerInventory _playerInventory => GameController.Instance.PlayerInventory;
        [System.Obsolete]
        private InteractionSystem _interactionSystem => GameController.Instance.InteractionSystem;

        [Header("ResourceUI")]
        [SerializeField] private TextMeshProUGUI _ironText;
		[SerializeField] private TextMeshProUGUI _tinText;
		[SerializeField] private TextMeshProUGUI _copperText;

		[Header("HealthUI")]
		[SerializeField] private Slider _playerHealth;
		[SerializeField] private Slider _machineHealth;

        [Header("OtherUI")]
        [SerializeField] private UIInteractionPannel _interactionPanel;
        #endregion

        #region Unity Callbacks
        void Start()
		{
			_playerInventory.OnInventoryUpdated += UpdateInventoryUI;
		}

		void Update()
		{

		}
        #endregion

        #region Public Methods
        public  void ShowInteraction(string message)
        {
			_interactionPanel.Show(message);
        }

		public void HideInteraction()
		{
            _interactionPanel.Hide();
        }

		public void UpdatePlayerHealth(float value)
		{
            _playerHealth.value = value;
        }

		public void UpdateMachineHealth(float value)
		{
            _machineHealth.value = value;
        }
        #endregion

        #region Private Methods
        private void UpdateInventoryUI()
		{
			if (_playerInventory.InventoryCount.ContainsKey(RecolectableType.Iron))
				_ironText.text = _playerInventory.InventoryCount[RecolectableType.Iron].ToString();
			else
				_ironText.text = "0";

			if (_playerInventory.InventoryCount.ContainsKey(RecolectableType.Tin))
				_tinText.text = _playerInventory.InventoryCount[RecolectableType.Tin].ToString();
			else 
				_tinText.text = "0";
			
			if (_playerInventory.InventoryCount.ContainsKey(RecolectableType.Copper))
				_copperText.text = _playerInventory.InventoryCount[RecolectableType.Copper].ToString();
			else
				_copperText.text = "0";
		}
        #endregion
    }
}