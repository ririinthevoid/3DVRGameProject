using UnityEngine;
using TMPro;

namespace Deforestation.UI
{

	public class UIInteractionPannel : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		[SerializeField] private TextMeshProUGUI _textPanel;
		#endregion

		#region Unity Callbacks
		void Start()
		{
            gameObject.SetActive(false);

        }

        void Update()
		{

		}
		#endregion

		#region Public Methods
		public void Show(string message)
		{
			gameObject.SetActive(true);
			_textPanel.text = message; 
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
		#endregion

		#region Private Methods
		#endregion
	}
}