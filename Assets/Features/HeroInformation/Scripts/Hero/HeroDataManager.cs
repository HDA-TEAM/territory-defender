
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroDataManager : SingletonBase<HeroDataManager> 
{
    // Convert data from HeroDataAsset (data tho) to Data HeroComposite to use
    [SerializeField] private HeroDataAsset _heroDataAsset;

    public List<HeroComposite> HeroComposites;
    private new void Awake()
    {
        // LoadData
        HeroComposites = new List<HeroComposite>();
        HeroComposites = LoadHeroData();
    }
    private List<HeroComposite> LoadHeroData()
    {
        List<HeroDataSO> listHeroDataSo = _heroDataAsset.GetAllHeroData();
        HeroComposites.Clear();
        
        foreach (var heroDataSo in listHeroDataSo)
        {
            HeroComposites.Add(
                new HeroComposite
                {
                    Name = heroDataSo._stats.GetInformation(InformationId.Name),
                    Level = heroDataSo._stats.GetStat(StatId.Level).ToString(""),
                    Hp = heroDataSo._stats.GetStat(StatId.MaxHeal).ToString(""),
                    Atk = heroDataSo._stats.GetStat(StatId.AttackDamage).ToString(""),
                    Def = heroDataSo._stats.GetStat(StatId.Armour).ToString(""),
                    Range = heroDataSo._stats.GetStat(StatId.AttackRange).ToString("F2"),
                    Avatar = heroDataSo._imageHero,
                    HeroChoose = heroDataSo._imageHeroChoose,
                    HeroOwned = heroDataSo._imageHeroOwned,
                   
                    Skills = heroDataSo._heroSkills.GetAllSkillData()
                }
            );
        }
        
        //Messenger.Default.Publish(new ListCompositePayload{Composites = _heroComposites.Cast<IComposite>().ToList()});
        // Provide data of heroes for ListHeroChooseViewModel
        GameEvents.UpdateListCompositeData(HeroComposites.Cast<IComposite>().ToList());

        return HeroComposites;
    }
}

