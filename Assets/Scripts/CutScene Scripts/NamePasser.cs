using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamePasser : MonoBehaviour
{
    public static NamePasser Instance { get; private set; }  // 싱글톤 인스턴스

    public string playername;
    public string playersurname;
    public string playeraltername;

    void Awake()
    {

        // 싱글톤 패턴 적용
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);  // 중복된 인스턴스가 생기면 파괴
        }
    }

    // 다른 스크립트에서 접근할 수 있는 메서드 추가 (선택 사항)
    public void SetPlayerInfo(string name, string surname, string altername)
    {
        playername = name;
        playersurname = surname;
        playeraltername = altername;

        Debug.Log("Name set Success");
    }
}
