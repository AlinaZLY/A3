using UnityEngine;
using TMPro;

public class sherryCoinCollector : MonoBehaviour
{
    public int coinCount = 0;              // 当前吃了多少
    public int totalCoins = 5;             // 场景中的金币总数
    public int maxCoinsCanEat = 5;         // 最多允许吃掉几个金币
    public TextMeshProUGUI coinText;       // UI文字对象

    void Start()
    {
        UpdateCoinUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin") && coinCount < maxCoinsCanEat)
        {
            coinCount++;
            Destroy(other.gameObject);     // 吃掉金币
            UpdateCoinUI();
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = coinCount + " / " + totalCoins;
        }
    }
}
