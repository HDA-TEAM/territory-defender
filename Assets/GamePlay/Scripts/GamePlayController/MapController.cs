using Common.Loading.Scripts;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using UnityEngine;

public class MapController : GamePlayMainFlowBase
{
    [SerializeField] private MapDataConfig _mapDataConfig;
    [SerializeField] private SpriteRenderer _spriteRendererMap;
    protected override void OnSetupNewGame(SetUpNewGamePayload setUpNewGamePayload)
    {
        _spriteRendererMap.sprite =_mapDataConfig.GeConfigByKey(setUpNewGamePayload.StartStageComposite.StageId).MapSprite;
    }
    protected override void OnResetGame(ResetGamePayload resetGamePayload)
    {
    }
}
