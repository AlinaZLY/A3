using UnityEngine;
using TMPro;

public class sherryAutoDialogueTrigger : MonoBehaviour
{
    public GameObject dialoguePanel; // 对话框
    public TextMeshProUGUI dialogueText; // 对话文本
    [TextArea(2, 5)]
    public string[] dialogueLines; // 对话内容数组

    private int currentLine = 0; // 当前显示的对话行数
    private bool hasTriggered = false; // 确保只触发一次

    void Start()
    {
        dialoguePanel.SetActive(false); // 游戏开始时对话框隐藏
        dialogueText.text = ""; // 游戏开始时对话框文本为空
    }

    private void OnTriggerEnter(Collider other)
    {
        // 碰到 Player 时触发对话框
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartDialogue(); // 开始对话
        }
    }

    void StartDialogue()
    {
        currentLine = 0;

        // 检查对话内容是否为空
        if (dialogueLines.Length > 0)
        {
            dialoguePanel.SetActive(true); // 显示对话框
            dialogueText.text = dialogueLines[currentLine]; // 显示第一句对话
            Invoke("NextLine", 2f); // 2秒后显示下一句
        }
        else
        {
            Debug.LogError("对话内容为空！"); // 如果没有对话内容，输出错误
        }
    }

    void NextLine()
    {
        currentLine++;

        // 确保当前行数不超过数组长度
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine]; // 显示下一句对话
            Invoke("NextLine", 2f); // 继续显示下一句
        }
        else
        {
            Invoke("HideDialogue", 2f); // 对话结束后隐藏对话框
        }
    }

    void HideDialogue()
    {
        dialoguePanel.SetActive(false); // 隐藏对话框
        dialogueText.text = ""; // 清空对话文本
    }
}
