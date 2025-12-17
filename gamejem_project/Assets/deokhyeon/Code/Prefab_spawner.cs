// 2025-12-17 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using static UnityEngine.Random; // UnityEngine.Random을 명시적으로 사용

public class Prefab_spawner : MonoBehaviour
{
    public GameObject[] donglePrefabs; // 스폰 가능한 Dongle 프리팹 배열
    public Transform spawnPoint; // 스폰 위치
    public GameObject currentPreview; // 현재 미리보기 오브젝트
    public int maxSpawnLevel = 0;
    public AudioSource audioSource; // AudioSource 컴포넌트
    public AudioClip spawnSound; // 소환 시 재생할 사운드

    public float spawnCooldown = 1.0f; // 쿨타임 (초)
    private float lastSpawnTime = -999f; // 마지막 스폰 시간


    private GameEndManager gameEndManager;
    private Ending_trigger endingTrigger;

    private void Start()
    {
        // 초기 미리보기 오브젝트 생성
        SpawnPreview();

    // GameManager 오브젝트에서 GameEndManager 컴포넌트를 가져옵니다.
    GameObject gameManager = GameObject.Find("GameManager");
    if (gameManager != null)
    {
    gameEndManager = gameManager.GetComponent<GameEndManager>();
    }

    // ending_trigger 오브젝트에서 Ending_trigger 컴포넌트를 가져옵니다.
    GameObject endingTriggerObject = GameObject.Find("ending_trigger");
    if (endingTriggerObject != null)
    {
    endingTrigger = endingTriggerObject.GetComponent<Ending_trigger>();
    }
    }

    void Update()
    {
        if (currentPreview != null)
        {
            currentPreview.transform.position = spawnPoint.position;
        }

        // 스페이스바를 누르면 현재 미리보기 오브젝트를 떨어뜨리고 새로운 미리보기 생성
        if (Input.GetKeyDown(KeyCode.Space) && CanSpawn())
        {
            DropDongle();
            SpawnPreview();
            lastSpawnTime = Time.time;
        }
    }

    private bool CanSpawn()
    {
        if (gameEndManager.isGameRunning == true && endingTrigger.isGameCleared == false)
        {
            return Time.time >= lastSpawnTime + spawnCooldown;
        }
        else
        {
            return false;
        }
    }

    private void SpawnPreview()
    {
        // 랜덤으로 Dongle 선택 (현재 스폰 가능한 최대 레벨까지)
    int randomIndex = Range(0, maxSpawnLevel + 1); // maxSpawnLevel을 포함하여 랜덤 선택

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
            // 소환 시 사운드 재생
            if (audioSource != null && spawnSound != null)
            {
                AudioSource.PlayClipAtPoint(spawnSound, spawnPoint.position);
            }
            // 물리 효과 활성화
            currentPreview.GetComponent<Collider2D>().enabled = true;
            currentPreview.GetComponent<Rigidbody2D>().simulated = true;

            // 미리보기 오브젝트 초기화
            currentPreview = null;
        }
    }

    public void UpdateMaxSpawnLevel(int newLevel)
    {
        // 최대 스폰 레벨 업데이트
        if (newLevel > maxSpawnLevel)
        {
            maxSpawnLevel = newLevel;
        }
    }
}