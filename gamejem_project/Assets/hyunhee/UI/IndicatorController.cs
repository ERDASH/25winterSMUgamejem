// 2025-12-18 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    public GameObject endingTrigger; // ending_trigger 오브젝트
    public RectTransform indicatorUI; // 인디케이터 UI (Canvas 내 RectTransform)
    public float minScale = 0.5f; // 최소 크기 비율
    public float maxScale = 1.0f; // 최대 크기 비율
    public float maxDistance = 50f; // 최대 거리 (이 거리 이상이면 최소 크기)

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (endingTrigger != null && indicatorUI != null && mainCamera != null)
        {
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(endingTrigger.transform.position);

            if (screenPoint.z > 0 && (screenPoint.x < 0 || screenPoint.x > Screen.width || screenPoint.y < 0 || screenPoint.y > Screen.height))
            {
                indicatorUI.gameObject.SetActive(true);

                // 화면 가장자리로 위치 조정
                float x = Mathf.Clamp(screenPoint.x, 0, Screen.width);
                float y = Mathf.Clamp(screenPoint.y, 0, Screen.height);
                indicatorUI.position = new Vector3(x, y, indicatorUI.position.z);

                // 거리 계산
                float distance = Vector3.Distance(mainCamera.transform.position, endingTrigger.transform.position);

                // 거리 기반 크기 조정
                float scale = Mathf.Lerp(maxScale, minScale, Mathf.Clamp01(distance / maxDistance));
                indicatorUI.localScale = new Vector3(scale, scale, 1);
            }
            else
            {
                indicatorUI.gameObject.SetActive(false);
            }
        }
    }
}
