using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
  [SerializeField] AudioClip coinAudio;
  [SerializeField] int scoreValue = 100;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      AudioSource.PlayClipAtPoint(coinAudio, Camera.main.transform.position);
      FindObjectOfType<GameSession>().IncreaseScore(scoreValue);
      Destroy(gameObject);
    }
  }
}
