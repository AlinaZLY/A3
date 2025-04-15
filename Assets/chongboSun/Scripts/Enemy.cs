

using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("属性")]
    public float health = 100f;

    private NavMeshAgent agent;
    private Transform target;
    [Header("金币掉落")]
    public GameObject goldCoinPrefab; // 需在Inspector中赋值
    public int goldDropAmount = 10;

    void Start()
    {
        Debug.LogError("1111");
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("MainTower").transform;

        if (target != null && agent != null)
        {
            agent.SetDestination(target.position);
        } // 修复这里缺失的闭合大括号
        else
        {
            Debug.LogError("导航初始化失败！");
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{name} 受到 {damage} 伤害，剩余血量：{health}");
        if (health <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        if (agent == null) return;

        if (agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            Debug.LogWarning("路径被阻挡！当前位置：" + transform.position);
        }

        if (agent.remainingDistance < 1f)
        {
            Debug.Log("已到达目标！");
        }
    }

    void Die()
    {
        if (goldCoinPrefab != null)
        {
            Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
            Debug.Log($"生成金币：{goldDropAmount}枚");
        }
        else
        {
            Debug.LogError("金币预制体未赋值！");
        }
        Destroy(gameObject);
    }
}