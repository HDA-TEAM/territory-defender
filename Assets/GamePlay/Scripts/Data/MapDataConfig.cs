using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace GamePlay.Scripts.Data
{
    [Serializable,Preserve]
    public struct MapDataComposite
    {
        public Sprite MapSprite;
    }
    [CreateAssetMenu(fileName = "MapDataConfig", menuName = "ScriptableObject/Configs/MapDataConfig")]
    public class MapDataConfig : DataConfigBase<StageId,MapDataComposite>
    {
    }
}