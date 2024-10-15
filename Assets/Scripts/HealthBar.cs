using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField] Transform target;
  [SerializeField] Vector3 offset;
  [Tooltip("Time in seconds to wait before hiding the health bar")]
  [SerializeField] float showHideDelay = 1f;
  Slider healthBar;
  Canvas canvas;

  void Start()
  {
    healthBar = GetComponent<Slider>();
    canvas = GetComponentInParent<Canvas>();
    canvas.enabled = false;
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
    StartCoroutine(DisplayHealthBar());
  }

  IEnumerator DisplayHealthBar()
  {
    canvas.enabled = true;
    yield return new WaitForSeconds(showHideDelay);
    canvas.enabled = false;
  }
}
