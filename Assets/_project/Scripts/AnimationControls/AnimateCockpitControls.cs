using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCockpitControls : MonoBehaviour
{
    [Header("Flight control transforms and ranges")]
    [SerializeField]
    Transform _joystick;

    [Header("Flight control transforms and ranges")]
    [SerializeField]
    Vector3 _joystickRange = Vector3.zero;

    [Header("Flight control transforms and ranges")]
    [SerializeField]
    List<Transform> _throttles;

    [Header("Flight control transforms and ranges")]
    [SerializeField]
    float _throttleRange = 35f;
    [SerializeField]
    // ShipInputControls _movementInput;
    // IMovementControls ControlInput => _movementInput.MovementControls;

    IMovementControls _movementInput;

    void Update()
    {
        if (_movementInput == null) return;

        _joystick.localRotation = Quaternion.Euler(
            _movementInput.PitchAmount * _joystickRange.x,
            _movementInput.YawAmount * _joystickRange.y,
            _movementInput.RollAmount * _joystickRange.z
        );

        Vector3 throttleRotation = _throttles[0].localRotation.eulerAngles;
        throttleRotation.x = _movementInput.ThrustAmount * _throttleRange;
        foreach (Transform throttle in _throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }
    }
    public void Init(IMovementControls movementControls)
    {
        _movementInput = movementControls;
    }
}
