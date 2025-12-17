using UnityEngine;

public class gameExit : MonoBehaviour
{
    public void ExitGame()
    {
        // 게임 종료
        Application.Quit();
        Debug.Log("게임이 종료되었습니다.");
    }
}
