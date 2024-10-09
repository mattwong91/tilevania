using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  // NOTE not sure if isPlayer is needed in this case
  [SerializeField] bool isPlayer;
  [SerializeField] int maxHealth = 4;

  int health;

  public int GetHealth() { return health; }
  public int GetMaxHealth() { return maxHealth; }

  void Awake()
  {
    health = maxHealth;
  }

  public void TakeDamage()
  {
    health--;
    if (health < 0) { health = 0; }
    GetComponentInChildren<HealthBar>().UpdateHealthBar();
    Debug.Log($"{gameObject.name} has {health} health left");
  }

  public void TakeDamage(int damage)
  {
    health -= damage;
    if (health < 0) { health = 0; }
    GetComponentInChildren<HealthBar>().UpdateHealthBar();
    Debug.Log($"{gameObject.name} has {health} health left");
  }

  public void ResetHealth()
  {
    health = maxHealth;
  }

}
