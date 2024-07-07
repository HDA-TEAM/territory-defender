using Common.Scripts.Data.DataAsset;
using Features.MasteryPage.Scripts.Rune;
using GamePlay.Scripts.Character.Stats;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Data
{
    [CreateAssetMenu(fileName = "Rune", menuName = "ScriptableObject/Data/Rune")]
    public class RuneDataSo : ScriptableObject
    {
        [SerializeField]
        private RuneId _runeId;

        public string _name;
        public string Description;
        public int _maxLevel;
        public List<float> PowerUnits;
        [JsonIgnore]
        public Sprite _avatarSelected;
        [JsonIgnore]
        public Sprite _avatarStarted;

        public StatId StatId;
        
        [HideInInspector] public List<EffectId> _effects;
        public RuneId GetRuneId()
        {
            return _runeId;
        }
    }
}
