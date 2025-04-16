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

        // 使用激光攻击
        ShowLaser(currentTarget.transform.position);
    }

    void ShowLaser(Vector3 targetPos)
    {
        if (laserRenderer == null) return;

        // Make sure there are at least 2 points in the line renderer
        laserRenderer.positionCount = 2;

        // Set the positions
        laserRenderer.SetPosition(0, transform.position + Vector3.up);  // Starting point of the laser
        laserRenderer.SetPosition(1, targetPos);  // End point (target position)

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
