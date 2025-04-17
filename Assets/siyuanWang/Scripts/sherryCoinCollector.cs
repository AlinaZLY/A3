using UnityEngine;

public class sherryCoinCollector : MonoBehaviour
{
    public int coinCount = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject); // 吃掉金币
            // 你也可以加个音效或粒子效果在这里
            Debug.Log("吃了金币！当前金币数：" + coinCount);
        }
    }
}
