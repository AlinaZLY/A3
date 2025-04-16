using System.Collections;
using UnityEngine;
using System.Collections.Generic;
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

    [Header("��������")]
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
        // ���ʵ�ʹ����߼�
        Enemy enemy = currentTarget.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"���� {currentTarget.name} ��� {damage} �˺�");
        }
        ShowLaser(currentTarget.transform.position);
    }
    void ShowLaser(Vector3 targetPos)
    {
        // ���ü������
        laserRenderer.startColor = laserColor;
        laserRenderer.endColor = laserColor;
        laserRenderer.startWidth = 0.1f;
        laserRenderer.endWidth = 0.1f;

        // ����·����
        laserRenderer.positionCount = 2;
        laserRenderer.SetPosition(0, transform.position + Vector3.up);
        laserRenderer.SetPosition(1, targetPos);

        // Э�̿�����ʾʱ��
        StartCoroutine(HideLaser());
    }

    IEnumerator HideLaser()
    {
        yield return new WaitForSeconds(laserDuration);
        laserRenderer.positionCount = 0;
    }
    // �ڳ�����ͼ����ʾ������Χ
    void OnDrawGizmosSelected()
    {
        Gizmos.color = rangeColor;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}