using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace GamePlay.Scripts.Character.Stats
{
    [CreateAssetMenu(fileName = "StatIconConfig", menuName = "ScriptableObject/StatIconConfig")]
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
