// 2025-12-18 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorPulse : MonoBehaviour
{
    private Image backgroundImage;
    public float pulseDuration = 3f; // 색상이 변화하는 데 걸리는 시간 (초)
    public Color darkColor = new Color(122f / 255f, 122f / 255f, 122f / 255f, 1f); // 어두운 색상 (RGB 122,122,122)
    public Color brightColor = new Color(1f, 1f, 1f, 1f); // 밝은 색상 (RGB 255,255,255)

    private void Awake()
    {
        // Image 컴포넌트를 가져옵니다.
        backgroundImage = GetComponent<Image>();
        if (backgroundImage == null)
        {
        Debug.LogError("Image 컴포넌트가 없습니다. background 오브젝트에 Image 컴포넌트를 추가하세요.");
        }
    }

    private void Update()
    {
        if (backgroundImage != null)
        {
        // 시간에 따라 색상을 변화시킵니다.
        float t = Mathf.PingPong(Time.time / pulseDuration, 1f);
        backgroundImage.color = Color.Lerp(darkColor, brightColor, t);
        }
    }
}
