using UnityEngine;
using UnityEngine.UI;

public class ItemBackView : MonoBehaviour
{
    [SerializeField] private Button _backButton;

    private UIManagerStateMachine _stateMachine;
    private void Start()
    {
        _stateMachine = new UIManagerStateMachine();
        _backButton.onClick.AddListener(OnBackButtonPressed);
    }
    
    private void OnBackButtonPressed()
    {
        _stateMachine.BackPressed();
    }
}


