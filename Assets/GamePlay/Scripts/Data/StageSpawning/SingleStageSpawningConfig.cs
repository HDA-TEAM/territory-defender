using Common.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GamePlay.Scripts.Data.StageSpawning
{
    [Serializable]
    public struct SingleStageSpawningConfig
    {
        public StageId StageId;
        public List<WaveSpawning> WavesSpawning;
    
        [Serializable, Preserve]
        public struct WaveSpawning
        {
            public List<GroupSpawning> GroupsSpawning;
            public List<int> GetRoutesHasSpawningInThisWave()
            {
                List<int> routesHasSpawning = new List<int>();
                foreach (var groupSpawning in GroupsSpawning)
                {
                    if (!routesHasSpawning.Contains(groupSpawning.RouteId))
                    {
                        routesHasSpawning.Add(groupSpawning.RouteId);
                    }
                }
                return routesHasSpawning;
            }
        }

        [Serializable, Preserve]
        public struct GroupSpawning
        {
            public float StartSpawning;
            public UnitId.Enemy ObjectSpawn;
            public int RouteId;
            public int NumberSpawning;
        }

        public int GetTotalUnitsSpawning()
        {
            int total = 0;
            foreach (WaveSpawning waveSpawning in WavesSpawning)
            {
                foreach (GroupSpawning groupSpawning in waveSpawning.GroupsSpawning)
                {
                    total += groupSpawning.NumberSpawning;
                }
            }
            return total;
        }
    }
}
