
using UnityEngine;

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
    }

    // 在场景视图中显示攻击范围
    void OnDrawGizmosSelected()
    {
        Gizmos.color = rangeColor;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}