using System;
using System.Collections.Generic;
using UnityEngine;

public class AIShipMovementControls : MovementControlsBase
{
    [SerializeField]
    Transform _target;

    [SerializeField]
    bool _enableYaw = true;

    [SerializeField]
    PIDController _yawPidController;

    [SerializeField]
    bool _enablePitch = true;

    [SerializeField]
    PIDController _pitchPidController;

    [SerializeField]
    // CollisionAvoidance _collisionAvoidance;

    public override float YawAmount => GetYawAmount();
    public override float PitchAmount => GetPitchAmount();
    public override float RollAmount => GetRollAmount();
    public override float ThrustAmount => GetThrustAmount();

    float DistanceToTarget => _target ? Vector3.Distance(_target.position, _transform.position) : 0f;

    public Vector3 _localDirection;
    public float _distanceToTarget;
    public float _pitch, _yaw, _thrust;
    Transform _transform;
    float _yawAmount, _pitchAmount, _rollAmount, _thrustAmount, _horizontalAvoidance, _verticalAvoidance;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        if (!_target) return;

        _distanceToTarget = DistanceToTarget;
        _localDirection = Quaternion.Inverse(_transform.rotation) * (_target.position - _transform.position);
        // CheckCollisionAvoidance();
        // _yawAmount = GetYawAmount();
        // _pitchAmount = GetPitchAmount();
        // _rollAmount = GetRollAmount();
        // _thrustAmount = GetThrustAmount();
    }

    float GetYawAmount()
    {
        if (!_target || !_enableYaw) return 0f;
        // if (!Mathf.Approximately(0f, _horizontalAvoidance)) return _horizontalAvoidance;
        _yaw = Mathf.Atan2(_localDirection.x, _localDirection.z) * Mathf.Rad2Deg;
        if (Mathf.Approximately(0, _yaw)) return 0f;
        return _yawPidController.Update(Time.deltaTime, _yaw, 0f) * -1f;
    }

    float GetPitchAmount()
    {
        if (!_target || !_enablePitch) return 0f;
        // if (!Mathf.Approximately(0f, _verticalAvoidance)) return _verticalAvoidance;
        _pitch = Vector3.Angle(Vector3.down, _localDirection) - 90f;
        return _pitchPidController.Update(Time.deltaTime, _pitch, 0f);
    }

    float GetRollAmount()
    {
        if (!_target) return 0f;
        // if (!Mathf.Approximately(0f, _verticalAvoidance) || !Mathf.Approximately(0f, _horizontalAvoidance)) return 0f;
        return Math.Abs(_yaw) > 0.25f ? _yaw * -1 : 0f;
    }

    float GetThrustAmount()
    {
        _thrustAmount = Mathf.Lerp(_thrustAmount, _distanceToTarget > 100f ? 1f : 0f, Time.deltaTime);
        return _thrustAmount;
    }


    // void CheckCollisionAvoidance()
    // {
    //     CheckForHorizontalCollision();
    //     CheckForVerticalCollision();
    // }

    // void CheckForVerticalCollision()
    // {
    //     if (Mathf.Approximately(0f, _collisionAvoidance.VerticalAvoidance))
    //     {
    //         _verticalAvoidance = 0f;
    //     }
    //     else
    //     {
    //         _verticalAvoidance = _collisionAvoidance.VerticalAvoidance;
    //     }
    // }

    // void CheckForHorizontalCollision()
    // {
    //     if (Mathf.Approximately(0f, _collisionAvoidance.HorizontalAvoidance))
    //     {
    //         _horizontalAvoidance = 0f;
    //     }
    //     else
    //     {
    //         _horizontalAvoidance = _collisionAvoidance.HorizontalAvoidance;
    //     }
    // }

    // public void SetTarget(Transform target)
    // {
    //     _target = target;
    // }
}
