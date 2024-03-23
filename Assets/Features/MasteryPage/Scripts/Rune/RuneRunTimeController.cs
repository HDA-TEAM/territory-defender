using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Features.MasteryPage.Scripts.Rune
{
    [CreateAssetMenu(fileName = "RuneController", menuName = "MasteryPage/RuneController")]
    public class RuneRunTimeController : ScriptableObject
    {
        [SerializeField] private Effect _effects;

        public List<int> GetRuneEffect(RuneComposite rune)
        {
            var effectList = new List<int>();
            // This assumes each rune affects only one stat for simplicity
            foreach (var data in rune.Effects)
            {
                effectList.Add(_effects.GetEffectValue(data));
            }
        
            return effectList;
        }
        
        public void ApplyRuneEffects(RuneComposite rune, UnitId.Tower tower)
        {
            // foreach (Effect effect in _effects)
            // {
            //     // Assuming RuneComposite has a way to determine the relevant EffectStats
            //     EffectStats stat = effect.GetEffect(rune.RuneId);
            //     int effectValue = effect.GetEffectValue(stat);
            //
            //     // Apply the effect based on the stat
            //     switch (stat)
            //     {
            //         case EffectStats.Cooldown:
            //             // tower.Cooldown -= effectValue; // Assuming 'effectValue' is a reduction
            //             break;
            //         case EffectStats.Damage:
            //             // tower.Damage += effectValue;
            //             break;
            //         case EffectStats.Crit:
            //             // tower.CritChance += effectValue;
            //             break;
            //         // Handle other stats
            //     }
            // }
        }
    }

    public struct EffectComposite
    {
        public EffectId EffectId;
        public int AdditionalStats;
    }
}
