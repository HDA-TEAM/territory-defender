using System;
using System.Collections.Generic;
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
        
        _buttonHeroInfo.onClick.AddListener(() => _stateMachine.ChangeState<HeroInfoState>());
        //_buttonShop.onClick.AddListener(() => _stateMachine.ChangeState<ShopState>());
        _buttonDictionary.onClick.AddListener(() => _stateMachine.ChangeState<DictionaryState>());
        _buttonHistory.onClick.AddListener(() => _stateMachine.ChangeState<HistoryState>());
        _buttonUpgradeTower.onClick.AddListener(() => _stateMachine.ChangeState<MasteryPageState>());
        _buttonSetting.onClick.AddListener((() => _stateMachine.ChangeState<SettingState>()));
        _buttonQuest.onClick.AddListener((() => _stateMachine.ChangeState<QuestState>()));
    }
}
