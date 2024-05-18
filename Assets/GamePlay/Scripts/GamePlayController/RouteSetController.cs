using Common.Scripts.Utilities;
using CustomInspector;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using GamePlay.Scripts.Route;
using GamePlay.Scripts.Route.PreviewCallWaveTooltip;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.GamePlayController
{
    public struct OnGetNearestPosFromRoutePayload
    {
        public Vector3 PosInput;
        public Action<Vector3> OnCalculateSuccess;
    }
    public class RouteSetController : GamePlayMainFlowBase
    {
        [Button("SaveToConfig")]
        [Button("LoadFromConfig")]
        [SerializeField] private StageId _currentStageId;
        [SerializeField] private List<SingleRoute> _currentSingleRouteComposite = new List<SingleRoute>();
        [SerializeField] private List<SingleRoute> _activeSingleRouteComposite;
        [SerializeField] private RouteSetConfig _routeSetConfig;
        [SerializeField] private CallWaveListViewModel _callWaveListViewModel;
        public List<SingleRoute> ActiveSingleRouteLineRenderers
        {
            get
            {
                return _activeSingleRouteComposite;
            }
        }
        
        protected override void Awake()
        {
            base.Awake();
            Messenger.Default.Subscribe<OnGetNearestPosFromRoutePayload>(GetNearestPosFromRoute);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Messenger.Default.Unsubscribe<OnGetNearestPosFromRoutePayload>(GetNearestPosFromRoute);
        }
        
        private void Start()
        {
            foreach (var lineRender in _currentSingleRouteComposite)
            {
                lineRender.SingleLineRenderer.widthMultiplier = 0f;
            }
        }

        private void SaveToConfig()
        {
            RouteSetConfig.RouteSet routeSet = new RouteSetConfig.RouteSet
            {
                RouteLines = new List<RouteSetConfig.RouteLine>(),
            };

            for (int i = 0; i < _currentSingleRouteComposite.Count; i++)
            {
                // Check if lineRender want to save
                if (!_currentSingleRouteComposite[i].SingleLineRenderer.gameObject.activeSelf)
                    continue;

                routeSet.ECallWaveUnitPreviewDirectionType = _currentSingleRouteComposite[i].HandleSingleCallWaveShowTooltip.ECallWaveUnitPreviewDirectionType;

                // add RouteSetConfig.RouteLine
                routeSet.RouteLines.Add(new RouteSetConfig.RouteLine
                {
                    CallwaveButtonPos = _currentSingleRouteComposite[i].CallWaveBtnViewModel.transform.position,
                    PointSet = new List<Vector3>()
                });

                for (int j = 0; j < _currentSingleRouteComposite[i].SingleLineRenderer.positionCount; j++)
                    routeSet.RouteLines[i].PointSet.Add(_currentSingleRouteComposite[i].SingleLineRenderer.GetPosition(j));
            }

            _routeSetConfig.SaveToConfig(routeSet, _currentStageId);
        }

        private void LoadFromConfig()
        {
            List<CallWaveBtnViewModel> callWaveViews = new List<CallWaveBtnViewModel>();
            List<HandleSingleCallWaveShowTooltip> handleSingleCallWaveShowTooltips = new List<HandleSingleCallWaveShowTooltip>();
            RouteSetConfig.RouteSet lineRouteSet = _routeSetConfig.LoadFromConfig(_currentStageId);
            int lineCount = lineRouteSet.RouteLines.Count;
            for (int i = 0; i < _currentSingleRouteComposite.Count; i++)
            {
                // If route set config < total active routeSet on map
                if (i >= lineCount)
                {
                    _currentSingleRouteComposite[i].SingleLineRenderer.gameObject.SetActive(false);
                    continue;
                }

                // Set call wave button pos
                _currentSingleRouteComposite[i].CallWaveBtnViewModel.transform.position = lineRouteSet.RouteLines[i].CallwaveButtonPos;
                callWaveViews.Add(_currentSingleRouteComposite[i].CallWaveBtnViewModel);
                
                handleSingleCallWaveShowTooltips.Add(_currentSingleRouteComposite[i].HandleSingleCallWaveShowTooltip);
                handleSingleCallWaveShowTooltips[i].ECallWaveUnitPreviewDirectionType = _currentSingleRouteComposite[i].HandleSingleCallWaveShowTooltip.ECallWaveUnitPreviewDirectionType;
                // Check if this lineRender available to save
                _currentSingleRouteComposite[i].SingleLineRenderer.gameObject.SetActive(true);

                // Init lineRender max space
                _currentSingleRouteComposite[i].SingleLineRenderer.positionCount = lineRouteSet.RouteLines[i].PointSet.Count;

                // Set position at each point in lineRender
                // z always zero
                for (int j = 0; j < lineRouteSet.RouteLines[i].PointSet.Count; j++)
                    _currentSingleRouteComposite[i].SingleLineRenderer.SetPosition(j,
                        new Vector3(
                            lineRouteSet.RouteLines[i].PointSet[j].x,
                            lineRouteSet.RouteLines[i].PointSet[j].y,
                            0));
            }
            BuildActiveRoute();
            _callWaveListViewModel.Setup(callWaveViews, handleSingleCallWaveShowTooltips, _currentStageId);
        }
        private void BuildActiveRoute()
        {
            _activeSingleRouteComposite = new List<SingleRoute>();
            foreach (var tSingleRoute in _currentSingleRouteComposite)
            {
                if (tSingleRoute.gameObject.activeSelf)
                    _activeSingleRouteComposite.Add(tSingleRoute);
            }
        }
        private void GetNearestPosFromRoute(OnGetNearestPosFromRoutePayload payload)
        {
            float nearestDis = float.MaxValue;
            Vector3 res = Vector3.zero;
            foreach (SingleRoute routeLineRender in ActiveSingleRouteLineRenderers)
            {
                for (int i = 0; i < routeLineRender.SingleLineRenderer.positionCount; i++)
                {
                    float curDis = VectorUtility.Distance2dOfTwoPos(payload.PosInput, routeLineRender.SingleLineRenderer.GetPosition(i));
                    if (!(nearestDis > curDis))
                        continue;
                    nearestDis = curDis;
                    res = routeLineRender.SingleLineRenderer.GetPosition(i);

                }
            }
            payload.OnCalculateSuccess?.Invoke(res);
        }
        protected override void OnSetupNewGame(SetUpNewGamePayload setUpNewGamePayload)
        {
            _currentStageId = setUpNewGamePayload.StartStageComposite.StageId;
            LoadFromConfig();
        }
        protected override void OnResetGame(ResetGamePayload resetGamePayload)
        {
        }
    }
}
