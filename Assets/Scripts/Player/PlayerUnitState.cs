using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerUnitState : UnitCtrlState
{
    protected new PlayerUnit _owner;

    public PlayerUnitState(PlayerUnit unit) : base(unit)
    {
        _owner = unit;
    }
}
