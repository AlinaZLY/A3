using System.Collections;
//using System.Diagnostics;

//using System.Diagnostics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // �������б�Ҫ�ֶ�
    private TowerData data;
    private GameObject currentTarget;
    private float nextAttackTime;
    private LineRenderer laserRenderer;
    private ParticleSystem laserEffect;
    private float laserAttackInterval;
    [Header("�������")]
    public Transform turretPivot;  // ������ת����
    public Transform firePoint;    // �����﷢���
    public ParticleSystem muzzleFlash; // ǹ������

    public void Initialize(TowerData towerData)
    {
        data = towerData;

        // ��ʼ����ײ�巶Χ
        GetComponent<SphereCollider>().radius = data.attackRange;

        // ��ʼ���������
        InitializeAttackComponents();
    }

    void InitializeAttackComponents()
    {
        // ���⹥����ʼ��
        if (data.useLaser)
        {
            laserRenderer = GetComponent<LineRenderer>();
            laserEffect = GetComponentInChildren<ParticleSystem>();
            laserRenderer.positionCount = 2;
        }

        // �����﹥����ʼ��
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
        // �ڹܳ���Ŀ��
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
        Debug.Log($"��⵽ {colliders.Length} ����ײ��"); // �����⵽����ײ������

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
            Debug.Log($"��ǰĿ��: {currentTarget.name}");
        }
        else
        {
            Debug.Log("δ�ҵ���ЧĿ��");
        }
    }

    void Attack()
    {
        // ���⹥��
        if (data.useLaser)
        {
            ApplyLaserDamage();
            return;
        }

        // �����﹥��
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
            enemy.TakeDamage(data.damage ); // �����˺�

            // ���Ӽ���Ч��
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
        // ���¼���λ��
        laserRenderer.SetPosition(0, firePoint.position);
        laserRenderer.SetPosition(1, currentTarget.transform.position);

        // ��������Ч��λ��
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