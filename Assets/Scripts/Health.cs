using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  [SerializeField] bool isPlayer;
  [SerializeField] int health = 4;

  [SerializeField] AudioClip hitAudio;

  public int GetHealth() { return health; }

  public void TakeDamage()
  {
    if (isPlayer)
    {
      AudioSource.PlayClipAtPoint(hitAudio, Camera.main.transform.position);
    }
    health--;
    Debug.Log($"{gameObject.name} has {health} health left");
  }

}
