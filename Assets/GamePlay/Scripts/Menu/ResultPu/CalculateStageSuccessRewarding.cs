using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.GamePlay
{
    public class CalculateStageSuccessRewarding
    {
        public int GetStarsRewarding(int maxHealth, int curHealth)
        {
            List<IStageSuccessCondition> successConditions = new List<IStageSuccessCondition>
            {
                new UserHealthCondition(curHealth, maxHealth),
                new UserHealthCondition(curHealth, maxHealth * 3 / 4),
                new UserHealthCondition(curHealth, maxHealth / 2),
            };

            int totalStarsRewarding = 0;
            foreach (var successCondition in successConditions)
            {
                if (successCondition.IsPassCondition())
                {
                    totalStarsRewarding += 1;
                }
            }
            return totalStarsRewarding;
        }
    }

    public interface IStageSuccessCondition
    {
        public bool IsPassCondition();
    }
    public abstract class StageSuccessCondition : IStageSuccessCondition
    {
        public virtual bool IsPassCondition()
        {
            return false;
        }
    }
    public class UserHealthCondition : StageSuccessCondition
    {
        private readonly int _curHealth;
        private readonly int _minPassHealth;
        public UserHealthCondition(int curHealth, int minPassHealth)
        {
            _curHealth = curHealth;
            _minPassHealth = minPassHealth;
        }
        public override bool IsPassCondition()
        {
            Debug.Log(_curHealth + " " + _minPassHealth);
            return _curHealth >= _minPassHealth;
        }
    }
}
