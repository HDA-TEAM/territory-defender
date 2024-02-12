using System;

public static class GameEvents
{
    public static event Action<StageComposite> OnStageSelected;

    public static void SelectStage(StageComposite stage)
    {
        OnStageSelected?.Invoke(stage);
    }
}
