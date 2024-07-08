using TMPro;
using UnityEngine;

namespace Features.Dictionary.Scripts.View
{
    public struct UnitDictionaryStatComposite
    {
        public string StatName;
        public string StatVal;
    }
    public class UnitDictionaryStatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtStatName;
        [SerializeField] private TextMeshProUGUI _txtStatVal;
        public void Setup(UnitDictionaryStatComposite unitDictionaryStatComposite)
        {
            _txtStatName.text = unitDictionaryStatComposite.StatName;
            _txtStatVal.text = unitDictionaryStatComposite.StatVal;
        }
    }
}
