using Common.Scripts;
using GamePlay.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Menu.UnitInformationPanel
{
    public class UnitAvatarView : MonoBehaviour
    {
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _txtName;
        [SerializeField] private TotalUnitDataConfig _totalUnitDataConfig;
        public void Setup(ShowStatsInformationComposite statsInformationComposite)
        {
            _txtName.text = statsInformationComposite.Name;
            Debug.Log(_totalUnitDataConfig.GetSingleUnitDataConfig(UnitId.Hero.TrungTrac.ToString()).UnitBase.name);
        }
    }
}
