using Common.Loading.Scripts;
using Common.Scripts.Data.DataAsset;
using GamePlay.Scripts.Menu.ResultPu;
using UnityEngine;

public class StageSuccessModelView : MonoBehaviour
{
    [SerializeField] private StageSuccessView _stageSuccessView;
    [SerializeField] private StageSuccessRewardItemView _rewardCoinItemView;
    [SerializeField] private StageSuccessRewardItemView _rewardTalentPointItemView;
    [SerializeField] private int _defaultCoinPerStar;
    [SerializeField] private int _defaultTalentPointPerStar;
    [SerializeField] private InventoryDataAsset _inventoryDataAsset;
    public void SetupView(int claimingStars, int incStars)
    {
        _stageSuccessView.Setup(OnClickContinue, claimingStars);
        if (incStars > 0)
        {
            int coinClaim = _defaultCoinPerStar * incStars;
            int talentPointClaim = _defaultTalentPointPerStar * incStars;
            
            _inventoryDataAsset.TryChangeInventoryData(InventoryType.SliverCoin, coinClaim);
            _inventoryDataAsset.TryChangeInventoryData(InventoryType.TalentPoint, talentPointClaim);
            _rewardCoinItemView.SetUp(coinClaim);
            _rewardTalentPointItemView.SetUp(talentPointClaim);
        }
        else
        {
            _rewardCoinItemView.gameObject.SetActive(false);
            _rewardTalentPointItemView.gameObject.SetActive(false);
        }

    }
    private void OnClickContinue()
    {
        // Load scene home
        LoadingSceneController.Instance.LoadingGameToHome();
    }
}
