using Common.Loading.Scripts;
using GamePlay.Scripts.Data;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private MapDataConfig _mapDataConfig;
    [SerializeField] private SpriteRenderer _spriteRendererMap;
    public void SetUpNewGame(StartStageComposite startStageComposite)
    {
        _spriteRendererMap.sprite =_mapDataConfig.GeConfigByKey(startStageComposite.StageId).MapSprite;
    }
}
