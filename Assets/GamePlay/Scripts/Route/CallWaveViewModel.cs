using UnityEngine;

namespace GamePlay.Scripts.Route
{
    public class CallWaveViewModel : MonoBehaviour
    {
        [SerializeField] private CallWaveView _callWaveView;
        public void Setup()
        {
            _callWaveView.Setup(CallWave);
        }
        private void CallWave()
        {
            
        }
    }
}
