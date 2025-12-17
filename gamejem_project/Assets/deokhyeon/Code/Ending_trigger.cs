using System;
using UnityEditor;
using UnityEngine;
using TMPro;

public class Ending_trigger : MonoBehaviour
{
    public GameEndManager gameManager;
    public GameObject clearUI;

    private bool doOnce = false;
    private float clearTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ai"))
            return;

        // 이미 실행됐으면 무시
        if (doOnce)
            return;

        doOnce = true;

        // UI 켜기
        if (clearUI != null)
            clearUI.SetActive(true);

        // 클리어 순간 시간 고정
        if (gameManager != null && gameManager.elapsedTimeText != null)
        {
            gameManager.isCleared = true;
            clearTime = gameManager.elapsedTime;
            gameManager.elapsedTimeText.text =
                $"Clear Time: {clearTime:F2} seconds";
        }
    }
}