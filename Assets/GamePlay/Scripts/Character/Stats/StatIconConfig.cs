using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Character.StateMachine.Stats
{
    public class StatIconConfig : ScriptableObject
    {
        [SerializeField] [SerializedDictionary("StatId","StatIcon")]
        private SerializedDictionary<StatId,Sprite> _statIcon;

        public Sprite GetStatIcon(StatId statId)
        {
            if (_statIcon.TryGetValue(statId, out Sprite res))
                return res;
            Debug.LogError($"No stat value found for key {statId} on {this.name}");
            return null;
        }
    }
}
