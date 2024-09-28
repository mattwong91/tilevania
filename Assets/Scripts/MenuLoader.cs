using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
  public void StartGame()
  {
    SceneManager.LoadScene(1);
  }

  public void ExitGame()
  {
    Debug.Log("Qutting game.");
    Application.Quit();
  }
}
