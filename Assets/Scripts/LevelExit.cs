using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
  [SerializeField] float loadDelay = 1f;
  [SerializeField] AudioClip exitAudio;

  GameSession currentGameSession;
  PlayerInput playerInput;
  AudioPlayerBGM audioPlayerBGM;

  void Start()
  {
    currentGameSession = FindObjectOfType<GameSession>();
    playerInput = FindObjectOfType<PlayerInput>();
    audioPlayerBGM = FindObjectOfType<AudioPlayerBGM>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    playerInput.enabled = false;
    AudioSource.PlayClipAtPoint(exitAudio, Camera.main.transform.position);
    StartCoroutine(LoadNextLevel());
  }

  IEnumerator LoadNextLevel()
  {
    if (currentGameSession == null)
    {
      currentGameSession = FindObjectOfType<GameSession>();
    }

    yield return new WaitForSecondsRealtime(loadDelay);
    int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

    if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextLevelIndex = 0;
    }

    FindObjectOfType<ScenePersist>().ResetScenePersist();
    if (nextLevelIndex == SceneManager.sceneCountInBuildSettings - 1)
    {
      audioPlayerBGM.SetBossTheme();
    }
    if (nextLevelIndex == 0)
    {
      currentGameSession.EndGameSession();
    }
    SceneManager.LoadScene(nextLevelIndex);
  }
}
