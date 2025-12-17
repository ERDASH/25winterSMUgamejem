// 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
// 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.


public class AICharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 10f; // 점프 힘
    public Vector2 sensorSize = new Vector2(40f, 20f); // 큰 센서 크기
    public Vector2 shortSensorSize = new Vector2(10f, 5f); // 짧은 센서 크기
    public Vector2 direction;
    public LayerMask dongleLayer; // Dongle 레이어 설정

    private Rigidbody2D rb;
    private bool IsGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveTowardsHighestGroundedDongle();
    }

    // 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

private void MoveTowardsHighestGroundedDongle()
{
    // 큰 센서 범위 내의 Dongle 탐색
    Collider2D[] nearbyDongles = Physics2D.OverlapBoxAll(transform.position, sensorSize, 0, dongleLayer);

    DongleMerge highestDongle = null;
    float highestY = float.MinValue;

    foreach (Collider2D collider in nearbyDongles)
    {
        DongleMerge dongle = collider.GetComponent<DongleMerge>();
        if (dongle != null && dongle.isGrounded)
        {
            // 가장 높은 위치의 Dongle 찾기
            if (dongle.transform.position.y > highestY)
            {
                highestY = dongle.transform.position.y;
                highestDongle = dongle;
            }
        }
    }

    if (highestDongle != null)
    {
        // 가장 높은 Dongle을 향해 이동
        direction = (highestDongle.transform.position - transform.position).normalized;

        // 좌우 이동
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        // 짧은 센서 범위 내에 Dongle이 있는지 확인
        if (IsDongleInShortSensorRange() && IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            IsGrounded = false;
        }
    }
    else
    {
        // 주변에 Dongle이 없으면 멈춤
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
}

    private bool IsDongleInShortSensorRange()
    {
        // 짧은 센서 범위 내의 Dongle 탐색
        Collider2D[] nearbyDongles = Physics2D.OverlapBoxAll(transform.position, shortSensorSize, 0, dongleLayer);

        foreach (Collider2D collider in nearbyDongles)
        {
            DongleMerge dongle = collider.GetComponent<DongleMerge>();
            if (dongle != null)
            {
                return true; // 짧은 센서 범위 내에 Dongle이 존재
            }
        }

        return false; // 짧은 센서 범위 내에 Dongle이 없음
    }

    private void OnDrawGizmosSelected()
    {
        // 큰 센서 범위를 시각적으로 표시
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, sensorSize);

        // 짧은 센서 범위를 시각적으로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, shortSensorSize);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았는지 확인
        if (collision.contacts[0].normal.y > 0.5f)
        {
            IsGrounded = true;
        }
    }
}
