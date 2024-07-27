using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using System;
using UnityEngine;

namespace Features.HeroInformation.Scripts.Mode
{
    public class HeroUpgradeViewModel : MonoBehaviour
    {
        [SerializeField] private ButtonUpgradeView _buttonUpgradeView;
        [SerializeField] private InventoryDataAsset _inventoryDataAsset;
        [SerializeField] private HeroDataAsset _heroDataAsset;
        [SerializeField] private HeroUpgradeConfig _heroUpgradeConfig;

        private Action _onClickUpgradeSuccess;
        private UnitId.Hero _curHeroId;
        private int _upgradeCoin;
        public void SetUpUpgradable(UnitId.Hero heroId, Action onClickUpgrade)
        {
            _upgradeCoin = _heroUpgradeConfig.GetExpNeed(_heroDataAsset.GetHeroLevel(heroId));
            int curCoin = _inventoryDataAsset.GetInventoryDataByType(InventoryType.SliverCoin).Amount;
            bool isUpgradable = curCoin >= _upgradeCoin;
            Action upgradeAction = isUpgradable ? onClickUpgrade : null;
            _onClickUpgradeSuccess = upgradeAction;
            _curHeroId = heroId;
            _buttonUpgradeView.Setup(_upgradeCoin, isUpgradable, OnClickUpgradeSuccess);
        }
        private void OnClickUpgradeSuccess()
        {
            if (_onClickUpgradeSuccess != null)
            {
                _inventoryDataAsset.TryChangeInventoryData(InventoryType.SliverCoin, -_upgradeCoin);
                _heroDataAsset.UpgradeHeroLevel(_curHeroId);
            }
            _onClickUpgradeSuccess?.Invoke();
        }
    }
}
