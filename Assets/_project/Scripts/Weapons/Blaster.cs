using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] Projectile _projectilePrefab;

    [SerializeField] Transform _muzzle;
    float _coolDownTime;

    int _launchForce, _damage;
    float _duration;

    bool CanFire
    {
        get
        {
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }

    float _coolDown;

    IWeaponControls _weaponInput;

    // Update is called once per frame
    void Update()
    {
        if (_weaponInput == null)
        {
            return;
        }

        if (CanFire && _weaponInput.PrimaryFired)
        {
            FireProjectile();
        }
    }

    public void Init(IWeaponControls weaponInput, float coolDown, int launchForce,
        float duration,
        int damage)
    {
        _weaponInput = weaponInput;
        _coolDownTime = coolDown;
        _launchForce = launchForce;
        _duration = duration;
        _damage = damage;
    }

    void FireProjectile()
    {
        _coolDown = _coolDownTime;
        Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
        Projectile projectile = Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
        projectile.gameObject.SetActive(false);
        projectile.Init(_launchForce, _damage, _duration);
        projectile.gameObject.SetActive(true);
    }
}
