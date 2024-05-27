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
                lineRender._subLineRendererSet.SetMultiplier(0f);
            }
        }

        private void SaveToConfig()
        {
            RouteSetConfig.RouteSet routeSet = new RouteSetConfig.RouteSet
            {
                MainRouteLines = new List<RouteSetConfig.RouteLine>(),
            };

            foreach (SingleRoute mainRoute in _currentSingleRouteComposite)
            {
                // Check if lineRender want to save
                if (!mainRoute._subLineRendererSet.gameObject.activeSelf)
                    continue;

                routeSet.ECallWaveUnitPreviewDirectionType = mainRoute.HandleSingleCallWaveShowTooltip.ECallWaveUnitPreviewDirectionType;

                // add RouteSetConfig.subRouteLines
                List<RouteSetConfig.SubRouteLine> subRouteLines = new List<RouteSetConfig.SubRouteLine>();
                foreach (LineRenderer line in mainRoute._subLineRendererSet.GetActiveSubLine())
                {
                    List<Vector3> subLine = new List<Vector3>();
                    for (int i = 0; i < line.positionCount; i++)
                        subLine.Add(line.GetPosition(i));

                    subRouteLines.Add(new RouteSetConfig.SubRouteLine
                    {
                        PointSet = subLine,
                    });
                }

                // add RouteSetConfig.RouteLine
                routeSet.MainRouteLines.Add(new RouteSetConfig.RouteLine
                {
                    CallwaveButtonPos = mainRoute.CallWaveBtnViewModel.transform.position,
                    SubRouteLines = subRouteLines,
                });
            }
            _routeSetConfig.SaveToConfig(routeSet, _currentStageId);
        }

        private void LoadFromConfig()
        {
            List<CallWaveBtnViewModel> callWaveViews = new List<CallWaveBtnViewModel>();
            List<HandleSingleCallWaveShowTooltip> handleSingleCallWaveShowTooltips = new List<HandleSingleCallWaveShowTooltip>();
            RouteSetConfig.RouteSet lineRouteSet = _routeSetConfig.LoadFromConfig(_currentStageId);
            int mainLineCount = lineRouteSet.MainRouteLines.Count;
            for (int i = 0; i < _currentSingleRouteComposite.Count; i++)
            {
                // If route set config < total active routeSet on map
                if (i >= mainLineCount)
                {
                    _currentSingleRouteComposite[i]._subLineRendererSet.gameObject.SetActive(false);
                    continue;
                }

                // Set call wave button pos
                _currentSingleRouteComposite[i].CallWaveBtnViewModel.transform.position = lineRouteSet.MainRouteLines[i].CallwaveButtonPos;
                callWaveViews.Add(_currentSingleRouteComposite[i].CallWaveBtnViewModel);

                handleSingleCallWaveShowTooltips.Add(_currentSingleRouteComposite[i].HandleSingleCallWaveShowTooltip);
                handleSingleCallWaveShowTooltips[i].ECallWaveUnitPreviewDirectionType = _currentSingleRouteComposite[i].HandleSingleCallWaveShowTooltip.ECallWaveUnitPreviewDirectionType;

                // Check if this lineRender available to save
                _currentSingleRouteComposite[i]._subLineRendererSet.gameObject.SetActive(true);

                for (int j = 0; j < lineRouteSet.MainRouteLines[i].SubRouteLines.Count; j++)
                {
                    // Init lineRender max space
                    _currentSingleRouteComposite[i]._subLineRendererSet.CurSubLineRenderers[j].positionCount = lineRouteSet.MainRouteLines[i].SubRouteLines[j].PointSet.Count;
                    // Set position at each point in sub lineRender
                    // z always zero
                    for (int k = 0; k < _currentSingleRouteComposite[i]._subLineRendererSet.CurSubLineRenderers[j].positionCount; k++)
                    {
                        _currentSingleRouteComposite[i]._subLineRendererSet.CurSubLineRenderers[j].SetPosition(k, new Vector3(
                            lineRouteSet.MainRouteLines[i].SubRouteLines[j].PointSet[k].x,
                            lineRouteSet.MainRouteLines[i].SubRouteLines[j].PointSet[k].y,
                            0));
                    }
                }
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
                LineRenderer curCenterLine = routeLineRender._subLineRendererSet.GetCenterSubLineRenderer();
                for (int i = 0; i < curCenterLine.positionCount; i++)
                {
                    Vector3 curPos = curCenterLine.GetPosition(i);
                    float curDis = VectorUtility.Distance2dOfTwoPos(payload.PosInput, curPos);
                    if (!(nearestDis > curDis))
                        continue;
                    nearestDis = curDis;
                    res = curPos;
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
