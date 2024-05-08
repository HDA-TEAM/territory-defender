using Common.Loading.Scripts;
using Common.Scripts.Navigator;
using UnityEngine;
using UnityEngine.UI;

public class StageFailedPu : CommonModal
{
    [Header("UI")]
    [SerializeField] private Button _btnReplay;
    [SerializeField] private Button _btnQuit;
    private void Awake()
    {
        _btnQuit.onClick.AddListener(OnClickQuit);
    }

    private void OnClickQuit()
    {
        // Load scene home
        LoadingSceneController.Instance.LoadingGameToHome();
    }
}
