using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  // NOTE not sure if isPlayer is needed in this case
  [SerializeField] bool isPlayer;
  [SerializeField] int health = 4;
  [SerializeField] AudioClip hitAudio;
  Rigidbody2D rb2D;

  public int GetHealth() { return health; }

  void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
  }

  public void TakeDamage()
  {
    AudioSource.PlayClipAtPoint(hitAudio, Camera.main.transform.position);
    health--;
    if (health < 0) { health = 0; }
    Debug.Log($"{gameObject.name} has {health} health left");
  }

  public void TakeDamage(int damage)
  {
    AudioSource.PlayClipAtPoint(hitAudio, Camera.main.transform.position);
    health -= damage;
    if (health < 0) { health = 0; }
    Debug.Log($"{gameObject.name} has {health} health left");
  }

}
