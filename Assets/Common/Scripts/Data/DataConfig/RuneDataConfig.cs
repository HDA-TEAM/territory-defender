using Common.Scripts.Data.DataAsset;
using Features.MasteryPage.Scripts.Rune;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    [CreateAssetMenu(fileName = "Rune", menuName = "ScriptableObject/Data/Rune")]
    public class RuneDataConfig : ScriptableObject
    {
        [SerializeField]
        private RuneId _runeId;

        public string _name;
        public int _maxLevel;
        [JsonIgnore]
        public Sprite _avatarSelected;
        [JsonIgnore]
        public Sprite _avatarStarted;

        public List<EffectId> _effects;
        public RuneId GetRuneId()
        {
            return _runeId;
        }
    }
}
