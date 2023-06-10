using UnityEngine;
using UnityEngine.UI;

public class ButtonBuildTower : MonoBehaviour
{
    public Button buttonBuild; 
    [SerializeField] Image defaultIcon;
    [SerializeField] Image acceptedIcon;
    public void OnHandleAccepted()
    {
        acceptedIcon.gameObject.SetActive(true);
        defaultIcon.gameObject.SetActive(false);
    }
    public bool IsAccepted()
    {
        return  acceptedIcon.gameObject.activeSelf;
    }
    public void ResetToDefault()
    {
        acceptedIcon.gameObject.SetActive(false);
        defaultIcon.gameObject.SetActive(true);
        buttonBuild.onClick.RemoveAllListeners();
    }
    
}
