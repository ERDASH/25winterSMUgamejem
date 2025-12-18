// 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
// 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.


public class DongleMerge : MonoBehaviour
{
    public int level; // Dongle의 레벨
    public GameObject nextLevelPrefab; // 다음 레벨의 Dongle 프리팹
    private bool isMerging = false; // 중복 Merge 방지 플래그
    private const int maxLevel = 11; // 최대 레벨 설정
    public bool isGrounded = false; // 바닥 또는 다른 Dongle 위에 닿아있는지 여부를 나타내는 플래그
    public GameObject mergeEffectPrefab; // 병합 시 출력할 이펙트 프리팹

    public AudioSource audioSource; // AudioSource 컴포넌트
    public AudioClip mergeSound; // 병합 시 재생할 사운드

    private bool hasPlayed = false; // 사운드가 재생되었는지 확인하는 플래그

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았는지 확인
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.GetComponent<DongleMerge>() != null)
        {
            isGrounded = true;
        }

        // 최대 level 동글이 예외처리
        if (isMerging || level >= maxLevel) return;

        // 이미 병합 중이면 실행하지 않음
        if (isMerging) return;

        // 충돌한 객체가 Dongle인지 확인
        DongleMerge otherDongle = collision.gameObject.GetComponent<DongleMerge>();
        if (otherDongle != null && otherDongle.level == this.level && !otherDongle.isMerging)
        {
            // 병합 시작 플래그 설정
            isMerging = true;
            otherDongle.isMerging = true;

            // 병합 처리
            MergeDongles(otherDongle);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥 또는 다른 Dongle에서 떨어졌는지 확인
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.GetComponent<DongleMerge>() != null)
        {
            isGrounded = false;
        }
    }

    private void MergeDongles(DongleMerge otherDongle)
    {
        // 두 Dongle의 위치 평균 계산
        Vector3 spawnPosition = (this.transform.position + otherDongle.transform.position) / 2;

        // 병합 이펙트 출력
        if (mergeEffectPrefab != null)
        {
            GameObject mergeEffect = Instantiate(mergeEffectPrefab, spawnPosition, Quaternion.identity);
            Destroy(mergeEffect, 1f); // 생성된 병합 이펙트를 1초 후에 삭제
        }
        else
        {
            Debug.LogWarning("Merge effect prefab is not assigned!");
        }

        // 병합 시 사운드 재생
        if (!hasPlayed)
        {
            AudioSource.PlayClipAtPoint(mergeSound, spawnPosition);
            hasPlayed = true;
        }


        // 다음 단계의 Dongle 스폰
        if (nextLevelPrefab != null)
        {
            GameObject newDongle = Instantiate(nextLevelPrefab, spawnPosition, Quaternion.identity);

            // Prefab_spawner의 최대 스폰 레벨 업데이트
            Prefab_spawner spawner = FindObjectOfType<Prefab_spawner>();
            if (spawner != null)
            {
                DongleMerge newDongleMerge = newDongle.GetComponent<DongleMerge>();
                if (newDongleMerge != null)
                {
                    spawner.UpdateMaxSpawnLevel(newDongleMerge.level);
                }
            }
        }
        else
        {
            Debug.LogError("Next level prefab is not assigned!");
        }

        // 기존 Dongle 삭제
        Destroy(otherDongle.gameObject);
        Destroy(this.gameObject);
    }
}
