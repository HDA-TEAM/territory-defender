using Common.Scripts;
using GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip;
using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public class TowerSellingTool : TowerToolBase
    {
        [Header("Sounds"), Space(12)]
        [SerializeField] private AudioClip _audioClipSold;
        
        protected override void ApplyTool()
        {
            _towerKit.SellingTower();
            Messenger.Default.Publish(new AudioPlayOneShotPayload
            {
                AudioClip = _audioClipSold,
            });
        }
        protected override void ShowPreviewChanging()
        {
            _towerKit.ShowPreviewChanging(
                new TowerPreviewSoldToolTipComposite(_towerKit.GetSoldTowerCoin().ToString()
                )
            );
        }
    }
}
