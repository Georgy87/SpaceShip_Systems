using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEnginee : MonoBehaviour
{
    [SerializeField] GameObject _thruster;

    IMovementControls _shipMovementControls;
    Rigidbody _rigidbody;
    float _thrustForce;
    float _thrustAmount;

    bool ThrustersEnabled => !Mathf.Approximately(0f, _shipMovementControls.ThrustAmount);

    void Update()
    {
        // Debug.Log(_shipMovementControls);
        ActivateThrusters();
    }

    // void FixedUpdate()
    // {
    //     if (!ThrustersEnabled) return;
    //     _rigidbody.AddForce(transform.forward * _thrustAmount * Time.fixedDeltaTime);
    // }


    public void Init(IMovementControls movementControls)
    {
        _shipMovementControls = movementControls;
    }

    void ActivateThrusters()
    {
        _thruster.SetActive(ThrustersEnabled);
        // if (!ThrustersEnabled) return;
        // _thrustAmount = _thrustForce * _shipMovementControls.ThrustAmount;
    }

}
