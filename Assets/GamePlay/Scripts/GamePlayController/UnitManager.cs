using Common.Scripts;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.GamePlay;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.GamePlayController
{
    public enum BeingTargetCommand
    {
        None = 0,
        Block = 1,
        Attack = 2,
    }

    public struct OnSubscribeUnitManagerPayload
    {
        public UnitBase UnitSubscribe;
    }

    public struct OnUnSubscribeUnitManagerPayload
    {
        public UnitBase UnitUnSubscribe;
    }

    public struct OnUnitResetTargetPayload
    {
        public UnitBase Unit;
    }

    public class UnitManager : GamePlayMainFlowBase
    {
        [SerializeField] private List<UnitBase> _unitAllys = new List<UnitBase>();
        [SerializeField] private List<UnitBase> _unitEnemies = new List<UnitBase>();
        [SerializeField] private InGameStateController _inGameStateController;
        private readonly List<Action> _onSubscribeAction = new List<Action>();
        private readonly List<Action> _onUnSubscribeAction = new List<Action>();
        private readonly List<Action> _onUnitOutAction = new List<Action>();
        private List<UnitBase> _unitsNeed;
        private readonly List<UnitBase> _allys = new List<UnitBase>();
        public bool IsEmptyActiveEnemy { get; protected set; }
        protected override void Awake()
        {
            base.Awake();
            Messenger.Default.Subscribe<OnSubscribeUnitManagerPayload>(OnSubscribe);
            Messenger.Default.Subscribe<OnUnSubscribeUnitManagerPayload>(OnUnSubscribe);
            Messenger.Default.Subscribe<OnUnitResetTargetPayload>(OnResetTarget);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Messenger.Default.Unsubscribe<OnSubscribeUnitManagerPayload>(OnSubscribe);
            Messenger.Default.Unsubscribe<OnUnSubscribeUnitManagerPayload>(OnUnSubscribe);
            Messenger.Default.Unsubscribe<OnUnitResetTargetPayload>(OnResetTarget);
        }

        // A active and available unit can be subscribe
        private void OnSubscribe(OnSubscribeUnitManagerPayload payload) => _onSubscribeAction.Add(() => ExecuteOnSubscribe(payload.UnitSubscribe));
        private void ExecuteOnSubscribe(UnitBase unitBase)
        {
            if (UnitId.IsEnemySide(unitBase.UnitSide))
                _unitEnemies.Add(unitBase);
            else if (UnitId.IsAllySide(unitBase.UnitSide))
                _unitAllys.Add(unitBase);
        }
        // A de-active or unavailable unit will be UnSubscribe
        private void OnUnSubscribe(OnUnSubscribeUnitManagerPayload payload) => _onUnSubscribeAction.Add(() => ExecuteUnSubscribe(payload.UnitUnSubscribe));
        private void ExecuteUnSubscribe(UnitBase unitBase)
        {
            if (UnitId.IsEnemySide(unitBase.UnitSide) && _unitEnemies.Contains(unitBase))
                _unitEnemies.Remove(unitBase);
            else if (UnitId.IsAllySide(unitBase.UnitSide) && _unitAllys.Contains(unitBase))
                _unitAllys.Remove(unitBase);
            NotifyAllUnit(unitBase);
        }

        // Reset single Unit from outside handle
        private void OnResetTarget(OnUnitResetTargetPayload payload) => _onUnitOutAction.Add(() => NotifyAllUnit(payload.Unit));

        // Update units on map
        private void Update()
        {
            if (!_inGameStateController.IsGamePlaying)
                return;

            IsEmptyActiveEnemy = _unitEnemies.Count == 0;

            SynRuntimeAction();

            ClearUnavailableUnit();

            foreach (var enemy in _unitEnemies)
            {
                _unitsNeed = GetUnitsNeed(enemy.TargetSideNeeding()[0]);
                enemy.UnitController().UpdateStatus(_unitsNeed);
            }
            foreach (var ally in _unitAllys)
            {
                _unitsNeed = GetUnitsNeed(ally.TargetSideNeeding()[0]);
                ally.UnitController().UpdateStatus(_unitsNeed);
            }
        }
        private List<UnitBase> GetUnitsNeed(UnitId.BaseId baseId)
        {
            switch (baseId)
            {
                case UnitId.BaseId.Ally:
                    {
                        _allys.Clear();
                        foreach (var unit in _unitAllys)
                        {
                            if (unit.CharacterStateMachine().CharacterTroopBehaviourType != TroopBehaviourType.Tower)
                            {
                                _allys.Add(unit);
                            }
                        }
                        return _allys;
                    }
                case UnitId.BaseId.Enemy:
                    {
                        return _unitEnemies;
                    }

            }
            return new List<UnitBase>();
        }
        // A de-active or unavailable unit will be remove
        private void ClearUnavailableUnit()
        {
            _unitAllys.RemoveAll(unit => unit.gameObject.activeSelf == false);
            _unitEnemies.RemoveAll(unit => unit.gameObject.activeSelf == false);
        }

        // Ensuring always exist single unit updating flow 
        private void SynRuntimeAction()
        {
            foreach (var action in _onUnitOutAction)
                action?.Invoke();
            _onUnitOutAction.Clear();

            foreach (var action in _onSubscribeAction)
                action?.Invoke();
            _onSubscribeAction.Clear();

            foreach (var action in _onUnSubscribeAction)
                action?.Invoke();
            _onUnSubscribeAction.Clear();
        }

        // When having an unit out, we will notify for all unit to know that
        private void NotifyAllUnit(UnitBase unitOut)
        {
            UnitBase.OnTargetChangingComposite targetChangingComposite = new UnitBase.OnTargetChangingComposite();
            targetChangingComposite.SetDefault();
            foreach (var enemy in _unitEnemies)
            {
                if (enemy.CurrentTarget == unitOut)
                    enemy.OnTargetChanging?.Invoke(targetChangingComposite);
            }

            foreach (var ally in _unitAllys)
                if (ally.CurrentTarget == unitOut)
                    ally.OnTargetChanging?.Invoke(targetChangingComposite);

            unitOut.CurrentTarget = null;
            unitOut.OnTargetChanging?.Invoke(targetChangingComposite);
        }
        protected override void OnSetupNewGame(SetUpNewGamePayload setUpNewGamePayload)
        {
        }
        protected override void OnResetGame(ResetGamePayload resetGamePayload)
        {
        }
    }
}
