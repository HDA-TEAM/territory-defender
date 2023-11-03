using UnityEngine;
using UnityEngine.Serialization;

public class GlobalValue: Singleton<GlobalValue> {
    public int _nextScene = 0;
}