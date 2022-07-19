using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetCtrl : MonoBehaviour
{
    public Transform Target;

    [SerializeField] private float _rotationSpeed = 50.0f;
    [SerializeField] private float _pitchLimitMin = -50.0f;
    [SerializeField] private float _pitchLimitMax = 50.0f;

    private Vector3 _targetRotation = Vector3.zero;
    private Vector3 _velocity;

    private float _mouseX;
    private float _mouseY;

    private float _smoothTime = 0.1f;

    // Update is called once per frame
    void Update()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
    }

    private void LateUpdate()
    {
        //transform.position = Target.position + Vector3.up;

        _targetRotation.y += _mouseX * _rotationSpeed * Time.deltaTime;
        _targetRotation.x += _mouseY * _rotationSpeed * Time.deltaTime;

        _targetRotation.x = Mathf.Clamp(_targetRotation.x, _pitchLimitMin, _pitchLimitMax);

        _targetRotation.y = Clamp0360(_targetRotation.y);

        Quaternion localRotation = Quaternion.Euler(_targetRotation.x, _targetRotation.y, 0.0f);
        transform.rotation = localRotation;

        Vector3 targetEuler = transform.rotation.eulerAngles;
        if(_targetRotation.x < 0.0f)
        {
            _targetRotation = new Vector3(targetEuler.x > _pitchLimitMax ? targetEuler.x - 360.0f : targetEuler.x, targetEuler.y, targetEuler.z);
        }
        else
        {
            _targetRotation = targetEuler;
        }

        transform.position = Vector3.SmoothDamp(transform.position, Target.position + Vector3.up, ref _velocity, _smoothTime);
    }

    public static float Clamp0360(float eulerAngles)
    {
        float result = eulerAngles - Mathf.Ceil(eulerAngles / 360f) * 360f;
        if (result < 0)
        {
            result += 360f;
        }
        return result;
    }
}
