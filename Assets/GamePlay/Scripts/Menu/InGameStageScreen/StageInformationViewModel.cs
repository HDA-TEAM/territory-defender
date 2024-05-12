using GamePlay.Scripts.Data;
using GamePlay.Scripts.Stage;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Scripts.Menu.InGameStageScreen
{
    public class StageInformationViewModel : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private StageResourceView _stageResourceView;
        
        [Header("Data"), Space(12)] [SerializeField]
        private InGameResourceRuntimeData _resourceRuntimeData;

        private UpdateWavePayload _updateWavePayload;
        // tool
        // hero skill or hero selection
    
        private void Start()
        {
            UpdateView();
        }
        private void Awake()
        {
            Messenger.Default.Subscribe<UpdateWavePayload>(OnWaveChange);
            _resourceRuntimeData.RegisterLifeChange(OnInventoryChange);
            _resourceRuntimeData.RegisterCurrencyChange(OnInventoryChange);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<UpdateWavePayload>(OnWaveChange);
            _resourceRuntimeData.UnRegisterLifeChange(OnInventoryChange);
            _resourceRuntimeData.UnRegisterCurrencyChange(OnInventoryChange);
        }
        private void OnInventoryChange(int fakeValue) => UpdateView();
        private void OnWaveChange(UpdateWavePayload updateWavePayload)
        {
            _updateWavePayload = updateWavePayload;
            UpdateView();
        }
        private void UpdateView()
        {
            _stageResourceView.Setup(new StageResource
            {
                CurLife = _resourceRuntimeData.GetLifeValue(),
                TotalCoin = _resourceRuntimeData.GetCurrencyValue(),
                CurWaveCount = _updateWavePayload.CurWave,
                MaxWaveCount = _updateWavePayload.MaxWave,
            });
        }
    }
}
