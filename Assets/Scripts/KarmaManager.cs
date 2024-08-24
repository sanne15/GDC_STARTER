using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KarmaManager : MonoBehaviour
{
    public int humane_affinity { get; set; }

    void Awake()
    {
        humane_affinity = 0;
    }

    public void AddHumaneAffinity(int value)
    {
        humane_affinity += value;
    }

    public void MakeEnding(int num)
    {
        //���� �� ����
        switch(num)
        {
            case 0: // ���󿣵�����
                break;
            case 1: // �ش��� �ΰ�������
                SceneManager.LoadScene("Ending1");
                break;
            case 2: // �ش��� �κ���ȣ��
                SceneManager.LoadScene("Ending1");
                break;
        }

        //��� ������ ����

        if (humane_affinity >= 50) // ��Ÿ�� �ΰ� ��ȣ
        {
            SceneManager.LoadScene("Ending1");
        }
        else if (humane_affinity >= 30) // �ΰ� ��ȣ
        {
            SceneManager.LoadScene("Ending1");
        }
        else if (humane_affinity > 0) // ����� �����
        {
            SceneManager.LoadScene("Ending1");
        }
        else if (humane_affinity >= -30) // �κ� ��ȣ
        {
            SceneManager.LoadScene("Ending1");
        }
        else if (humane_affinity >= -50) // �κ� �����
        {
            SceneManager.LoadScene("Ending1");
        }
        else // �κ��� õ��
        {
            SceneManager.LoadScene("Ending1");
        }

        return;
    }
}
