using Common.Loading.Scripts;
using CustomInspector;
using GamePlay.Scripts.GamePlay;
using GamePlay.Scripts.Route;
using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.Scripts.GamePlayController
{
    public partial class InGameStateController
    {
#if UNITY_EDITOR
        public void SetUpTestNewGame(StartStageComposite startStageComposite)
        {
            _startStageComposite = startStageComposite;
            IsGamePlaying = true;
            
            Messenger.Default.Publish(new SetUpNewGamePayload
            {
                StartStageComposite = startStageComposite,
            });
        }
        [Button("SetUpTestNewGame",usePropertyAsParameter: true)]
#endif
       
        [SerializeField] private StartStageComposite _startStageComposite;
        
        public StartStageComposite StartStageComposite
        {
            get
            {
                return _startStageComposite;
            }
        }
       
        protected override void OnSetupNewGame(SetUpNewGamePayload setUpNewGamePayload)
        {
            Init();
            _startStageComposite = setUpNewGamePayload.StartStageComposite;
            Messenger.Default.Publish(new PrepareNextWavePayload
            {
                DurationEarlyCallWaveAvailable = 0f,
                WaveIndex = 0,
                OnEarlyCallWave = StartSpawning,
            });
        }
        protected override void OnResetGame(ResetGamePayload resetGamePayload)
        {
            IsGamePlaying = false;
        }
    }
}
