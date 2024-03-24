using System.Collections.Generic;
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
            
        }
    }

    public struct EffectComposite
    {
        public EffectId EffectId;
        public int AdditionalStats;
    }
}
