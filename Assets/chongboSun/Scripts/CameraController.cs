
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float zoomSpeed = 20f;
    public float minZoom = 5f;
    public float maxZoom = 50f;
    public float smoothTime = 0.2f;

    // ��������
    public Transform zoomReferencePoint; // ���Ųο��㣨������Ϊ�������㣩
    public bool useLocalForward = true;  // �Ƿ�ʹ�ñ���ǰ��

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        targetPosition = transform.position;
        if (!zoomReferencePoint) zoomReferencePoint = transform;
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
        ApplySmoothMovement();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = useLocalForward ?
            transform.forward :
            Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

        forward.y = 0;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        targetPosition += (forward * v + right * h) * moveSpeed * Time.deltaTime;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.001f)
        {
            // �����������ֵ
            Debug.Log($"Scroll Input: {scroll}");

            // ���¼��㷽���������ؼ�������
            Vector3 zoomDir = useLocalForward ?
                transform.forward :
                (zoomReferencePoint.position - transform.position).normalized;

            // ����ʵ�����ŷ�����Ҫ�Ķ���
            float zoomSign = Mathf.Sign(scroll);
            Vector3 desiredMove = zoomDir * zoomSpeed * Time.deltaTime * zoomSign;

            // ������ʾ����
            Debug.DrawRay(transform.position, desiredMove * 10, Color.red, 2f);

            // ������λ��
            Vector3 newPosition = targetPosition + desiredMove;

            // ���㵽�ο���ľ���
            float currentDistance = Vector3.Distance(newPosition, zoomReferencePoint.position);

            // �������������Ϣ
            Debug.Log($"Current Distance: {currentDistance}");

            // Ӧ�þ�������
            if (currentDistance >= minZoom && currentDistance <= maxZoom)
            {
                targetPosition = newPosition;
            }
            else
            {
                // ��ȷ�߽�����
                float clampedDistance = Mathf.Clamp(currentDistance, minZoom, maxZoom);
                targetPosition = zoomReferencePoint.position +
                               (newPosition - zoomReferencePoint.position).normalized * clampedDistance;
            }
        }
    }

    void ApplySmoothMovement()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }

    // ���Ը�������ʾ���ŷ�Χ��
    void OnDrawGizmosSelected()
    {
        if (zoomReferencePoint)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(zoomReferencePoint.position, minZoom);
            Gizmos.DrawWireSphere(zoomReferencePoint.position, maxZoom);
        }
    }
}