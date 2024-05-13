using Common.Loading.Scripts;
using CustomInspector;
using GamePlay.Scripts.GamePlay;
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
        
        public override void SetUpNewGame(StartStageComposite startStageComposite)
        {
            Init();
            
            _startStageComposite = startStageComposite;

            Messenger.Default.Publish(new SetUpNewGamePayload
            {
                StartStageComposite = startStageComposite,
            });
        }
        public override void ResetGame()
        {
            IsGamePlaying = false;
            // Stop update game first
            UnitManager.Instance.ResetGame();
            // remove all units
            Messenger.Default.Publish(new ResetGamePayload());
        }
    }
}
