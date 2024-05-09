using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Features.MasteryPage.Scripts.Rune
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
        public RuneId GetRuneId() => _runeId;

    }
}
