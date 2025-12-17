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
    public Vector2 tagSensorSize = new Vector2(20f, 10f);   // Tag 센서 크기
    public string targetTag = "Button";   

    private Rigidbody2D rb;
    private bool IsGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private bool IsTagInSensorRange()
{
    Collider2D[] objects = Physics2D.OverlapBoxAll(transform.position, tagSensorSize, 0);

    foreach (Collider2D col in objects)
    {
        if (col.CompareTag(targetTag))
        {
            return true;    // 범위 안에 해당 태그 존재
        }
    }

    return false;   // 없음
}

private void MoveTowardsPriorityTarget(Transform target)
{
    float dirX = Mathf.Sign(target.position.x - transform.position.x);
    rb.linearVelocity = new Vector2(dirX * moveSpeed, rb.linearVelocity.y);

    // 🔥 타깃이 더 높은 위치에 있고, 땅에 닿아 있다면 점프
    if (IsGrounded && target.position.y > transform.position.y + 0.1f)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        IsGrounded = false; 
    }
}

    private void FixedUpdate()
    {
        Transform priorityTarget = GetTargetObjectInSensor();

    if (priorityTarget != null)
    {
        MoveTowardsPriorityTarget(priorityTarget);
        return;
    }

    MoveTowardsHighestGroundedDongle();
    }

private Transform GetTargetObjectInSensor()
{
    Collider2D[] objects = Physics2D.OverlapBoxAll(transform.position, tagSensorSize, 0);

    Transform closest = null;
    float closestDist = float.MaxValue;

    foreach (Collider2D col in objects)
    {
        if (col.CompareTag(targetTag))
        {
            float dist = Vector2.Distance(transform.position, col.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = col.transform;
            }
        }
    }

    return closest; // 없다면 null
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

    if (highestDongle == null)
    return;

float myY = transform.position.y;
float targetY = highestDongle.transform.position.y;

bool hasHigherTarget = !IsGrounded || targetY > myY + 0.05f;

if (!hasHigherTarget)
{
    // 이미 가장 높은 위치
    rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    return;
}

// 이동
float dirX = Mathf.Sign(highestDongle.transform.position.x - transform.position.x);
rb.linearVelocity = new Vector2(dirX * moveSpeed, rb.linearVelocity.y);

    // 점프
    if (IsGrounded && IsDongleInShortSensorRange())
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        IsGrounded = false;
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
    
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, tagSensorSize);
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
