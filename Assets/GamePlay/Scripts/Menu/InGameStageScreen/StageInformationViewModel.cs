using GamePlay.Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Scripts.Menu.InGameStageScreen
{
    public struct StageResourcePayload
    {
        
    }
    public class StageInformationViewModel : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private StageResourceView _stageResourceView;
        
        [Header("Data"), Space(12)] [SerializeField]
        private InGameResourceRuntimeData _resourceRuntimeData;

        // tool
        // hero skill or hero selection
    
        private void Start()
        {
            UpdateView();
        }
        private void Awake()
        {
            _resourceRuntimeData.RegisterLifeChange(OnInventoryChange);
            _resourceRuntimeData.RegisterCurrencyChange(OnInventoryChange);
        }
        private void OnDestroy()
        {
            _resourceRuntimeData.UnRegisterLifeChange(OnInventoryChange);
            _resourceRuntimeData.UnRegisterCurrencyChange(OnInventoryChange);
        }
        private void OnInventoryChange(int fakeValue) => UpdateView();
        private void UpdateView()
        {
            _stageResourceView.Setup(new StageResource
            {
                CurLife = _resourceRuntimeData.GetLifeValue(),
                TotalCoin = _resourceRuntimeData.GetCurrencyValue(),
                CurWaveCount = 0,
                MaxWaveCount = 5,
            });
        }
    }
}
