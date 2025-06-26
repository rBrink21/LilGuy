using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private float bulletSpeed;
    private Transform currentTarget;
    [HideInInspector] public bool friendlyShooter;

    [SerializeField] private bool shootAtFixedAngle;
    [Range(0,360)]
    [SerializeField] private float fixedAngle;
    [SerializeField] private float debugFixedAngleLineLength = 10f;
    private float timeSinceLastShot = Mathf.Infinity;

    private void OnDrawGizmosSelected()
    {
        if (shootAtFixedAngle)
        {
            Gizmos.color = Color.red;
            Vector2 direction = GetDirectionFromAngle(fixedAngle);
            Vector3 endPoint = transform.position + (Vector3)(direction * debugFixedAngleLineLength);
            Gizmos.DrawLine(transform.position, endPoint);
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        
        if (timeSinceLastShot > timeBetweenShots && (currentTarget != null || shootAtFixedAngle))
        {
            if (shootAtFixedAngle)
            {
                Vector2 direction = GetDirectionFromAngle(fixedAngle);
                ShootBullet(direction);
                return;
            }
            ShootBullet(GetDirectionToTarget(currentTarget.transform.position));
        }
    }

    public void SetTarget(Transform target)
    {
        if (shootAtFixedAngle) return;
        currentTarget = target;
    }

    private void ShootBullet(Vector2 direction)
    {
        timeSinceLastShot = 0;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Destroy(bullet, bullet.GetComponent<Bullet>().lifetime);
        bullet.layer = friendlyShooter ? 7 : 6;
        
        Quaternion rotation = Quaternion.LookRotation(
            new Vector3(direction.x, direction.y, transform.position.z) - transform.position ,
            transform.TransformDirection(Vector3.back)
        );
        bullet.transform.rotation = new Quaternion( 0 , 0 , rotation.z , rotation.w );
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }
    private Vector2 GetDirectionToTarget(Vector3 target)
    {
        return ((Vector2)(target - transform.position)).normalized;
    }
    
    private Vector2 GetDirectionFromAngle(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
