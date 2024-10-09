using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  Health health;
  Slider healthBar;

  void Start()
  {
    healthBar = GetComponent<Slider>();
    health = GetComponentInParent<Health>();
  }

  public void UpdateHealthBar()
  {
    healthBar.value = (float)health.GetHealth() / health.GetMaxHealth();
  }
}
