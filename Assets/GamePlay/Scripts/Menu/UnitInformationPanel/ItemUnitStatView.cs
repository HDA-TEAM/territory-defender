using GamePlay.Scripts.Character.StateMachine.Stats;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Menu.UnitInformationPanel
{
    public struct ShowStatsInformationComposite
    {
        public string Name;
        public List<ItemStatComposite> StatComposites;
    }
    public struct ItemStatComposite
    {
        public StatId StatId;
        public string StatVal;
    }
    public class ItemUnitStatView : MonoBehaviour
    {
        [SerializeField] private Image _iconStat;
        [SerializeField] private TextMeshProUGUI _txtStatVal;
        [SerializeField] private StatIconConfig _statIconConfig;
        public void Setup(ItemStatComposite itemStatComposite)
        {
            _iconStat.sprite = _statIconConfig.GetStatIcon(itemStatComposite.StatId);
            _txtStatVal.text = itemStatComposite.StatVal;
        }
    }
}
