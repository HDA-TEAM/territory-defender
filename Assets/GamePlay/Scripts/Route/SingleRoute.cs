using GamePlay.Scripts.Route.PreviewCallWaveTooltip;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Scripts.Route
{
    public class SingleRoute : MonoBehaviour
    {
        [FormerlySerializedAs("CallWaveView")]
        public CallWaveBtnView _callWaveBtnView; 
        public LineRenderer SingleLineRenderer;
        public HandleSingleCallWaveShowTooltip HandleSingleCallWaveShowTooltip;
    }
}
