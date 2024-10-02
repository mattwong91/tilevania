using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
  [SerializeField] float loadDelay = 1f;
  GameSession currentGameSession;
  PlayerInput playerInput;
  [SerializeField] AudioClip exitAudio;

  void Start()
  {
    currentGameSession = FindObjectOfType<GameSession>();
    playerInput = FindObjectOfType<PlayerInput>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    playerInput.enabled = false;
    AudioSource.PlayClipAtPoint(exitAudio, Camera.main.transform.position);
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

    FindObjectOfType<ScenePersist>().ResetScenePersist();
    if (nextLevelIndex == 0)
    {
      currentGameSession.EndGameSession();
    }
    SceneManager.LoadScene(nextLevelIndex);
  }
}
