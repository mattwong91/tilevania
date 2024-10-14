using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
  [SerializeField] AudioClip selectionAudio;

  public void StartGame()
  {
    AudioSource.PlayClipAtPoint(selectionAudio, Camera.main.transform.position);
    SceneManager.LoadScene(1);
  }

  public void ExitGame()
  {
    Debug.Log("Qutting game.");
    AudioSource.PlayClipAtPoint(selectionAudio, Camera.main.transform.position);
    Application.Quit();
  }
}
