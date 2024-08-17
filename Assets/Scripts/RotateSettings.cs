using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSettings : MonoBehaviour
{
    public float rotationSpeed = 100f;  // 아이콘이 회전하는 속도

    void Update()
    {
        // 매 프레임마다 아이콘을 회전시킵니다.
        // transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
