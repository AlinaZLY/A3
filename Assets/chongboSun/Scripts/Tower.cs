using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class Tower : MonoBehaviour
{
    [Header("攻击设置")]
    public float attackRange = 3f;
    public float attackRate = 1f;
    public int damage = 10;

    [Header("调试")]
    public Color rangeColor = Color.red; // 范围显示颜色

    private float nextAttackTime;
    private GameObject currentTarget;

    [Header("弹道设置")]
    public LineRenderer laserRenderer;
    public float laserDuration = 0.2f;
    public Color laserColor = Color.red;


    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            FindTarget();
            if (currentTarget != null)
            {
                Attack();
                nextAttackTime = Time.time + attackRate;
            }
        }
    }

    void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                currentTarget = col.gameObject;
                break;
            }
        }
    }

    void Attack()
    {
        // 添加实际攻击逻辑
        Enemy enemy = currentTarget.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"攻击 {currentTarget.name} 造成 {damage} 伤害");
        }
        ShowLaser(currentTarget.transform.position);
    }
    void ShowLaser(Vector3 targetPos)
    {
        // 配置激光参数
        laserRenderer.startColor = laserColor;
        laserRenderer.endColor = laserColor;
        laserRenderer.startWidth = 0.1f;
        laserRenderer.endWidth = 0.1f;

        // 设置路径点
        laserRenderer.positionCount = 2;
        laserRenderer.SetPosition(0, transform.position + Vector3.up);
        laserRenderer.SetPosition(1, targetPos);

        // 协程控制显示时间
        StartCoroutine(HideLaser());
    }

    IEnumerator HideLaser()
    {
        yield return new WaitForSeconds(laserDuration);
        laserRenderer.positionCount = 0;
    }
    // 在场景视图中显示攻击范围
    void OnDrawGizmosSelected()
    {
        Gizmos.color = rangeColor;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}