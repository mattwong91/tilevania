using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  [SerializeField] float moveSpeed = 2f;
  [Tooltip("Delay upon making contact with player")]
  [SerializeField] float moveDelay = 0.5f;
  [Tooltip("Delay after being hit with bullet")]
  [SerializeField] float hitDelay = 0.5f;
  Rigidbody2D rb2D;
  bool isStopped = false;

  void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    if (isStopped) { return; }
    rb2D.velocity = new Vector2(moveSpeed, 0);
    FlipSprite();
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
    {
      moveSpeed = -moveSpeed;
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      StartCoroutine(PauseMovement(moveDelay));
    }
    if (other.gameObject.tag == "Bullet")
    {
      StartCoroutine(PauseMovement(hitDelay));
    }
  }

  void FlipSprite()
  {
    bool hasHorizontalSpeed = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;

    if (hasHorizontalSpeed)
    {
      transform.localScale = new Vector2(Mathf.Sign(rb2D.velocity.x), 1f);
    }
  }

  IEnumerator PauseMovement(float delay)
  {
    isStopped = true;
    rb2D.velocity = new Vector2(0f, 0f);
    yield return new WaitForSeconds(delay);
    isStopped = false;
  }
}
