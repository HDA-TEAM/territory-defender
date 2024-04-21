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
    private void Start()
    {
        var stateMachine = UIManagerStateMachine.Instance;   
        _buttonHeroInfo.onClick.AddListener(() => stateMachine.ChangeModalState<HeroInfoPuState>());
        //_buttonShop.onClick.AddListener(() => _stateMachine.ChangeState<ShopState>());
        _buttonDictionary.onClick.AddListener(() => stateMachine.ChangeModalState<DictionaryPuState>());
        _buttonHistory.onClick.AddListener(() => stateMachine.ChangeModalState<HistoryPuState>());
        _buttonUpgradeTower.onClick.AddListener(() => stateMachine.ChangeModalState<MasteryPagePuState>());
        _buttonSetting.onClick.AddListener((() => stateMachine.ChangeModalState<SettingPuState>()));
        _buttonQuest.onClick.AddListener((() => stateMachine.ChangeModalState<QuestPuState>()));
    }
}
