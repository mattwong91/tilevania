using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  [SerializeField] float moveSpeed = 2f;
  Rigidbody2D rb2D;
  public bool isHit = false;

  void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    if (isHit) { return; }
    rb2D.velocity = new Vector2(moveSpeed, 0);
  }

  void OnTriggerExit2D(Collider2D other)
  {
    moveSpeed = -moveSpeed;
    FlipEnemyFacing();
  }

  void FlipEnemyFacing()
  {
    transform.localScale = new Vector2(-Mathf.Sign(rb2D.velocity.x), 1f);
  }
}
