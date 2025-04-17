using System.Collections;
//using System.Diagnostics;

//using System.Diagnostics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // 声明所有必要字段
    private TowerData data;
    private GameObject currentTarget;
    private float nextAttackTime;
    private LineRenderer laserRenderer;
    private ParticleSystem laserEffect;
    private float laserAttackInterval;
    [Header("组件引用")]
    public Transform turretPivot;  // 炮塔旋转部件
    public Transform firePoint;    // 抛射物发射点
    public ParticleSystem muzzleFlash; // 枪口闪光

    public void Initialize(TowerData towerData)
    {
        data = towerData;

        // 初始化碰撞体范围
        GetComponent<SphereCollider>().radius = data.attackRange;

        // 初始化攻击组件
        InitializeAttackComponents();
    }

    void InitializeAttackComponents()
    {
        // 激光攻击初始化
        if (data.useLaser)
        {
            laserRenderer = GetComponent<LineRenderer>();
            laserEffect = GetComponentInChildren<ParticleSystem>();
            laserRenderer.positionCount = 2;
        }

        // 抛射物攻击初始化
        if (data.useProjectile && firePoint == null)
        {
            firePoint = new GameObject("FirePoint").transform;
            firePoint.SetParent(turretPivot);
            firePoint.localPosition = Vector3.forward * 0.5f;
        }
    }

    void Update()
    {
        Debug.Log(data);
        if (data == null) return;
        Debug.Log("9090909");
        if (Time.time >= nextAttackTime)
        {
            Debug.Log("9090909");
            FindTarget();
            if (currentTarget != null)
            {
                UpdateTurretRotation();
                Attack();
                nextAttackTime = Time.time + data.attackRate;
            }
        }

        UpdateLaserEffect();
    }

    void UpdateTurretRotation()
    {
        // 炮管朝向目标
        Vector3 dir = currentTarget.transform.position - turretPivot.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        turretPivot.rotation = Quaternion.Slerp(
            turretPivot.rotation,
            lookRotation,
            Time.deltaTime * 10f
        );
    }

    void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, data.attackRange);
        Debug.Log($"检测到 {colliders.Length} 个碰撞体"); // 输出检测到的碰撞体数量

        foreach (var col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                currentTarget = col.gameObject;
                break;
            }
        }
        if (currentTarget != null)
        {
            Debug.Log($"当前目标: {currentTarget.name}");
        }
        else
        {
            Debug.Log("未找到有效目标");
        }
    }

    void Attack()
    {
        // 激光攻击
        if (data.useLaser)
        {
            ApplyLaserDamage();
            return;
        }

        // 抛射物攻击
        if (data.useProjectile)
        {
            LaunchProjectile();
        }
    }

    void ApplyLaserDamage()
    {
        Enemy enemy = currentTarget.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log(data.damage);
            enemy.TakeDamage(data.damage ); // 持续伤害

            // 附加减速效果
            if (data.slowAmount > 0)
            {
                enemy.ApplySlow(data.slowAmount, data.slowDuration);
            }
        }
    }

    void LaunchProjectile()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        GameObject projectile = Instantiate(
            data.projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Initialize(data.damage, currentTarget.transform);
        }
        else
        {
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * data.projectileSpeed;
        }

        Destroy(projectile, 4f);
    }

    void UpdateLaserEffect()
    {
        if (!data.useLaser || currentTarget == null) return;

        Debug.Log("45664");
        // 更新激光位置
        laserRenderer.SetPosition(0, firePoint.position);
        laserRenderer.SetPosition(1, currentTarget.transform.position);

        // 更新粒子效果位置
        if (laserEffect != null)
        {
            laserEffect.transform.position = currentTarget.transform.position;
        }
        
    }

    void OnDrawGizmosSelected()
    {
        if (data != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.attackRange);
        }
    }
}