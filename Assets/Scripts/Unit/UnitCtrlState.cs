using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitCtrlState 
{
    protected Unit _owner;

    public UnitCtrlState(Unit unit)
    {
        _owner = unit;
    }

    public abstract void Enter();

    public abstract void OnUpdate();

    public abstract void OnLateUpdate();

    public abstract void OnFixedUpdate();

    public abstract void Exit();
}
