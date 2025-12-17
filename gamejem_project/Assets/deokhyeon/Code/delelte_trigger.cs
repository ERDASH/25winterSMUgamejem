using UnityEngine;

public class delelte_trigger : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
  // 트리거에 닿은 오브젝트를 삭제
  if (collision.CompareTag("Clone")) // 클론 오브젝트에 "Clone" 태그를 설정해야 합니다.
  {
    Destroy(collision.gameObject);
  }
  }
}
