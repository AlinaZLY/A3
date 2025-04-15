
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("��������")]
    public float attackRange = 3f;
    public float attackRate = 1f;
    public int damage = 10;

    [Header("����")]
    public Color rangeColor = Color.red; // ��Χ��ʾ��ɫ

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
        // ���ʵ�ʹ����߼�
        Enemy enemy = currentTarget.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"���� {currentTarget.name} ��� {damage} �˺�");
        }
    }

    // �ڳ�����ͼ����ʾ������Χ
    void OnDrawGizmosSelected()
    {
        Gizmos.color = rangeColor;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}