using TMPro;
using UnityEngine;


public struct StageResource
{
    public int CurLife;
    public int TotalCoin;
    public int CurWaveCount;
    public int MaxWaveCount;
}
public class StageResourceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtUserLife;
    [SerializeField] private TextMeshProUGUI _txtTotalCoin;
    [SerializeField] private TextMeshProUGUI _txtWaveCount;
    
    public void Setup(StageResource stageResource)
    {
        _txtUserLife.text = stageResource.CurLife.ToString();
        _txtTotalCoin.text = stageResource.TotalCoin.ToString();
        _txtWaveCount.text = WaveCountStringFormat(stageResource.CurWaveCount, stageResource.MaxWaveCount);
    }
    private string WaveCountStringFormat(int curWaveCount, int maxWaveCount)
    {
        return $"{curWaveCount}/{maxWaveCount}";
    }
}
