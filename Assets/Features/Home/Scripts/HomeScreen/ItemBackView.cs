using BrunoMikoski.UIManager;
using UnityEngine;
using UnityEngine.UI;

public class ItemBackView : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    
    [SerializeField] private ListTowerViewModel _listTowerViewModel;
    [SerializeField] private ListHeroViewModel _listHeroViewModel;
    
    private UIManagerStateMachine _stateMachine;
    private void Start()
    {
        _stateMachine = new UIManagerStateMachine();
        _backButton.onClick.AddListener(OnBackButtonPressed);
    }

    private void CloseIfPopup(PrefabUIWindow window)
    {
        if (window != null && window.Layer.Behaviour == UILayerBehaviour.Additive)
        {
            window.Close();
            Debug.Log(window.name + " popup closed.");
        }
    }
    
    private void OnBackButtonPressed()
    {
        GlobalUtility.ResetView(_listTowerViewModel, _listHeroViewModel);
        _stateMachine.BackPressed();
    }
}


