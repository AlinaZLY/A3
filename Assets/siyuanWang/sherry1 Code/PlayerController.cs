using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("��Ҷ�����")] public Animator animator;
    [Header("�ƶ��ٶ�")] public float moveSpeed = 3f;
    [Header("ת��ƽ����")] public float rotateSmooth = 10f;
    [Header("��Ծ�߶�")] public float jumpHeight = 2f;
    [Header("����")] public float gravity = -9.81f;
    [Header("���ģ�ͱ任")] public Transform model;
    [Header("����任")] public Transform cameraTF;

    public Vector2 moveInput;
    [InspectorName("�ܷ����")] public List<string> canCtrl = new List<string>();
    public CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector3 moveDirection;// �����ƶ�����
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
            velocity.y = -2f; // ȷ����ɫ����

        // ��ȡ�������
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // �ܷ����
        if (canCtrl.Count != 0)
            return;

        Move();
        // ��Ծ
        if (Input.GetButtonDown("Jump"))
            Jump();

        // Ӧ������
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ����Ƿ��ڵ�����
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {

        }
    }

    void MovePlayer(Vector3 moveDirection)
    {
        // ����ֱ������Ϊ0��ֻ��ˮƽ�����ƶ�
        moveDirection.y = 0f;
        // �����ƶ��������ƶ����
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void RotatePlayer(Vector3 moveDirection)
    {
        // ͨ���ƶ�������ת���
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        // ����ת����ΪֻӰ�� Y ��
        toRotation = Quaternion.Euler(0f, toRotation.eulerAngles.y, 0f);
        model.rotation = Quaternion.Slerp(model.rotation, toRotation, Time.deltaTime * rotateSmooth);
    }
    void Move()
    {
        // �����ƶ�����
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        // ���ƶ���״̬���ƶ�
        if (moveDirection.magnitude >= 0.1f)
        {
            // ���������������ƶ�����
            moveDirection = cameraTF.TransformDirection(moveDirection);
            MovePlayer(moveDirection);  // �ƶ����
            RotatePlayer(moveDirection); // ��ת���
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }
    // ��Ծ
    void Jump()
    {
        if (!isGrounded)
            return;
        animator.SetTrigger("Jump");
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    // ֹͣ����
    public void StopCtrl(string reason)
    {
        canCtrl.Add(reason);
        animator.SetBool("Move", false);
    }
    // �ָ�����
    public void ResumeCtrl(string reason)
    {
        canCtrl.Remove(reason);
    }
}
