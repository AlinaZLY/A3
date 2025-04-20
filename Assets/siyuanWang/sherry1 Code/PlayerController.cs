using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("玩家动画机")] public Animator animator;
    [Header("移动速度")] public float moveSpeed = 3f;
    [Header("转向平滑度")] public float rotateSmooth = 10f;
    [Header("跳跃高度")] public float jumpHeight = 2f;
    [Header("重力")] public float gravity = -9.81f;
    [Header("玩家模型变换")] public Transform model;
    [Header("相机变换")] public Transform cameraTF;

    public Vector2 moveInput;
    [InspectorName("能否控制")] public List<string> canCtrl = new List<string>();
    public CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector3 moveDirection;// 计算移动方向
    void Start()
    {

    }
    void OnValidate()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // 确保角色贴地

        // 获取玩家输入
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // 能否控制
        if (canCtrl.Count != 0)
            return;

        Move();
        // 跳跃
        if (Input.GetButtonDown("Jump"))
            Jump();

        // 应用重力
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // 检查是否在地面上
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {

        }
    }

    void MovePlayer(Vector3 moveDirection)
    {
        // 将垂直方向设为0，只在水平方向移动
        moveDirection.y = 0f;
        // 计算移动向量并移动玩家
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void RotatePlayer(Vector3 moveDirection)
    {
        // 通过移动方向旋转玩家
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        // 将旋转限制为只影响 Y 轴
        toRotation = Quaternion.Euler(0f, toRotation.eulerAngles.y, 0f);
        model.rotation = Quaternion.Slerp(model.rotation, toRotation, Time.deltaTime * rotateSmooth);
    }
    void Move()
    {
        // 计算移动方向
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        // 控制动画状态和移动
        if (moveDirection.magnitude >= 0.1f)
        {
            // 根据相机方向调整移动方向
            moveDirection = cameraTF.TransformDirection(moveDirection);
            MovePlayer(moveDirection);  // 移动玩家
            RotatePlayer(moveDirection); // 旋转玩家
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }
    // 跳跃
    void Jump()
    {
        if (!isGrounded)
            return;
        animator.SetTrigger("Jump");
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    // 停止控制
    public void StopCtrl(string reason)
    {
        canCtrl.Add(reason);
        animator.SetBool("Move", false);
    }
    // 恢复控制
    public void ResumeCtrl(string reason)
    {
        canCtrl.Remove(reason);
    }
}
