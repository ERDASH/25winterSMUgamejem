// 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using static UnityEngine.Random; // UnityEngine.Random을 명시적으로 사용

public class Prefab_spawner : MonoBehaviour
{
    public GameObject[] donglePrefabs; // 스폰 가능한 Dongle 프리팹 배열
    public Transform spawnPoint; // 스폰 위치
    public GameObject currentPreview; // 현재 미리보기 오브젝트

    void Start()
    {
        // 초기 미리보기 오브젝트 생성
        SpawnPreview();
    }

    void Update()
    {
        if (currentPreview != null)
        {
        currentPreview.transform.position = spawnPoint.position;
        }

        // 스페이스바를 누르면 현재 미리보기 오브젝트를 떨어뜨리고 새로운 미리보기 생성
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropDongle();
            SpawnPreview();
        }
    }

    private void SpawnPreview()
    {
        // 랜덤으로 Dongle 선택
        int randomIndex = Range(0, donglePrefabs.Length); // UnityEngine.Random.Range 사용

        // 기존 미리보기 오브젝트 삭제
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        // 새로운 미리보기 오브젝트 생성
        currentPreview = Instantiate(donglePrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        currentPreview.transform.localScale *= 1f; // 미리보기 크기 조정
        currentPreview.GetComponent<Collider2D>().enabled = false; // 충돌 비활성화
        currentPreview.GetComponent<Rigidbody2D>().simulated = false; // 물리 비활성화
    }

    private void DropDongle()
    {
        if (currentPreview != null)
        {
            // 물리 효과 활성화
            currentPreview.GetComponent<Collider2D>().enabled = true;
            currentPreview.GetComponent<Rigidbody2D>().simulated = true;

            // 미리보기 오브젝트 초기화
            currentPreview = null;
        }
    }
}