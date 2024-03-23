using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Features.MasteryPage.Scripts.Rune;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Rune", menuName = "Rune", order = 2)]
public class RuneSO : ScriptableObject
{
    [SerializeField]
    private RuneId _runeId;
    
    public string _name;
    public int _maxLevel;
    [JsonIgnore]
    public Sprite _avatarSelected;
    [JsonIgnore]
    public Sprite _avatarStarted;

    public List<EffectId> _effects;
    public RuneId GetRuneId() => _runeId;

}
