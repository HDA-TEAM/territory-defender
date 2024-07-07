using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Dictionary.Scripts.View
{
    public struct UnitDictionaryDetailComposite
    {
        public string Name;
        public Sprite Avatar;
        public List<UnitDictionaryStatComposite> UnitDictionaryStatComposites;
    }
    public class UnitDictionaryDetailView : MonoBehaviour
    {
        [SerializeField]
        private List<UnitDictionaryStatView> _unitDictionaryStatViews;
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private Image _imgAvatar;
        public void SetUp(UnitDictionaryDetailComposite unitDictionaryDetailComposite)
        {
            int numView = unitDictionaryDetailComposite.UnitDictionaryStatComposites.Count;
            for (int i = 0; i < _unitDictionaryStatViews.Count; i++)
            {
                bool isShow = i < numView;
                if (i < numView)
                {
                    _unitDictionaryStatViews[i].Setup(unitDictionaryDetailComposite.UnitDictionaryStatComposites[i]);
                }
                _unitDictionaryStatViews[i].gameObject.SetActive(isShow);
            }
            _textName.text = unitDictionaryDetailComposite.Name;
            _imgAvatar.sprite = unitDictionaryDetailComposite.Avatar;
        }
    }
}
