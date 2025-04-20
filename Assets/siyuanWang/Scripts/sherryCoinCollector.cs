using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class sherryCoinCollector : MonoBehaviour
{
    public GameObject window;                // 成功窗口
    public GameObject failWindow;            // 失败窗口
    public int coinCount = 0;
    public int totalCoins = 4;               // 总金币改为4个
    public int maxCoinsCanEat = 4;           // 最大可吃金币改为4个
    public TextMeshProUGUI coinText;

    public float timer = 60f;                // 倒计时
    private bool gameEnded = false;

    void Start()
    {
        UpdateCoinUI();
        window.SetActive(false);             // 成功弹窗隐藏
        failWindow.SetActive(false);         // 失败弹窗隐藏
    }

    void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = 0f;
            CheckFailCondition();            // 时间到了检查是否失败
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin") && coinCount < maxCoinsCanEat)
        {
            coinCount++;
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();

            UpdateCoinUI();

            // 修改成功条件为等于总金币数
            if (coinCount >= totalCoins && !gameEnded)
            {
                gameEnded = true;
                window.SetActive(true);      // 成功窗口显示
                Debug.Log("成功过关！");
            }
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = coinCount + " / " + totalCoins;
        }
    }

    void CheckFailCondition()
    {
        // 失败条件改为小于总金币数
        if (coinCount < totalCoins)
        {
            gameEnded = true;
            failWindow.SetActive(true);      // 显示失败窗口
            Debug.Log("时间到，失败！");
        }
    }
}