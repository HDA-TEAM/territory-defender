using GamePlay.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;

namespace GamePlay.Scripts.Datas.StageSpawning
{
    public static class StageParseDataAdapter
    {
        public static List<StageParseData> ParseDataToJson(List<SingleStageSpawningConfig> singleStages)
        {
            List<StageParseData> stageParseValue = new List<StageParseData>();
            foreach (var singleStage in singleStages)
            {
                List<SingleStageSpawningConfig.WaveSpawning> curWavesSpawning = singleStage.WavesSpawning;
                for (int i = 0; i < curWavesSpawning.Count; i++)
                {
                    foreach (var singleGroup in curWavesSpawning[i].GroupsSpawning)
                    {
                        stageParseValue.Add(
                            new StageParseData
                            {
                                StageId = singleStage.StageId,
                                WaveIndex = i + 1,
                                StartSpawning = singleGroup.StartSpawning,
                                ObjectSpawning = singleGroup.ObjectSpawn,
                                RouteId = singleGroup.RouteId,
                                NumberSpawning = singleGroup.NumberSpawning
                            }
                        );
                    }
                }
            }
            return stageParseValue;
        }
       
        public static List<SingleStageSpawningConfig> ParseJsonToData(List<StageParseData> stageParseDataList)
        {
            List<SingleStageSpawningConfig> singleStages = new List<SingleStageSpawningConfig>();

            // Group the StageParseData by StageId
            var stageGroups = stageParseDataList.GroupBy(data => data.StageId);

            foreach (var stageGroup in stageGroups)
            {
                SingleStageSpawningConfig singleStage = new SingleStageSpawningConfig
                {
                    StageId = stageGroup.Key,
                    WavesSpawning = new List<SingleStageSpawningConfig.WaveSpawning>()
                };

                // Group the StageParseData within each stage group by WaveIndex
                var waveGroups = stageGroup.GroupBy(data => data.WaveIndex);

                foreach (var waveGroup in waveGroups)
                {
                    SingleStageSpawningConfig.WaveSpawning waveSpawning = new SingleStageSpawningConfig.WaveSpawning
                    {
                        GroupsSpawning = new List<SingleStageSpawningConfig.GroupSpawning>()
                    };

                    // Convert StageParseData to GroupSpawning
                    foreach (var parseData in waveGroup)
                    {
                        SingleStageSpawningConfig.GroupSpawning groupSpawning = new SingleStageSpawningConfig.GroupSpawning
                        {
                            StartSpawning = parseData.StartSpawning,
                            ObjectSpawn = parseData.ObjectSpawning,
                            RouteId = parseData.RouteId,
                            NumberSpawning = parseData.NumberSpawning
                        };

                        waveSpawning.GroupsSpawning.Add(groupSpawning);
                    }

                    // Add the WaveSpawning to the single stage
                    singleStage.WavesSpawning.Add(waveSpawning);
                }

                // Add the single stage to the list
                singleStages.Add(singleStage);
            }

            return singleStages;
        }
    }
    [Serializable,Preserve]
    public struct StageParseData
    {
        public StageId StageId;
        public int WaveIndex;
        public float StartSpawning;
        public UnitId.Enemy ObjectSpawning;
        public int RouteId;
        public int NumberSpawning;
    }
}
