using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSettings : MonoBehaviour
{
    public float rotationSpeed = 100f;  // �������� ȸ���ϴ� �ӵ�

    void Update()
    {
        // �� �����Ӹ��� �������� ȸ����ŵ�ϴ�.
        // transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
