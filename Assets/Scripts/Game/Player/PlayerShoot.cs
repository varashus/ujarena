using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _gunOffset;
    [SerializeField] private float _timeBetweenShots;

    private bool _fireContinuously;
    private float _lastFireTime;

    private void OnEnable()
    {
        Joystick.OnJoystickReleased += StopShooting;
    }

    private void OnDisable()
    {
        Joystick.OnJoystickReleased -= StopShooting;
    }

    private void Update()
    {
        if (_fireContinuously)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;
            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet();
                _lastFireTime = Time.time;
            }
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.velocity = _bulletSpeed * transform.up;
    }

    private void OnFire(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            _fireContinuously = true;
        }
        else
        {
            _fireContinuously = false;
        }
    }

    private void StopShooting()
    {
        _fireContinuously = false;
    }
}
