using System;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private float bulletSpeed;
    
    private float timeSinceLastShot;
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        var target = GameObject.FindGameObjectWithTag("Player");
        if (timeSinceLastShot > timeBetweenShots)
        {
            ShootBullet(target.transform);
        }
    }

    private void ShootBullet(Transform target)
    {
        timeSinceLastShot = 0;
        
        var bullet = Instantiate(bulletPrefab);
        Destroy(bullet,  bulletPrefab.GetComponent<Bullet>().lifetime);
        bullet.transform.position = transform.position;
        bullet.transform.LookAt(target);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = GetDirectionToTarget(target) * bulletSpeed;
    }

    private Vector3 GetRotationToTarget(Transform target)
    {
        var direction = Quaternion.FromToRotation(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        return direction.eulerAngles;
    }

    private Vector3 GetDirectionToTarget(Transform target)
    {
        return (target.position - transform.position).normalized;
    }
}
