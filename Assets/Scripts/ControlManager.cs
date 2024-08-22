using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public bool isCtrlPressed { get; private set; }

    void Update()
    {
        // Ctrl Ű �Է� ���� ����
        isCtrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    }
}
