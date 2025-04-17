using UnityEngine;
using System.Collections;

public class GoldCoin : MonoBehaviour
{
    [Header("飞行设置")]
    public int value = 10;
    public float flySpeed = 8f;
    public float startDelay = 0.5f;

    private Transform target;
    private bool isFlying = false;

    void Start()
    {
        // 可靠的目标查找方式
        GameObject mainTower = GameObject.FindGameObjectWithTag("MainTower");
        if (mainTower == null)
        {
            Debug.LogError("未找到主塔！");
            Destroy(gameObject);
            return;
        }

        target = mainTower.transform;
        StartCoroutine(FlyRoutine());
    }

    IEnumerator FlyRoutine()
    {
        yield return new WaitForSeconds(startDelay);
        isFlying = true;

        while (Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            if (target == null) yield break;

            // 平滑移动
            transform.position = Vector3.Lerp(
                transform.position,
                target.position,
                flySpeed * Time.deltaTime
            );
            yield return null;
        }

        CollectGold();
    }

    void CollectGold()
    {
        if (GoldSystem.Instance != null)
        {
            GoldSystem.Instance.AddGold(value);
        }
        Destroy(gameObject);
    }

    // 可视化飞行路径
    void OnDrawGizmos()
    {
        if (target != null && isFlying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}