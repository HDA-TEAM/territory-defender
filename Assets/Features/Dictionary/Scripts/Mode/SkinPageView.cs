using UnityEngine;

public class SkinPageView : MonoBehaviour, IHeroModePageView
{
    [SerializeField] private GameObject _objSkinPageView;
    
    public void PageSelected(bool isSelected)
    {
        _objSkinPageView.gameObject.SetActive(isSelected);
    }
}
