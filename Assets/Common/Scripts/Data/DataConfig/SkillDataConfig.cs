using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    public enum ESkillId
    {
        SummonElephant = 1,
        TurnTheWhip = 2,
        BrutalStrike = 11,
        FocusStrike = 12,
    }

    [CreateAssetMenu(fileName = "SkillDataConfig", menuName = "ScriptableObject/Config/SkillDataConfig")]
    public class SkillDataConfig : ScriptableObject
    {
        [SerializedDictionary("SkillId", "SkillDataSO")]
        [SerializeField] private SerializedDictionary<ESkillId, SkillDataSO> _skillDataDict =
            new SerializedDictionary<ESkillId, SkillDataSO>();

        public SkillDataSO GetSkillDataById(ESkillId skillId)
        {
            _skillDataDict.TryGetValue(skillId, out SkillDataSO skillDataSo);
            return skillDataSo;
        }

        public List<SkillDataSO> GetAllSkillData()
        {
            return _skillDataDict.Values.ToList();
        }
    }
}
