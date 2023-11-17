using System;
using System.Collections.Generic;
using Features.Talent.Hero;
using Features.Talent.Tower;
using Unity.VisualScripting;
using UnityEngine;

namespace Features.Talent
{
    public class TalentUpgrade : MonoBehaviour
    {
        // Upgrade talent for a specific hero type
        public void UpgradeHeroTalent(HeroTypeEnum heroType, CharacterUnitConfig unitConfig)
        {
            IHero hero = HeroFactory.CreateHero(heroType);
            hero.UpgradeProperty(unitConfig);
        }
        
        // Upgrade talent for a specific tower type
        public void UpgradeTowerTalent(TowerTypeEnum towerType, TowerUnitConfig unitConfig)
        {
            ITower tower = TowerFactory.CreateTower(towerType);
            tower.UpgradeProperty(unitConfig);
        }
        
        // Upgrade talent for a hero skill
        public void UpgradeHeroSkill(SkillConfig skillConfig, IHeroSkill heroSkill)
        {
            heroSkill.UpgradeLevelSkill(skillConfig);
        }
        
        // Upgrade talent for a tower skill
        public void UpgradeTowerSkill(SkillConfig skillConfig, ITowerSkill towerSkill)
        {
            towerSkill.UpgradeLevelSkill(skillConfig);
        }
    }
}