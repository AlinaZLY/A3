using UnityEngine;

public class sherryPlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;     // 跳跃力度
    public LayerMask groundLayer;    // 地面图层
    public Transform groundCheck;    // 地面检测点
    public float groundCheckRadius = 0.2f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 检测是否在地面
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // 按空格跳跃
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
}
