using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        // ��ȡ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // �����ƶ����򣨻�����������ˮƽͶӰ��
        Vector3 forward = transform.forward;
        forward.y = 0; // ������ֱ����
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        // �ϳ��ƶ�����
        Vector3 moveDirection = (forward * v + right * h).normalized;

        // ִ���ƶ�
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}