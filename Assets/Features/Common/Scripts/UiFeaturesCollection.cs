using BrunoMikoski.ScriptableObjectCollections;
using UnityEngine;
using System.Collections.Generic;

namespace HDATeam.Features.Common
{
    [CreateAssetMenu(menuName = "ScriptableObject Collection/Collections/Create UiFeaturesCollection", fileName = "UiFeaturesCollection", order = 0)]
    public class UiFeaturesCollection : ScriptableObjectCollection<UiFeatures>
    {
    }
}
