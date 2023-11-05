//  Automatically generated
//

using BrunoMikoski.ScriptableObjectCollections;
using BrunoMikoski.UIManager;
using System.Collections.Generic;
using System;

namespace BrunoMikoski.UIManager
{
    public class UiWindowCollectionStatic
    {
        private static bool hasCachedValues;
        private static UIWindowCollection values;
        
        private static bool hasCachedHomeMenuScreen;
        private static BrunoMikoski.UIManager.PrefabUIWindow cachedHomeMenuScreen;
        private static bool hasCachedHeroesScreen;
        private static BrunoMikoski.UIManager.PrefabUIWindow cachedHeroesScreen;
        private static bool hasCachedDictionaryScreen;
        private static BrunoMikoski.UIManager.PrefabUIWindow cachedDictionaryScreen;
        private static bool hasCachedHistoryScreen;
        private static BrunoMikoski.UIManager.PrefabUIWindow cachedHistoryScreen;
        
        public static BrunoMikoski.UIManager.UIWindowCollection Values
        {
            get
            {
                if (!hasCachedValues)
                    hasCachedValues = CollectionsRegistry.Instance.TryGetCollectionByGUID(new LongGuid(5661902693059371451, 5179766507890645133), out values);
                return values;
            }
        }
        
        
        public static BrunoMikoski.UIManager.PrefabUIWindow HomeMenuScreen
        {
            get
            {
                if (!hasCachedHomeMenuScreen)
                    hasCachedHomeMenuScreen = Values.TryGetItemByGUID(new LongGuid(5078061919637549043, -5364199186704963455), out cachedHomeMenuScreen);
                return cachedHomeMenuScreen;
            }
        }
        
        public static BrunoMikoski.UIManager.PrefabUIWindow HeroesScreen
        {
            get
            {
                if (!hasCachedHeroesScreen)
                    hasCachedHeroesScreen = Values.TryGetItemByGUID(new LongGuid(4788013222594754729, -8140499308280744519), out cachedHeroesScreen);
                return cachedHeroesScreen;
            }
        }
        
        public static BrunoMikoski.UIManager.PrefabUIWindow DictionaryScreen
        {
            get
            {
                if (!hasCachedDictionaryScreen)
                    hasCachedDictionaryScreen = Values.TryGetItemByGUID(new LongGuid(4828414472837802015, 542265694455116172), out cachedDictionaryScreen);
                return cachedDictionaryScreen;
            }
        }
        
        public static BrunoMikoski.UIManager.PrefabUIWindow HistoryScreen
        {
            get
            {
                if (!hasCachedHistoryScreen)
                    hasCachedHistoryScreen = Values.TryGetItemByGUID(new LongGuid(5665893544405310904, 3215892205131976629), out cachedHistoryScreen);
                return cachedHistoryScreen;
            }
        }
        
        
    }
}
