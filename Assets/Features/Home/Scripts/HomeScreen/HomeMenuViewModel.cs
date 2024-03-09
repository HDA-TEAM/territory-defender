using UnityEngine;
using UnityEngine.UI;

public class HomeMenuViewModel: MonoBehaviour
{
    //[SerializeField] private Button _buttonShop;
    [SerializeField] private Button _buttonHeroInfo;
    [SerializeField] private Button _buttonDictionary;
    [SerializeField] private Button _buttonHistory;
    [SerializeField] private Button _buttonUpgradeTower;
    [SerializeField] private Button _buttonSetting;
    [SerializeField] private Button _buttonQuest;

    // Internal
    private static UIManagerStateMachine _stateMachine;
    private void Start()
    {
        _stateMachine = new UIManagerStateMachine();
        
        _buttonHeroInfo.onClick.AddListener(() => _stateMachine.ChangeModalState<HeroInfoSceneState>());
        //_buttonShop.onClick.AddListener(() => _stateMachine.ChangeState<ShopState>());
        _buttonDictionary.onClick.AddListener(() => _stateMachine.ChangeModalState<DictionarySceneState>());
        _buttonHistory.onClick.AddListener(() => _stateMachine.ChangeModalState<HistoryState>());
        _buttonUpgradeTower.onClick.AddListener(() => _stateMachine.ChangeModalState<MasteryPageState>());
        _buttonSetting.onClick.AddListener((() => _stateMachine.ChangeModalState<SettingState>()));
        _buttonQuest.onClick.AddListener((() => _stateMachine.ChangeModalState<QuestState>()));
    }
}
