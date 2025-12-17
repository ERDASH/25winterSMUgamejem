// 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject targetObject; // JJoayo 오브젝트를 연결하세요.
    public float stuckTimeLimit = 3.0f; // 갇혀있다고 판단하기 위한 시간 (초)
    public LayerMask obstacleLayer; // 장애물 레이어 설정
    public float minMoveDistance = 0.01f; // 이 이하 이동이면 멈춘 것으로 판단
    public float minVelocity = 0.05f;     // 이 이상 속도가 있으면 '움찔거림'

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float stuckTimer;
    private float gameStartTime;
    private bool isGameRunning = false;
    private Vector2 lastPosition;
    private Vector2 rayDirection = Vector2.up;
    private float rayDistance = 1f;

    void Start()
    {
        if (targetObject != null)
        {
            rb = targetObject.GetComponent<Rigidbody2D>();
            boxCollider = targetObject.GetComponent<BoxCollider2D>();

            if (rb == null || boxCollider == null)
            {
                Debug.LogError("Target object must have Rigidbody2D and BoxCollider2D components!");
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned!");
        }

        // 게임 시작 시간 기록
        gameStartTime = Time.time;
        isGameRunning = true;
        lastPosition = rb.position;
    }

    void FixedUpdate()
{
    if (targetObject == null || rb == null || boxCollider == null)
        return;

    Vector2 boxTopCenter = new Vector2(
        boxCollider.bounds.center.x,
        boxCollider.bounds.max.y
    );

    // 레이 시각화 (Scene 뷰 + Gizmos ON 필수)
    Debug.DrawRay(
        boxTopCenter,
        rayDirection * rayDistance,
        Color.red,
        0.1f
    );

    RaycastHit2D hitUp = Physics2D.Raycast(
        boxTopCenter,
        rayDirection,
        rayDistance,
        obstacleLayer
    );

    float movedDistance = Vector2.Distance(rb.position, lastPosition);
    float velocityMagnitude = rb.linearVelocity.magnitude;

    bool tryingToMoveButStuck =
        movedDistance < minMoveDistance &&
        velocityMagnitude > minVelocity;

    if (hitUp.collider != null && tryingToMoveButStuck)
    {
        stuckTimer += Time.fixedDeltaTime;

        if (stuckTimer >= stuckTimeLimit)
        {
            Debug.Log($"{targetObject.name} is STUCK");
            EndGame();
        }
    }
    else
    {
        stuckTimer = 0f;
    }

    lastPosition = rb.position;
}

    public void EndGame()
    {
        if (isGameRunning)
        {
            isGameRunning = false;
            float gameEndTime = Time.time;
            float elapsedTime = gameEndTime - gameStartTime;

            Debug.Log($"Game ended. Total play time: {elapsedTime} seconds.");
            // 게임이 끝났을 때 추가 행동을 여기에 작성하세요.
        }
    }
}
