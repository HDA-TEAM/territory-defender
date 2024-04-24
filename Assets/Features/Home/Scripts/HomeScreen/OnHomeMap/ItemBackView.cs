using UnityEngine;
using UnityEngine.UI;

public class ItemBackView : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    
    private void Start()
    {
        _backButton.onClick.AddListener(OnBackButtonPressed);
    }
    
    private void OnBackButtonPressed()
    {
        var stateMachine = UIManagerStateMachine.Instance;   
        stateMachine.BackPressed();
    }
}


