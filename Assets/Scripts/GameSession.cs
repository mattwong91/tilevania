using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
  [SerializeField] int playerLives = 3;
  [SerializeField] TextMeshProUGUI livesText;
  [SerializeField] TextMeshProUGUI scoreText;
  [SerializeField] float deathDelay = 1.5f;
  int playerScore = 0;


  void Awake()
  {
    int numGameSessions = FindObjectsOfType<GameSession>().Length;
    if (numGameSessions > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }

  void Start()
  {
    livesText.text = playerLives.ToString();
    scoreText.text = playerScore.ToString();
  }

  public void ProcessPlayerDeath()
  {
    if (playerLives > 1)
    {
      Invoke("TakeLife", deathDelay);
    }
    else
    {
      Invoke("ResetGameSession", deathDelay);
    }
  }

  void ResetGameSession()
  {
    FindObjectOfType<ScenePersist>().ResetScenePersist();
    SceneManager.LoadScene(0);
    Destroy(gameObject);
  }

  void TakeLife()
  {
    playerLives--;
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
    livesText.text = playerLives.ToString();
  }

  public void IncreaseScore(int scorePoints)
  {
    playerScore += scorePoints;
    scoreText.text = playerScore.ToString();
  }
}
