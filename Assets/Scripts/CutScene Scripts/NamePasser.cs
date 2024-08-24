using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamePasser : MonoBehaviour
{
    public static NamePasser Instance { get; private set; }  // �̱��� �ν��Ͻ�

    public string playername;
    public string playersurname;
    public string playeraltername;

    void Awake()
    {

        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);  // �ߺ��� �ν��Ͻ��� ����� �ı�
        }
    }

    // �ٸ� ��ũ��Ʈ���� ������ �� �ִ� �޼��� �߰� (���� ����)
    public void SetPlayerInfo(string name, string surname, string altername)
    {
        playername = name;
        playersurname = surname;
        playeraltername = altername;

        Debug.Log("Name set Success");
    }
}
