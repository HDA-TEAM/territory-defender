using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillDetailView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtSkillName;
    [SerializeField] private TextMeshProUGUI _txtSkillText;
    [SerializeField] private Image _imgSkill;
    
    #region Core
    public void Setup(SkillComposite skillComposite)
    {
        _txtSkillName.text = skillComposite.Name;
        _txtSkillText.text = skillComposite.SkillText;
        _imgSkill.sprite = skillComposite.SkillImage;
    }
    #endregion
}

