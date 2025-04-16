
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("属性")]
    public float health = 100f;

    [Header("金币掉落")]
    public GameObject goldCoinPrefab; // 需在Inspector中赋值
    public int goldDropAmount = 10;

    // 只保留一个agent声明
    private NavMeshAgent agent;
    private Transform target;
    private float originalSpeed;
    private float slowEndTime;

    void Start()
    {
        Debug.LogError("1111");
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("MainTower").transform;
        originalSpeed = agent.speed; // 保存初始速度

        if (target != null && agent != null)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogError("导航初始化失败！");
        }
    }

    public void ApplySlow(float slowPercent, float duration)
    {
        agent.speed = originalSpeed * (1 - slowPercent);
        slowEndTime = Time.time + duration;
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

        // 路径检测
        if (agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            Debug.LogWarning("路径被阻挡！当前位置：" + transform.position);
        }

        // 目标距离检测
        if (agent.remainingDistance < 1f)
        {
            Debug.Log("已到达目标！");
        }

        // 减速持续时间检测
        if (Time.time > slowEndTime && Mathf.Approximately(agent.speed, originalSpeed) == false)
        {
            agent.speed = originalSpeed;
            Debug.Log("减速效果结束");
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