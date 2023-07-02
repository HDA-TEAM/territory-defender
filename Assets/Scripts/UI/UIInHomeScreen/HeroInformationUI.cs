using UnityEngine;

using UI.UIInHomeScreen;
public class HeroInformationUI : MonoBehaviour
{
    private HeroController _hero;

    private void Start()
    {
        _hero = gameObject.AddComponent<HeroController>();
    }

    public void HeroInformationLoad()
    {
        _hero.Load();
    }
}
