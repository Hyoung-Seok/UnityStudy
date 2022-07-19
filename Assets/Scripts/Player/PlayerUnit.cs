using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public Rigidbody Rigidbody;
    public Animator Animator;
    
    public float Speed = 9.0f;
    public float RotationSpeed = 8.0f;
    public float CurrentSpeed = 0.0f;
    public float AccelerateSpeed = 40.0f;
    public float TiltWeight = 0.0f;
    
    private PlayerUnitState_Move _moveState;

    private void Start()
    {
        _moveState = new PlayerUnitState_Move(this);
        ChangeState(_moveState);
    }
}
