using System;
using UnityEngine;
namespace Deforestation
{
	public class HealthSystem : MonoBehaviour
	{
		#region Properties
		public event Action<float> OnHealthChanged;
		public event Action OnDeath;
		public float CurrentHealth;
		#endregion

		#region Fields
		[SerializeField] float maxHealth;
		#endregion

		#region Unity Callbacks
		void Awake()
		{
			CurrentHealth = maxHealth;
		}
		#endregion

		#region Public Methods
		public void OnDamaged(float damage)
		{
			Debug.Log("Damaged " + gameObject.name + " for: " + damage);
			CurrentHealth -= damage;
            OnHealthChanged?.Invoke(CurrentHealth);

			if (CurrentHealth < 0)
			{
				Death();
			}
		}

		public void OnHealed(float heal)
		{
			Debug.Log("Healed " + gameObject.name + " for: " + heal);
			CurrentHealth += heal;
			CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);
			OnHealthChanged?.Invoke(CurrentHealth);
		}
		#endregion

		#region Private Methods
		private void Death()
		{
			OnDeath?.Invoke();
		}
		#endregion
	}
}