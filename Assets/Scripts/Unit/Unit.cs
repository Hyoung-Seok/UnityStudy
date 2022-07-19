using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //상태 저장 변수
    protected UnitCtrlState _currentState = null;
    protected UnitCtrlState _prevState = null;
    
    // 상태 변경 함수
    public UnitCtrlState ChangeState(UnitCtrlState changedState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _prevState = _currentState;

        _currentState = changedState;
        _currentState.Enter();

        return _currentState;
    }
    
    //이전 상태로 갱신
    public UnitCtrlState ChangeToPrevState()
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        var temp = _prevState;
        _prevState = _currentState;

        _currentState = temp;
        if (_currentState != null)
        {
            _currentState.Enter();
        }

        return _currentState;
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnUpdate();
        }
    }

    private void LastUpdate()
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
