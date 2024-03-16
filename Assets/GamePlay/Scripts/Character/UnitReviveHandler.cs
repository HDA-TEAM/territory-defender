using System;

public class UnitReviveHandler : UnitBaseComponent
{
    private Action<UnitBase> _onRevive;

    public void SetupRevive(Action<UnitBase> onRevive)
    {
        _onRevive = onRevive;
    }
    public void OnDisable()
    {
        _onRevive?.Invoke(_unitBaseParent);
        _onRevive = null;
    }
}
