using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features.HeroInformation
{
    public class LevelSystem : MonoBehaviour
    {
        private int _level;
        private int _experience;
        private int _experienceToNextLevel;

        public LevelSystem()
        {
            _level = 0;
            _experience = 0;
            _experienceToNextLevel = 100;
        }

        public void AddExperience(int amount)
        {
            _experience += amount;
            if (_experience >= _experienceToNextLevel)
            {
                // Enough exp to level up
                _level++;
                _experience -= _experienceToNextLevel;
            }
        }

        public int GetLevelNumber()
        {
            return _level;
        }
    }
}

