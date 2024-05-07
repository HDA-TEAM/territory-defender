using Common.Loading.Scripts;
using Common.Scripts.Utilities;
using CustomInspector;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using GamePlay.Scripts.Route;
using GamePlay.Scripts.Route.PreviewCallWaveTooltip;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.GamePlayController
{
    public class RouteSetController : GamePlaySingletonBase<RouteSetController>
    {
        [Button("SaveToConfig")]
        [Button("LoadFromConfig")]
        [SerializeField] private StageId _currentStageId;

        [SerializeField] private List<SingleRoute> _currentSingleRouteComposite = new List<SingleRoute>();
        [SerializeField] private RouteSetConfig _routeSetConfig;
        [SerializeField] private CallWaveListViewModel _callWaveListViewModel;
        public List<SingleRoute> CurrentSingleRouteLineRenderers
        {
            get
            {
                return _currentSingleRouteComposite;
            }
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
            _callWaveListViewModel.Setup(callWaveViews, handleSingleCallWaveShowTooltips, _currentStageId);
        }
        public Vector3 GetNearestPosFromRoute(Vector3 posA)
        {
            float nearestDis = float.MaxValue;
            Vector3 res = Vector3.zero;
            foreach (var routeLineRender in _currentSingleRouteComposite)
            {
                for (int i = 0; i < routeLineRender.SingleLineRenderer.positionCount; i++)
                {
                    var curDis = VectorUtility.Distance2dOfTwoPos(posA, routeLineRender.SingleLineRenderer.GetPosition(i));
                    if (nearestDis > curDis)
                    {
                        nearestDis = curDis;
                        res = routeLineRender.SingleLineRenderer.GetPosition(i);
                    }

                }
            }
            return res;
        }

        public override void SetUpNewGame(StartStageComposite startStageComposite)
        {
            _currentStageId = startStageComposite.StageId;
            LoadFromConfig();
        }
        public override void ResetGame()
        {

        }
    }
}
