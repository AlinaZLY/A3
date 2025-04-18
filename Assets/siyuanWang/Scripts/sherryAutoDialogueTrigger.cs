using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sherryAutoDialogueTrigger : MonoBehaviour
{
    public GameObject dialoguePanel; // 对话框
    public string[] dialogueLines; // 对话内容数组
    private bool hasTriggered = false; // 确保只触发一次

    void Start()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        // 碰到 Player 时触发对话框
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            dialoguePanel.SetActive(true);
        }
    }
}
