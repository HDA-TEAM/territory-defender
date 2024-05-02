using GamePlay.Scripts.Tower.TowerKIT;
using System;
using UnityEngine;

namespace GamePlay.Scripts.Route
{
    public class CallWaveBtnViewModel : MonoBehaviour
    {
        [SerializeField] private CallWaveBtnView _callWaveBtnView;
        [SerializeField] private ConfirmHandle _confirmHandle;

        private int _routeId;
        private Action<int> _onReviewChanging;
        private Action<int> _onAccepted;
        public void Setup(Action<int> onAccepted, Action<int> onPreviewChanging, int routeId)
        {
            _routeId = routeId;
            _onReviewChanging = onPreviewChanging;
            _onAccepted = onAccepted;
            
            _confirmHandle.SetUp(OnAccepted,OnPreviewChanging);
            _callWaveBtnView.Setup(null);
        }
        private void OnAccepted()
        {
            _onAccepted?.Invoke(_routeId);
        }
        private void OnPreviewChanging()
        {
            _onReviewChanging?.Invoke(_routeId);
        }

    }
}
