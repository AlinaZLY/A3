using UnityEngine;
using System.Collections;

public class GoldCoin : MonoBehaviour
{
    [Header("��������")]
    public int value = 10;
    public float flySpeed = 8f;
    public float startDelay = 0.5f;

    private Transform target;
    private bool isFlying = false;

    void Start()
    {
        // �ɿ���Ŀ����ҷ�ʽ
        GameObject mainTower = GameObject.FindGameObjectWithTag("MainTower");
        if (mainTower == null)
        {
            Debug.LogError("δ�ҵ�������");
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

            // ƽ���ƶ�
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

    // ���ӻ�����·��
    void OnDrawGizmos()
    {
        if (target != null && isFlying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}