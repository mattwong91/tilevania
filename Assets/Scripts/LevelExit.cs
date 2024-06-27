using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
  [SerializeField] float loadDelay = 1f;

  void OnTriggerEnter2D(Collider2D other)
  {
    StartCoroutine(LoadNextLevel());
  }

  IEnumerator LoadNextLevel()
  {
    yield return new WaitForSecondsRealtime(loadDelay);
    int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

    if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextLevelIndex = 0;
    }

    SceneManager.LoadScene(nextLevelIndex);
  }
}
