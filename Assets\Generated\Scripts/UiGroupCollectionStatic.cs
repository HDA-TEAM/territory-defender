//  Automatically generated
//

using BrunoMikoski.ScriptableObjectCollections;
using BrunoMikoski.UIManager;
using System.Collections.Generic;
using System;

namespace BrunoMikoski.UIManager
{
    public partial class UIGroup
    {
        private static bool hasCachedValues;
        private static UIGroupCollection values;
        
        private static bool hasCachedMain;
        private static BrunoMikoski.UIManager.UIGroup cachedMain;
        
        public static BrunoMikoski.UIManager.UIGroupCollection Values
        {
            get
            {
                if (!hasCachedValues)
                    hasCachedValues = CollectionsRegistry.Instance.TryGetCollectionByGUID(new LongGuid(5470617374342625789, -6487955789764117348), out values);
                return values;
            }
        }
        
        
        public static BrunoMikoski.UIManager.UIGroup Main
        {
            get
            {
                if (!hasCachedMain)
                    hasCachedMain = Values.TryGetItemByGUID(new LongGuid(5760426473656594190, -4641576356034238561), out cachedMain);
                return cachedMain;
            }
        }
        
        
    }
}
