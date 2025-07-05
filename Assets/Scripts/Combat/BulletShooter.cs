using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private float bulletSpeed;
    private Transform currentTarget;
    private bool friendlyShooter;

    
    [Header("Fixed Angle Settings")]
    [SerializeField] private bool shootAtFixedAngle;
    [Range(0,360)]
    [SerializeField] private float fixedAngle;
    [SerializeField] private float debugFixedAngleLineLength = 10f;
    private float timeSinceLastShot = Mathf.Infinity;
    [SerializeField] private bool angleRelativeToFriend;

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


    private void Start()
    {
        friendlyShooter = CompareTag("Friend");
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        
        if (timeSinceLastShot > timeBetweenShots && (currentTarget != null || shootAtFixedAngle && Vector2.Distance(
                transform.position, 
                GameObject.FindGameObjectWithTag("Player").transform.position) < 20f))
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

    private void ShootBullet(Vector3 direction)
    {
        timeSinceLastShot = 0;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Destroy(bullet, bullet.GetComponent<Bullet>().lifetime);
        bullet.layer = friendlyShooter ? 7 : 6;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }
    private Vector3 GetDirectionToTarget(Vector3 target)
    {
        return (target - transform.position).normalized;
    }
    
    private Vector2 GetDirectionFromAngle(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;

        if (angleRelativeToFriend)
        {
            angleRad += (transform.eulerAngles.z * Mathf.Deg2Rad);
        }
        
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
