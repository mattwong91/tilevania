using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField] Transform target;
  [SerializeField] Vector3 offset;
  Slider healthBar;

  void Start()
  {
    healthBar = GetComponent<Slider>();
  }

  void Update()
  {
    PositionHealthBar();
  }

  void PositionHealthBar()
  {
    transform.rotation = Camera.main.transform.rotation;
    if (target == null)
    {
      Destroy(gameObject);
    }
    else
    {
      transform.position = target.position + offset;
    }
  }
  public void UpdateHealthBar(float currentHealth, float maxHealth)
  {
    healthBar.value = currentHealth / maxHealth;
  }
}
