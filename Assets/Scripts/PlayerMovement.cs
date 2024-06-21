using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] float runSpeed = 5f;
  [SerializeField] float jumpSpeed = 5f;

  Vector2 moveInput;
  Rigidbody2D rb2D;
  Animator playerAnimator;
  CapsuleCollider2D playerCollider;

  void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
    playerAnimator = GetComponent<Animator>();
    playerCollider = GetComponent<CapsuleCollider2D>();
  }


  void Update()
  {
    Run();
    FlipSprite();
  }

  void OnMove(InputValue value)
  {
    moveInput = value.Get<Vector2>();
    Debug.Log(moveInput);
  }

  void OnJump(InputValue value)
  {
    if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
    if (value.isPressed)
    {
      rb2D.velocity += new Vector2(0f, jumpSpeed);
    }
  }

  void Run()
  {
    Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb2D.velocity.y);
    rb2D.velocity = playerVelocity;

    bool hasHorizontalSpeed = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;
    playerAnimator.SetBool("isRunning", hasHorizontalSpeed);

  }

  void FlipSprite()
  {
    bool hasHorizontalSpeed = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;

    if (hasHorizontalSpeed)
    {
      transform.localScale = new Vector2(Mathf.Sign(rb2D.velocity.x), 1f);
    }
  }

}
