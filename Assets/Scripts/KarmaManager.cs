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
        //진행 중 엔딩
        switch(num)
        {
            case 0: // 정상엔딩으로
                break;
            case 1: // 극단적 인간주의자
                SceneManager.LoadScene("Ending1");
                break;
            case 2: // 극단적 로봇옹호가
                SceneManager.LoadScene("Ending1");
                break;
        }

        //모두 끝나고 엔딩

        if (humane_affinity >= 50) // 배타적 인간 우호
        {
            SceneManager.LoadScene("Ending1");
        }
        else if (humane_affinity >= 30) // 인간 우호
        {
            SceneManager.LoadScene("Ending1");
        }
        else if (humane_affinity > 0) // 평범한 사장님
        {
            SceneManager.LoadScene("Ending1");
        }
        else if (humane_affinity >= -30) // 로봇 우호
        {
            SceneManager.LoadScene("Ending1");
        }
        else if (humane_affinity >= -50) // 로봇 사랑꾼
        {
            SceneManager.LoadScene("Ending1");
        }
        else // 로봇의 천하
        {
            SceneManager.LoadScene("Ending1");
        }

        return;
    }
}
