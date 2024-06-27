using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
  [SerializeField] AudioClip coinAudio;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      AudioSource.PlayClipAtPoint(coinAudio, Camera.main.transform.position);
      Destroy(gameObject);
    }
  }
}
