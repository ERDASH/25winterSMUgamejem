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
    private const int maxLevel = 7; // 최대 레벨 설정
    public bool isGrounded = false; // 바닥 또는 다른 Dongle 위에 닿아있는지 여부를 나타내는 플래그

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

        // 다음 단계의 Dongle 스폰
        if (nextLevelPrefab != null)
        {
            Instantiate(nextLevelPrefab, spawnPosition, Quaternion.identity);
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
