using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Detonator _hitEffect;
    // [SerializeField]
    // [Range(1000f, 25000f)]
    // float _launchForce = 5000f;
    // [SerializeField][Range(10, 100)] int _damage = 50;
    // [SerializeField][Range(2f, 10f)] float _range = 5f;
    float _launchForce;
    int _damage;
    float _range;
    float _duration;

    bool OutOfFuel
    {
        get
        {
            _duration -= Time.deltaTime;
            return _duration <= 0f;
        }
    }

    Rigidbody _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        _rigidBody.AddForce(_launchForce * transform.forward);
        _duration = _range;
    }

    private void OnDisable()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
    }


    void Update()
    {
        if (OutOfFuel) Destroy(gameObject);
    }


    public void Init(int launchForce, int damage, float range)
    {
        _launchForce = launchForce;
        _damage = damage;
        _range = range;
        // _rigidBody.velocity = velocity;
        // _rigidBody.angularVelocity = angularVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.collider.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Vector3 hitPosition = collision.GetContact(0).point;
            damageable.TakeDamage(_damage, hitPosition);
        }

        if (_hitEffect != null)
        {
            Instantiate(_hitEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
