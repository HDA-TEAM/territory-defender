using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "Hero", order = 2)]
public class HeroDataSO : ScriptableObject
{
    public string _heroName;
    public int _heroLevel;
    public int _heroHp;
    public int _heroAtk;
    public int _heroDef;
    public float _heroRange;
    public Sprite _heroImage;
    //public 
    [SerializeField] private Stats _stats;
}
