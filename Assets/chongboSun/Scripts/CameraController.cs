
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float zoomSpeed = 20f;
    public float minZoom = 5f;
    public float maxZoom = 50f;
    public float smoothTime = 0.2f;

    // 新增参数
    public Transform zoomReferencePoint; // 缩放参考点（建议设为场景焦点）
    public bool useLocalForward = true;  // 是否使用本地前向

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
            // 调试输出滚轮值
            Debug.Log($"Scroll Input: {scroll}");

            // 重新计算方向向量（关键修正）
            Vector3 zoomDir = useLocalForward ?
                transform.forward :
                (zoomReferencePoint.position - transform.position).normalized;

            // 计算实际缩放方向（重要改动）
            float zoomSign = Mathf.Sign(scroll);
            Vector3 desiredMove = zoomDir * zoomSpeed * Time.deltaTime * zoomSign;

            // 调试显示方向
            Debug.DrawRay(transform.position, desiredMove * 10, Color.red, 2f);

            // 计算新位置
            Vector3 newPosition = targetPosition + desiredMove;

            // 计算到参考点的距离
            float currentDistance = Vector3.Distance(newPosition, zoomReferencePoint.position);

            // 调试输出距离信息
            Debug.Log($"Current Distance: {currentDistance}");

            // 应用距离限制
            if (currentDistance >= minZoom && currentDistance <= maxZoom)
            {
                targetPosition = newPosition;
            }
            else
            {
                // 精确边界吸附
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

    // 调试辅助（显示缩放范围）
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