using System;
using UnityEditor;
using UnityEngine;

public class Ending_trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D  other)
    
    {
        Debug.Log("Hit: " + other.name);
        // 충돌한 객체의 이름이 "JJOAYO"인지 확인
        if (other.CompareTag("Ai"))
        {
        // 디버그 메시지 출력
        Debug.Log("clear");
        }
    }
}
