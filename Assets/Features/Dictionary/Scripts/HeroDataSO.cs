using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "Hero", order = 2)]
public class HeroDataSO : ScriptableObject
{
    public string _heroName;
    public int _heroLevel;
    public Sprite _heroImage;
    [SerializeField] private Stats _stats;
}
