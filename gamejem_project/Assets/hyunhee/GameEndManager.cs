// 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEndManager : MonoBehaviour
{
    public TextMeshProUGUI elapsedTimeText;

    public GameObject targetObject; // JJoayo 오브젝트를 연결하세요.
    public float stuckTimeLimit = 3.0f; // 갇혀있다고 판단하기 위한 시간 (초)
    public LayerMask obstacleLayer; // 장애물 레이어 설정
    public float minMoveDistance = 0.01f; // 이 이하 이동이면 멈춘 것으로 판단
    public float elapsedTime;
    public bool isCleared = false;
    public GameObject failUI;
    public AudioSource audioSource; // AudioSource 컴포넌트
    public AudioClip dieSound; // 사망 시 재생할 사운드
    public AudioClip failSound; // 실패 시 재생할 사운드

    private Rigidbody2D rb;
    private PolygonCollider2D boxCollider;
    private AICharacterController controller;
    private float stuckTimer;
    private float gameStartTime;
    public bool isGameRunning = false;
    private Vector2 lastPosition;
    private Vector2 rayDirection = Vector2.up;
    private float rayDistance = 2f;

    void Start()
    {
        if (targetObject != null)
        {
            rb = targetObject.GetComponent<Rigidbody2D>();
            boxCollider = targetObject.GetComponent<PolygonCollider2D>();
            controller = targetObject.GetComponent<AICharacterController>();

            if (rb == null || boxCollider == null)
            {
                Debug.LogError("Target object must have Rigidbody2D and PolygCollider2D components!");
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

    bool tryingToMoveButStuck =
        movedDistance < minMoveDistance;

    if(controller.hasHigherTarget)
    {
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
    }

    lastPosition = rb.position;
}

    void Update()
    {
        if (isCleared)
        return;

        // 경과 시간 업데이트
        elapsedTime += Time.deltaTime;

        // TextMeshPro UI에 경과 시간 표시
        if (elapsedTimeText != null)
        {
        elapsedTimeText.text = $"Elapsed Time: {elapsedTime:F2} seconds";
        }
    }
    


    public void EndGame()
    {
        if (isGameRunning)
        {
            isGameRunning = false;
            float gameEndTime = Time.time;
            elapsedTime = gameEndTime - gameStartTime;
            
            audioSource.PlayOneShot(failSound);
            audioSource.volume = 0.1f;
            audioSource.PlayOneShot(dieSound);

            if (failUI != null) 
            {
                failUI.SetActive(true);
            }
            Debug.Log($"Game ended. Total play time: {elapsedTime} seconds.");
            // 게임이 끝났을 때 추가 행동을 여기에 작성하세요.
        }
    }
}

