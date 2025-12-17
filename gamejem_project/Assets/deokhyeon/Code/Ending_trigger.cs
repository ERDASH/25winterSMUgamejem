using System;
using UnityEditor;
using UnityEngine;
using TMPro;

public class Ending_trigger : MonoBehaviour
{
    public GameEndManager gameManager;
    public GameObject clearUI;
    public AudioSource audioSource; // AudioSource 컴포넌트
    public AudioClip buttonSound; // Button.wav 오디오 클립
    public AudioClip endSound;
    public AudioClip yohooSound;
    public bool isGameCleared = false;

    private bool hasPlayed = false; // 사운드가 재생되었는지 확인하는 플래그
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

        isGameCleared = true;
        
        if (!hasPlayed)
        {
        // 사운드 재생
        audioSource.PlayOneShot(buttonSound);
        audioSource.PlayOneShot(yohooSound);
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(endSound);
        hasPlayed = true; // 사운드가 재생되었음을 표시
        }

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