using System;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private float bulletSpeed;
    private Transform currentTarget;
    [HideInInspector] public bool friendlyShooter;

    [SerializeField] private bool shootAtFixedAngle;
    [SerializeField] private float fixedAngle;
    [SerializeField] private float debugFixedAngleLineLength = 10f;
    private float timeSinceLastShot = Mathf.Infinity;

    private void OnDrawGizmosSelected()
    {
        if (shootAtFixedAngle)
        {
            Gizmos.color = Color.red;

            var targetPosition = transform.position +
                                 new Vector3(AngleToDirection(fixedAngle).x, AngleToDirection(fixedAngle).y, 0) * 5;
            Gizmos.DrawLine(transform.position, targetPosition);
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        
        if (timeSinceLastShot > timeBetweenShots && currentTarget != null)
        {
            ShootBullet(currentTarget.transform.position);
        }
    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    private void ShootBullet(Vector3 target)
    {
        timeSinceLastShot = 0;
        
        var bullet = Instantiate(bulletPrefab);
        Destroy(bullet,  bulletPrefab.GetComponent<Bullet>().lifetime);
        bullet.transform.position = transform.position;
        bullet.layer = friendlyShooter ? 7 : 6;
        

        Quaternion rotation = Quaternion.LookRotation(
            target - transform.position ,
            transform.TransformDirection(Vector3.back)
        );
        bullet.transform.rotation = new Quaternion( 0 , 0 , rotation.z , rotation.w );
        
        bullet.GetComponent<Rigidbody2D>().linearVelocity = GetDirectionToTarget(target) * bulletSpeed;
    }

    private Vector3 GetRotationToTarget(Transform target)
    {
        var direction = Quaternion.FromToRotation(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        return direction.eulerAngles;
    }

    private Vector3 GetDirectionToTarget(Vector3 target)
    {
        return (target - transform.position).normalized;
    }

    private static Vector2 AngleToDirection(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleRad), -Mathf.Sin(angleRad));
    }
}
