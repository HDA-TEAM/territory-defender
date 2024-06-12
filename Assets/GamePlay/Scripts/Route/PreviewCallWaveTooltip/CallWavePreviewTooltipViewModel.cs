using Common.Scripts;
using GamePlay.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;


namespace GamePlay.Scripts.Route.PreviewCallWaveTooltip
{
    public enum ECallWaveUnitPreviewDirectionType
    {
        LeftSide = 0,
        RightSide = 1,
        TopSide = 2,
        DownSide = 3,
    }

    public struct CallWavePreviewUnitComposite
    {
        public ECallWaveUnitPreviewDirectionType DirectionType;
        public List<SingleUnitPreviewComposite> UnitPreviewComposites;
    }

    public struct SingleUnitPreviewComposite
    {
        public UnitId.Enemy EnemyId;
        public int Amount;
    }

    public class CallWavePreviewTooltipViewModel : MonoBehaviour
    {
        [SerializeField] private List<SingleUnitCallWavePreviewTooltipView> _unitCallWavePreviewViews;
        [SerializeField] private EnemyDataConfigBase _enemyDataConfigBase;
        public void Setup(CallWavePreviewUnitComposite callWavePreviewUnitComposite)
        {
            SetupViews(callWavePreviewUnitComposite.UnitPreviewComposites);
        }
        private void SetupViews(List<SingleUnitPreviewComposite> unitPreviewComposites)
        {
            int availableShowItem = unitPreviewComposites.Count;
            for (int i = 0; i < _unitCallWavePreviewViews.Count; i++)
            {
                if (i < availableShowItem)
                {
                    _unitCallWavePreviewViews[i].gameObject.SetActive(true);

                    var avatar = _enemyDataConfigBase.GetConfigByKey(unitPreviewComposites[i].EnemyId).UnitSprites;
                    _unitCallWavePreviewViews[i].Setup(avatar.AvatarIcon, unitPreviewComposites[i].Amount);
                }
                else
                    _unitCallWavePreviewViews[i].gameObject.SetActive(false);
            }
        }
    }
}
