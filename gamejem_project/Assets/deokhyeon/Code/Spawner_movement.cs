using UnityEngine;

public class Spawner_movement : MonoBehaviour
{
public float moveSpeed = 5f; // 이동 속도
public float minX = -7.5f; // X 좌표의 최소값
public float maxX = 7.5f; // X 좌표의 최대값

private GameEndManager gameEndManager;
private Ending_trigger endingTrigger;

private void Start()
{
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

private void Update()
{
    // 게임이 실행 중인지 확인
if (gameEndManager.isGameRunning == true && endingTrigger.isGameCleared == false) // isGameRunning이 true일 때만 이동
{
// 좌우 화살표 키를 사용하여 이동
float horizontalInput = Input.GetAxis("Horizontal");
Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
transform.Translate(movement);

// X 좌표를 제한
Vector3 clampedPosition = transform.position;
clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
transform.position = clampedPosition;
}
}
}