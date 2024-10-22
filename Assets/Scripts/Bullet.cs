using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField] float bulletSpeed = 15f;
  [SerializeField] AudioClip hitAudio;
  Rigidbody2D rb2D;
  PlayerMovement player;
  float xSpeed;

  void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
    player = FindObjectOfType<PlayerMovement>();
    xSpeed = player.transform.localScale.x * bulletSpeed;
  }

  void Update()
  {
    rb2D.velocity = new Vector2(xSpeed, 0f);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Enemy")
    {
      Health enemyHealth = other.GetComponentInParent<Health>();
      AudioSource.PlayClipAtPoint(hitAudio, Camera.main.transform.position);
      enemyHealth.TakeDamage();
      if (enemyHealth.GetHealth() <= 0)
      {
        enemyHealth.ModifyScore();
        Destroy(other.gameObject);
      }
    }
    Destroy(gameObject);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    Destroy(gameObject);
  }
}
