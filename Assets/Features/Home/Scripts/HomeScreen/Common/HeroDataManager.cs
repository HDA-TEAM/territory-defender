using Common.Scripts;
using Common.Scripts.Data;
using Common.Scripts.Data.DataAsset;
using GamePlay.Scripts.Character.Stats;
using System.Collections.Generic;
using Features.HeroInformation;
using UnityEngine;

public class HeroDataManager : SingletonBase<HeroDataManager>
{
    [Header("Data"), Space(12)]
    [SerializeField] private HeroDataAsset _heroDataAsset;
    public List<HeroComposite> HeroComposites { get; private set; }

    protected override void Awake()
    {
        base.Awake(); // Call the base to check singleton integrity
        LoadHeroData();
    }
    public void ReloadData()
    {
        LoadHeroData();
    }
    private void LoadHeroData()
    {
        // Ensure HeroComposites is initialized
        if (HeroComposites == null)
            HeroComposites = new List<HeroComposite>();
        
        else HeroComposites.Clear();
        
        if (_heroDataAsset == null)
            return;

        List<HeroDataSO> listHeroDataSo = _heroDataAsset.GetAllHeroData();

        
        foreach (var item in listHeroDataSo)
        {
            HeroComposites.Add(new HeroComposite
            {
                HeroId = item._heroId,
                Name = item._stats.GetInformation(InformationId.Name),
                Level = _heroDataAsset.GetHeroLevel(item._heroId).ToString(),
                Hp = item._stats.GetStat(StatId.MaxHeal).ToString(),
                Atk = item._stats.GetStat(StatId.AttackDamage).ToString(),
                Def = item._stats.GetStat(StatId.Armour).ToString(),
                Range = item._stats.GetStat(StatId.AttackRange).ToString("F2"),
                Avatar = item._imageHero,
                HeroChoose = item._imageHeroChoose,
                HeroOwned = item._imageHeroOwned,
                ActiveSkillId = item.ActiveSkillId,
                PassiveSkillId = item.PassiveSkillId,
            });
        }
    }
}

