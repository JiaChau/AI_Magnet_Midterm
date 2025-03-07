using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public RectTransform targetImage; // 캔버스에 있는 UI Image의 RectTransform
    public float radius = 100f; // 원의 반지름
    public float speed = 1f; // 회전 속도

    private float angle = 0f; // 현재 회전 각도

    void Update()
    {
        // 시간에 따라 각도 증가
        angle += speed * Time.deltaTime;

        // 새로운 x, y 위치 계산 (원의 중심은 부모 오브젝트의 위치)
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // 새로운 위치 설정
        targetImage.anchoredPosition = new Vector2(x, y);
    }
}
