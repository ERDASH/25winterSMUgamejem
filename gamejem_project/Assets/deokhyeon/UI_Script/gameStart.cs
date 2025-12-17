using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameStart : MonoBehaviour
{
    private Button startButton;

    private void Awake()
    {
    // 버튼 컴포넌트를 가져옵니다.
    startButton = GetComponent<Button>();

    // 버튼 클릭 이벤트에 메서드를 등록합니다.
    startButton.onClick.AddListener(startGame);
    }

    private void startGame()
    {
    // MainGame 씬을 다시 로드하여 게임을 재시작합니다.
    SceneManager.LoadScene("MainGame");
    }

    private void OnDestroy()
    {
    // 버튼 클릭 이벤트에서 메서드를 제거합니다.
    startButton.onClick.RemoveListener(startGame);
    }
}
