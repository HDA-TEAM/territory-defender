using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum ESkillId
{
    SummonElephant = 1,
    TurnTheWhip = 2,
}

[CreateAssetMenu(fileName = "SkillDataAsset", menuName = "ScriptableObject/DataAsset/SkillsDataAsset")]
public class SkillsDataAsset : ScriptableObject
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
