using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected UnitCtrlState _currentState = null;
    protected UnitCtrlState _prevState = null;

    public UnitCtrlState ChangeState(UnitCtrlState changedState)
    {
        if(_currentState != null)
        {
            _currentState.Exit();
        }
        _prevState = _currentState;

        _currentState = changedState;
        _currentState.Enter();

        return _currentState;
    }

    public UnitCtrlState ChangeToPrevState()
    {
        if(_currentState != null)
        {
            _currentState.Exit();
        }
        var temp = _prevState;
        _prevState = _currentState;

        _currentState = temp;
        if(_currentState != null)
        {
            _currentState.Enter();
        }

        return _currentState;
    }

    private void Update()
    {
        if(_currentState != null)
        {
            _currentState.OnUpdate();
        }
    }

    private void LateUpdate()
    {
        if (_currentState != null)
        {
            _currentState.OnLateUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (_currentState != null)
        {
            _currentState.OnFixedUpdate();
        }
    }
}
