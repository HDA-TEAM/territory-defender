using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Features.Home.Scripts.HomeScreen.OnHomeMap
{
    public class HomeMenuViewModel: MonoBehaviour
    {
        [Header("Button"), Space(12)]
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
            _buttonDictionary.onClick.AddListener(() => stateMachine.ChangeModalState<DictionaryPuState>());
            _buttonHistory.onClick.AddListener(() => stateMachine.ChangeModalState<HistoryPuState>());
            _buttonUpgradeTower.onClick.AddListener(() => stateMachine.ChangeModalState<MasteryPagePuState>());
            _buttonSetting.onClick.AddListener((() => stateMachine.ChangeModalState<SettingPuState>()));
            _buttonQuest.onClick.AddListener((() => stateMachine.ChangeModalState<QuestPuState>()));
            
        }
    }
}
