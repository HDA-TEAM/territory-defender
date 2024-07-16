using GamePlay.Scripts.Data;
using GamePlay.Scripts.Menu.UnitInformationPanel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Menu.InGameStageScreen.UnitInformationPanel
{
    public class UnitAvatarView : MonoBehaviour
    {
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _txtName;
        [SerializeField] private TotalUnitDataConfig _totalUnitDataConfig;
        public void Setup(ShowStatsInformationComposite statsInformationComposite)
        {
            _txtName.text = statsInformationComposite.Name;
            UnitDataComposite unitDataComposite = _totalUnitDataConfig.GetSingleUnitDataConfig(statsInformationComposite.UnitBase.UnitId);
            if (unitDataComposite.UnitSprites.AvatarIcon)
            {
                _avatar.sprite = unitDataComposite.UnitSprites.AvatarIcon;
                _avatar.SetNativeSize();
                RectTransform avatarRect = _avatar.GetComponent<RectTransform>();
                avatarRect.anchorMax = new Vector2(0.5f, 0.5f);
                avatarRect.anchorMin = new Vector2(0.5f, 0.5f);
                avatarRect.anchoredPosition = Vector2.zero;
            }
        }
    }
}
