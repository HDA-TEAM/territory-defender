using UnityEngine;
public interface SetupHeroViewMode
{
    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode)
    {
    } 
}
public class HeroSkinView : MonoBehaviour , SetupHeroViewMode
{
    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode)
    {
        
    }
}
