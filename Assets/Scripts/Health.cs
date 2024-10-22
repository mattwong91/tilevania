using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  [SerializeField] bool isBoss;
  [SerializeField] int maxHealth = 4;
  [Tooltip("Change in score upon defeat")]
  [SerializeField] int scoreModifier = 100;

  int health;
  HealthBar healthBar;
  LevelExit levelExit;

  public int GetHealth() { return health; }
  public int GetMaxHealth() { return maxHealth; }

  void Awake()
  {
    health = maxHealth;
    healthBar = GetComponentInChildren<HealthBar>();
    if (isBoss) { SetupBossLevel(); }
  }

  void SetupBossLevel()
  {
    levelExit = FindObjectOfType<LevelExit>();
    levelExit.GetComponent<SpriteRenderer>().enabled = false;
    levelExit.GetComponent<BoxCollider2D>().enabled = false;
  }

  public void TakeDamage()
  {
    health--;
    if (health < 0) { health = 0; }
    healthBar.UpdateHealthBar(health, maxHealth);
    if (isBoss && health <= 0)
    {
      if (levelExit == null) { levelExit = FindObjectOfType<LevelExit>(); }
      levelExit.GetComponent<SpriteRenderer>().enabled = true;
      levelExit.GetComponent<BoxCollider2D>().enabled = true;
    }
  }

  public void ModifyScore()
  {
    FindObjectOfType<GameSession>().IncreaseScore(scoreModifier);
  }

  public void ResetHealth()
  {
    health = maxHealth;
  }

}
