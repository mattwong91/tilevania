using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  [SerializeField] float moveSpeed = 2f;
  [SerializeField] float moveDelay = 0.5f;
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
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag != "Player")
    {
      moveSpeed = -moveSpeed;
      FlipEnemyFacing();
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      StartCoroutine(PauseMovement(moveDelay));
    }
  }

  // TODO Create a method for knockback. Should be triggered when the other collider is a bullet tag.
  // Knockback should push the enemy back and then call pause movement

  IEnumerator PauseMovement(float delay)
  {
    isStopped = true;
    rb2D.velocity = new Vector2(0f, 0f);
    yield return new WaitForSeconds(delay);
    isStopped = false;
  }

  void FlipEnemyFacing()
  {
    transform.localScale = new Vector2(-Mathf.Sign(rb2D.velocity.x), 1f);
  }
}
