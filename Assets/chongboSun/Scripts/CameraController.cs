using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        // 获取输入
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 计算移动方向（基于相机朝向的水平投影）
        Vector3 forward = transform.forward;
        forward.y = 0; // 消除垂直分量
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        // 合成移动向量
        Vector3 moveDirection = (forward * v + right * h).normalized;

        // 执行移动
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}