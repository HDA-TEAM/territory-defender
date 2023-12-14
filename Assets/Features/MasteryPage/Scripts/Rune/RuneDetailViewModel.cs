
using UnityEngine;

public class RuneDetailViewModel : MonoBehaviour
{
    [SerializeField] private RuneDetailView _runeDetailView;
    [SerializeField] private ItemUpgradeRuneView _itemUpgradeRuneView;
    
    private ItemUpgradeRuneView _preSelectedItem;
    public void Setup(RuneComposite runeComposite)
    {
        _runeDetailView.gameObject.SetActive(true);
        _itemUpgradeRuneView.gameObject.SetActive(true);
        _runeDetailView.Setup(runeComposite);
        
        _itemUpgradeRuneView.Setup(runeComposite, OnSelectedUpgradeRuneItem);
    }
    
    public void StartSetup()
    {
        gameObject.SetActive(true);
    }
    
    private void OnSelectedUpgradeRuneItem(ItemUpgradeRuneView itemUpgradeRuneView)
    {
        _preSelectedItem = itemUpgradeRuneView;
        
        //Setup(_preSelectedItem.RuneComposite);
        Debug.Log("Upgrade rune....");
    }
}
