// 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class SpawnPreview : MonoBehaviour
{
    public GameObject[] donglePrefabs; // 스폰 가능한 Dongle 프리팹 배열
    public Transform spawnPoint; // 스폰 위치
    private GameObject currentPreview; // 현재 미리보기 오브젝트

    void Update()
    {
        // 숫자패드 입력 감지
        for (int i = 1; i <= 7; i++)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1 + (i - 1))) // Keypad1부터 Keypad7까지 감지
            {
                UpdatePreview(i - 1); // 미리보기 업데이트
            }
        }
    }

    private void UpdatePreview(int index)
    {
        // 기존 미리보기 오브젝트 삭제
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        // 새로운 미리보기 오브젝트 생성
        if (index >= 0 && index < donglePrefabs.Length)
        {
            currentPreview = Instantiate(donglePrefabs[index], spawnPoint.position, Quaternion.identity);
            currentPreview.transform.localScale *= 0.8f; // 미리보기 크기 조정
            currentPreview.GetComponent<Collider2D>().enabled = false; // 충돌 비활성화
            currentPreview.GetComponent<Rigidbody2D>().simulated = false; // 물리 비활성화
        }
        else
        {
            Debug.LogWarning("Invalid Dongle index: " + index);
        }
    }
}
