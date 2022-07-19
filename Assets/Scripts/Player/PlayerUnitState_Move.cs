using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitState_Move : PlayerUnitState
{
    private float _inputVertical;
    private float _inputHorizontal;
    private Vector3 _movement = Vector3.zero;
    private Vector3 _prevDir = Vector3.zero;
    private float _rotationAngle;

    private static int _speedKey = Animator.StringToHash("Speed");
    private static int _tiltKey = Animator.StringToHash("Tilt");

    public PlayerUnitState_Move(PlayerUnit unit) : base(unit)
    {

    }

    public override void Enter()
    {
        _prevDir = _owner.transform.forward;
    }

    public override void Exit()
    {
    }

    public override void OnFixedUpdate()
    {
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir;
        Transform ownerTf = _owner.transform;
        Vector3 lookDir;

        if(_inputVertical != 0.0f || _inputHorizontal != 0.0)
        {
            moveDir = (camForward * _inputVertical) + (camRight * _inputHorizontal);
            moveDir.Normalize();
            lookDir = moveDir;
        }
        else
        {
            moveDir = _prevDir;
            moveDir.Normalize();
            lookDir = moveDir;
        }

        lookDir = (_owner.CurrentSpeed != 0.0f && lookDir != Vector3.zero) ? lookDir : ownerTf.forward;
        Quaternion targetRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        ownerTf.rotation = Quaternion.Lerp(ownerTf.rotation, targetRotation, _owner.RotationSpeed * Time.fixedDeltaTime);

        if(_owner.CurrentSpeed == _owner.Speed && targetRotation != Quaternion.identity)
        {
            if(lookDir == Vector3.zero)
            {
                lookDir = ownerTf.forward;
            }

            _rotationAngle = (int)Quaternion.Angle(ownerTf.rotation, targetRotation);
            if(Vector3.Dot(Vector3.Cross(ownerTf.forward, lookDir), ownerTf.up) < 0.0f)
            {
                _rotationAngle = -_rotationAngle;
            }

            _owner.TiltWeight = Mathf.Lerp(_owner.TiltWeight, _rotationAngle, Time.fixedDeltaTime * _owner.RotationSpeed);
        }
        else
        {
            _owner.TiltWeight = Mathf.MoveTowards(_owner.TiltWeight, 0.0f, _owner.AccelerateSpeed * Time.fixedDeltaTime);
        }

        _movement = moveDir * _owner.CurrentSpeed;
        //ownerTf.transform.position += _movement * Time.fixedDeltaTime;
        _owner.Rigidbody.velocity = _movement;

        _prevDir = lookDir;
    }

    public override void OnLateUpdate()
    {
    }

    public override void OnUpdate()
    {
        _inputVertical = Input.GetAxis("Vertical");
        _inputHorizontal = Input.GetAxis("Horizontal");

        UpdateSpeed();

        var animator = _owner.Animator;

        animator.SetFloat(_speedKey, _owner.CurrentSpeed);
        animator.SetFloat(_tiltKey, _owner.TiltWeight);

        //Vector3 camForward = Camera.main.transform.forward;
        //camForward.y = 0;
        //camForward.Normalize();

        //Vector3 camRight = Camera.main.transform.right;
        //camRight.y = 0;
        //camRight.Normalize();

        //Vector3 moveDir;
        //Transform ownerTf = _owner.transform;
        //Vector3 lookDir;

        //if (_inputVertical != 0.0f || _inputHorizontal != 0.0)
        //{
        //    moveDir = (camForward * _inputVertical) + (camRight * _inputHorizontal);
        //    moveDir.Normalize();
        //    lookDir = moveDir;
        //}
        //else
        //{
        //    moveDir = _prevDir;
        //    moveDir.Normalize();
        //    lookDir = moveDir;
        //}

        //lookDir = (_owner.CurrentSpeed != 0.0f && lookDir != Vector3.zero) ? lookDir : ownerTf.forward;
        //Quaternion targetRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        //ownerTf.rotation = Quaternion.Lerp(ownerTf.rotation, targetRotation, _owner.RotationSpeed * Time.deltaTime);

        //_movement = moveDir * _owner.CurrentSpeed;
        //ownerTf.transform.position += _movement * Time.deltaTime;
    
        //_prevDir = lookDir;
    }

    private void UpdateSpeed()
    {
        if(_inputVertical != 0 || _inputHorizontal != 0)
        {
            _owner.CurrentSpeed = Mathf.MoveTowards(_owner.CurrentSpeed, _owner.Speed, _owner.AccelerateSpeed * Time.deltaTime);
        }
        else
        {
            _owner.CurrentSpeed = Mathf.MoveTowards(_owner.CurrentSpeed, 0.0f, _owner.AccelerateSpeed * Time.deltaTime * 2f);
        }
    }
}
