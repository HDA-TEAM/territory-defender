using System.Collections.Generic;
using UnityEngine;

public class ListRuneViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemRuneView> _itemRuneViews;
    [SerializeField] private RuneDetailView _runeDetailView;
    [SerializeField] private ItemUpgradeRuneView _itemUpgradeRuneView;
    [SerializeField] private StarView _starView;

    [Header("Data"), Space(12)] 
    [SerializeField] private RuneDataAsset _runeDataAsset;
    [SerializeField] private StarDataAsset _starDataAsset;
    
    // Internal
    private List<RuneComposite> _runeComposites;
    private StarComposite _starComposite;
    private RuneDataSO _preRuneDataSo;
    private ItemRuneView _preSelectedItem;
    private ItemUpgradeRuneView _preSelectedUpgradeRuneView;
    private float _starNumber;
    
    private float _valueCastStringToInt;
    private void Awake()
    {
        _runeComposites = new List<RuneComposite>();
        _starComposite = new StarComposite();
        //_starNumber = _starView.GetStarNumber();
        UpdateData();
    }
    
    private void Start()
    {
        if (_runeDataAsset != null)
            _runeDataAsset._onDataUpdated += UpdateData;

        if (_starView != null)
            _starView._onDataUpdated += UpdateData;
    }
    
    private void UpdateData()
    {
        _runeComposites.Clear();
        
        // Load Rune data
        List<RuneDataSO> listRuneDataSo = _runeDataAsset.GetAllRuneData();
        foreach (var runeDataSo in listRuneDataSo)
        {
            _runeComposites.Add(
                new RuneComposite
                {
                    RuneId = runeDataSo._runeId,
                    TypeName = runeDataSo._name,
                    AdditionalValue =  runeDataSo._additionalValue,
                    Operate = runeDataSo._operate,
                    CurrentStacks = runeDataSo._currentStacks,
                    Stacks = runeDataSo._stacks,
                    StarNeedToUpgrade = runeDataSo._starNeedToUpgrade,
                    AvatarSelected = runeDataSo._avatarSelected,
                    AvatarStarted = runeDataSo._avatarStarted
                }
            );
        }
        
        // Load Star data
        _starComposite.StarNumber = _starDataAsset.GetStarNumber();
        
        UpdateView();
    }

    private void UpdateView()
    {
        for (int i = 0; i < _itemRuneViews.Count; i++)
        {
            // Setup rune view
            _itemRuneViews[i].SetRuneStacks(_runeComposites[i]);
            // Setup star view
            _starView.Setup(_starComposite);
            
            // Rune avatar logic
            _itemRuneViews[i].SetAvatarRune(_runeComposites[i].CurrentStacks > 0 ? _runeComposites[i].AvatarSelected : _runeComposites[i].AvatarStarted);
            
            // Selected rune setup
            _itemRuneViews[i].Setup(_runeComposites[i], OnSelectedRuneItem);
        }
    }

    private void OnSelectedRuneItem(ItemRuneView itemRuneView)
    {
        if (_preSelectedItem == null)
        {
            _itemUpgradeRuneView.gameObject.SetActive(true);
            _runeDetailView.gameObject.SetActive(true);
        }
        _preSelectedItem = itemRuneView;
        _runeDetailView.Setup(_preSelectedItem.RuneComposite);
        _itemUpgradeRuneView.Setup(_preSelectedItem.RuneComposite, OnSelectedUpgradeRuneItem);
    }
    
    private void OnSelectedUpgradeRuneItem(ItemUpgradeRuneView itemUpgradeRuneView)
    {
        if (itemUpgradeRuneView == null || _preSelectedItem == null || _runeDataAsset == null || _starView == null)
        {
            Debug.LogError("One or more required objects are null.");
            return;
        }
        
        _preSelectedUpgradeRuneView = itemUpgradeRuneView;
        
        // Update stacks data
        if (_preSelectedItem.RuneComposite.CurrentStacks < _preSelectedItem.RuneComposite.Stacks && _starDataAsset.GetStarNumber() > 0)
        {
            _preRuneDataSo = _runeDataAsset.GetRune(itemUpgradeRuneView.RuneComposite.RuneId);

            if (_preRuneDataSo != null)
            {
                _preRuneDataSo._currentStacks++;
            
                // Subtract star number
                _starDataAsset.UpdateStarData(_preRuneDataSo._starNeedToUpgrade);
            
                // Update rune data
                _runeDataAsset.RuneUpdate(_preRuneDataSo);
                Debug.Log("Upgrade rune successful....");
            }
        }
        else
        {
            Debug.Log("Upgrade rune fail");
        }
        
        Debug.Log("Upgrade rune....");
    }
}

public struct RuneComposite
{
    public RuneId RuneId;
    public string TypeName;
    public string AdditionalValue;
    public string Operate;
    public float CurrentStacks;
    public float Stacks;
    public float StarNeedToUpgrade;
    public Sprite AvatarSelected;
    public Sprite AvatarStarted;
}

public struct StarComposite
{
    public float StarNumber;
}
