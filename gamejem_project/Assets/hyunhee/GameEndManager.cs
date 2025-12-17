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

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float stuckTimer;

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
    }

    void Update()
    {
        if (targetObject != null && rb != null && boxCollider != null)
        {
            // 점프를 시도할 때 위에 장애물이 있는지 확인
            Vector2 boxTopCenter = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.max.y);
            RaycastHit2D hit = Physics2D.Raycast(boxTopCenter, Vector2.up, 0.1f, obstacleLayer);

            if (hit.collider != null)
            {
                stuckTimer += Time.deltaTime;

                // 갇혀있는지 확인
                if (stuckTimer >= stuckTimeLimit)
                {
                    Debug.Log($"{targetObject.name} is stuck due to an obstacle above!");
                    // 갇혀있을 때 추가 행동을 여기에 작성하세요.
                }
            }
            else
            {
                stuckTimer = 0; // 장애물이 없으면 타이머 초기화
            }
        }
    }
}
