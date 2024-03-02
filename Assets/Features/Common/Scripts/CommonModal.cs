using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;

public class CommonModal : Modal
{
    [SerializeField] private Button[] _btnCloses;
    private void Awake()
    {
        foreach (var btn in _btnCloses)
        {
            btn.onClick.AddListener(PopModal);
        }
    }
    private void PopModal() => HomeController.PopModal();
}
