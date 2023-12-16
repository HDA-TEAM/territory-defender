
using UnityEngine;

[CreateAssetMenu(fileName = "StarDataAsset", menuName = "ScriptableObject/DataAsset/StarDataAsset")]
public class StarDataAsset : ScriptableObject
{
    [SerializeField] private StarSO _starS0;
    
    public void UpdateStarData(float starNumber)
    {
        _starS0._starNumber -= starNumber;
        Debug.Log("Subtract star");
    }
    
    public float GetStarNumber()
    {
        return _starS0._starNumber;
    }
}

