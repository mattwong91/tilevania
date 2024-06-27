using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] float runSpeed = 5f;
  [SerializeField] float jumpSpeed = 5f;
  [SerializeField] float climbSpeed = 3f;
  [SerializeField] Vector2 deathKick = new Vector2(5f, 10f);
  [SerializeField] GameObject bullet;
  [SerializeField] Transform weapon;

  Vector2 moveInput;
  Rigidbody2D rb2D;
  Animator playerAnimator;
  CapsuleCollider2D bodyCollider;
  BoxCollider2D feetCollider;
  float gravityScaleAtStart;
  bool isAlive = true;

  void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
    playerAnimator = GetComponent<Animator>();
    bodyCollider = GetComponent<CapsuleCollider2D>();
    feetCollider = GetComponent<BoxCollider2D>();
    gravityScaleAtStart = rb2D.gravityScale;
  }


  void Update()
  {
    if (!isAlive) { return; }
    Run();
    FlipSprite();
    ClimbLadder();
    Die();
  }

  void OnMove(InputValue value)
  {
    if (!isAlive) { return; }
    moveInput = value.Get<Vector2>();
  }

  void OnJump(InputValue value)
  {
    if (!isAlive) { return; }
    if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
    if (value.isPressed)
    {
      rb2D.velocity += new Vector2(0f, jumpSpeed);
    }
  }

  void OnFire(InputValue value)
  {
    if (!isAlive) { return; }
    Instantiate(bullet, weapon.position, transform.rotation);
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

  void ClimbLadder()
  {
    if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
    {
      rb2D.gravityScale = gravityScaleAtStart;
      playerAnimator.SetBool("isClimbing", false);
      return;
    }

    rb2D.gravityScale = 0f;
    Vector2 climbVelocity = new Vector2(rb2D.velocity.x, moveInput.y * climbSpeed);
    rb2D.velocity = climbVelocity;

    bool hasVerticalSpeed = Mathf.Abs(rb2D.velocity.y) > Mathf.Epsilon;
    playerAnimator.SetBool("isClimbing", hasVerticalSpeed);
  }

  void Die()
  {
    if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
    {
      isAlive = false;
      playerAnimator.SetTrigger("Dying");
      rb2D.velocity = deathKick;
      FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
  }

}
