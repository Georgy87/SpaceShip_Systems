using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    ShipDataSo _shipData;

    // protected ShipInputControls _inputControls;
    [SerializeField]
    MovementControlsBase _movementControls;
    [SerializeField]
    WeaponControlsBase _weaponControls;

    [SerializeField]
    List<ShipEnginee> _engines;

    [SerializeField]
    List<Blaster> _blasters;
    Rigidbody _rigidBody;
    [Range(-1f, 1f)]
    float _thrustAmount, _pitchAmount, _rollAmount, _yawAmount = 0f;

    IMovementControls MovementInput => _movementControls;
    IWeaponControls WeaponInput => _weaponControls;

    [SerializeField]
    private AnimateCockpitControls _cockpitAnimationControls;
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        // _cockpitControl.Init(ControlInput);
        
        if (MovementInput == null) return;
        foreach (ShipEnginee engine in _engines)
        {
            engine.Init(MovementInput);
        }
        foreach (Blaster blaster in _blasters)
        {
            blaster.Init(WeaponInput, _shipData.BlasterCooldown, _shipData.BlasterLaunchForce, _shipData.BlasterProjectileDuration, _shipData.BlasterDamage);
        }
        if (_cockpitAnimationControls != null)
        {
            _cockpitAnimationControls.Init(MovementInput);
        }
    }

    void Update()
    {
        _thrustAmount = MovementInput.ThrustAmount;
        _rollAmount = MovementInput.RollAmount;
        _yawAmount = MovementInput.YawAmount;
        _pitchAmount = MovementInput.PitchAmount;


    }

    void FixedUpdate()
    {
        if (!Mathf.Approximately(0f, _pitchAmount))
        {
            _rigidBody.AddTorque(transform.right * (_shipData.PitchForce * _pitchAmount * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, _rollAmount))
        {
            _rigidBody.AddTorque(transform.forward * (_shipData.RollForce * _rollAmount * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, _yawAmount))
        {
            _rigidBody.AddTorque(transform.up * (_yawAmount * _shipData.YawForce * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, _thrustAmount))
        {
            _rigidBody.AddForce(transform.forward * (_shipData.ThrustForce * _thrustAmount * Time.fixedDeltaTime));
        }
    }
}