using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public void StartGame()
  {
    SceneManager.LoadScene("GameScene");
  }

  public void LoadGame()
  {
    Debug.Log("���� �̱���...");
  }

  public void EndingCompilation()
  {
    SceneManager.LoadScene("EndingCompilation");
  }

  public void OpenSettings()
  {
    SceneManager.LoadScene("Settings");
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
