using System;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private float bulletSpeed;
    private Transform currentTarget;
    public bool friendlyShooter;

    private float timeSinceLastShot = Mathf.Infinity;
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        
        if (timeSinceLastShot > timeBetweenShots && currentTarget != null)
        {
            ShootBullet(currentTarget.transform);
        }
    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    private void ShootBullet(Transform target)
    {
        timeSinceLastShot = 0;
        
        var bullet = Instantiate(bulletPrefab);
        Destroy(bullet,  bulletPrefab.GetComponent<Bullet>().lifetime);
        bullet.transform.position = transform.position;
        bullet.layer = friendlyShooter ? 7 : 6;
        

        Quaternion rotation = Quaternion.LookRotation(
            target.transform.position - transform.position ,
            transform.TransformDirection(Vector3.forward)
        );
        bullet.transform.rotation = new Quaternion( 0 , 0 , rotation.z , rotation.w );
        
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
