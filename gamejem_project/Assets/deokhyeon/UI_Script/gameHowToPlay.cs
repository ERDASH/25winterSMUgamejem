using UnityEngine;

public class gameHowToPlay : MonoBehaviour
{
    public GameObject start; // start 게임오브젝트를 드래그 앤 드롭으로 연결하세요.
    public GameObject HotToPlay_1; // HotToPlay_1 게임오브젝트를 드래그 앤 드롭으로 연결하세요.

    public void HowtpPlay()
    {
    if (start != null)
    {
    start.SetActive(false); // start 게임오브젝트 비활성화
    }

    if (HotToPlay_1 != null)
    {
    HotToPlay_1.SetActive(true); // HotToPlay_1 게임오브젝트 활성화
    }
    }
}
