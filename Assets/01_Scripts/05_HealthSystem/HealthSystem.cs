using System;
using UnityEngine;
namespace Deforestation
{
	public class HealthSystem : MonoBehaviour
	{
		#region Properties
		public event Action<float> OnHealthChanged;
		public event Action OnDeath;
		#endregion

		#region Fields
		[SerializeField] float maxHealth;
		[SerializeField] float currentHealth;
		#endregion

		#region Unity Callbacks
		void Awake()
		{
			currentHealth = maxHealth;
		}
		#endregion

		#region Public Methods
		public void OnDamaged(float damage)
		{
			currentHealth -= damage;
			OnHealthChanged?.Invoke(currentHealth);

			if (currentHealth < 0)
			{
				Death();
			}
		}

		public void OnHealed(float heal)
		{
			currentHealth += heal;
			currentHealth = Mathf.Min(currentHealth, maxHealth);
			OnHealthChanged?.Invoke(currentHealth);
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