using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoDetailView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtStageName;
    [SerializeField] private Image _imgStageMap;
    
    #region Core

    public void Setup(StageComposite stageComposite)
    {
        _txtStageName.text = stageComposite.StageName;
    }
    #endregion
}

