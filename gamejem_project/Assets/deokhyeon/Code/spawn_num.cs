using UnityEngine;

public class spawn_num : MonoBehaviour
{
 
    public GameObject[] donglePrefabs; // Dongle 프리팹 배열
    public Transform spawnPoint; // Dongle이 스폰될 위치

    void Update()
    {
    // 숫자패드 입력 감지
    for (int i = 1; i <= 9; i++)
    {
    if (Input.GetKeyDown(KeyCode.Keypad1 + (i - 1))) // Keypad1부터 Keypad7까지 감지
    {
    SpawnDongle(i - 1); // 배열 인덱스는 0부터 시작하므로 i-1
    }
    }
    }

    private void SpawnDongle(int index)
    {
    if (index >= 0 && index < donglePrefabs.Length)
    {
    // Dongle 스폰
    Instantiate(donglePrefabs[index], spawnPoint.position, Quaternion.identity);
    }
    else
    {
    Debug.LogWarning("Invalid Dongle index: " + index);
    }
    }
}
