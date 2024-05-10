using GamePlay.Scripts.Tower.TowerKIT.TowerTools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT
{
    public class TowerToolController : MonoBehaviour
    {
        [SerializeField] private TowerKit _towerKit;
        [SerializeField] private List<TowerToolBase> _towerToolBases;
        [SerializeField] private List<ConfirmHandle> _confirmHandles;
        public Object CurrentConfirmHandle;
        private Object _preConfirmHandle;
        private void Reset()
        {
            _towerToolBases.Clear();
            _confirmHandles.Clear();
            _towerToolBases = gameObject.GetComponentsInChildren<TowerToolBase>().ToList();
            foreach (TowerToolBase towerToolBase in _towerToolBases)
                towerToolBase.Reset();
            _confirmHandles = gameObject.GetComponentsInChildren<ConfirmHandle>().ToList();
        }
        private void Awake()
        {
            foreach (var confirmHandle in _confirmHandles)
                confirmHandle.SetUpSelected(SetCurrentSelectedConfirm);
            foreach (var towerToolBase in _towerToolBases)
                towerToolBase.SetUp(_towerKit);
        }
        private void SetCurrentSelectedConfirm(Object confirmHandle)
        {
            if (_preConfirmHandle != null && _preConfirmHandle != confirmHandle)
            {
                ((ConfirmHandle)_preConfirmHandle).ResetToDefault();
            }
            CurrentConfirmHandle = confirmHandle;
            _preConfirmHandle = CurrentConfirmHandle;
        }

    }
}
