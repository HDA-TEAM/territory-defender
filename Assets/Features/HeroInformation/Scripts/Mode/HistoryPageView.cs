using UnityEngine;

public interface IHeroModePageView
{
    public void PageSelected(bool isSelected);
}
public class HistoryPageView : MonoBehaviour, IHeroModePageView
{
    [SerializeField] private GameObject _objHistoryPageView;

    public void PageSelected(bool isSelected)
    {
        _objHistoryPageView.gameObject.SetActive(isSelected);
    }
}
