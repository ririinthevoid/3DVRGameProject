using System;
using UnityEngine;
namespace Deforestation.Machine.Weapon
{

	public class UpgradeManager : MonoBehaviour
	{
		#region Properties
		public event Action<int> OnMaxAmmoUpgraded;
		public event Action<float> OnReloadSpeedUpgraded;
		public event Action<int> OnHealthRegenUpgraded;
		#endregion

		#region Fields
		[Header("Mineral Ammounts")]
		private int _iron;
		private int _tin;
		private int _copper;

		[Header("Reload Upgrades")]
		[SerializeField] private float _baseReloadSpeed;
		private float _newReloadSpeed;
		private int _reloadSpeedUpgrades;

		[Header("Ammo Upgrades")]
		[SerializeField] private int _baseMaxAmmo;
		private int _newMaxAmmo;
		private int _maxAmmoUpgrades;

		[Header("Health Regen Upgrades")]
		[SerializeField] private int _baseHealthRegen;
		private int _newHealthRegen;
		private int _healthRegenUpgrades;
		#endregion

		#region Unity Callbacks
		void Start()
		{

		}

		[System.Obsolete]
		void Update()
		{
			var Dictionary = GameController.Instance.PlayerInventory.InventoryCount;

            _tin = Dictionary[Inventory.RecolectableType.Tin];
            _copper = Dictionary[Inventory.RecolectableType.Copper];
            _iron = Dictionary[Inventory.RecolectableType.Iron];


            _reloadSpeedUpgrades = _iron / 10;
			if(_reloadSpeedUpgrades < 10)
            {
				ReloadSpeedUpgrade();
			}

            _maxAmmoUpgrades = _tin / 5;
			if(_maxAmmoUpgrades < 20)
			{
                MaxAmmoUpgrade();
			}

            _healthRegenUpgrades = _copper / 10;
			_newHealthRegen = _baseHealthRegen * _healthRegenUpgrades;
			Debug.Log(_newHealthRegen);
			OnHealthRegenUpgraded?.Invoke(_newHealthRegen);
        }
        #endregion

        #region Private Methods
        private void MaxAmmoUpgrade()
		{
			_newMaxAmmo = _baseMaxAmmo + _maxAmmoUpgrades;
			Debug.Log(_newMaxAmmo);
			OnMaxAmmoUpgraded?.Invoke(_newMaxAmmo);
		}

		private void ReloadSpeedUpgrade()
		{
			_newReloadSpeed = _baseReloadSpeed - ((_baseReloadSpeed * 5 * _reloadSpeedUpgrades / 100));
			Debug.Log(_newReloadSpeed);
            OnReloadSpeedUpgraded?.Invoke(_newReloadSpeed);
		}
		#endregion
	}
}