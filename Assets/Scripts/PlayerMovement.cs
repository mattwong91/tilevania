using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] float runSpeed = 5f;
  [SerializeField] float jumpSpeed = 5f;
  [SerializeField] float climbSpeed = 3f;
  [SerializeField] Vector2 deathKick = new Vector2(5f, 10f);
  [SerializeField] float horizontalKnockback = 20f;
  [SerializeField] float verticalKnockback = 20f;
  [SerializeField] GameObject bullet;
  [SerializeField] Transform weapon;
  [SerializeField] AudioClip hitAudio;
  [SerializeField] AudioClip spikeAudio;
  [SerializeField] AudioClip shootAudio;

  Vector2 moveInput;
  Rigidbody2D rb2D;
  Animator playerAnimator;
  CapsuleCollider2D bodyCollider;
  BoxCollider2D feetCollider;
  Health playerHealth;
  float gravityScaleAtStart;
  bool isAlive = true;

  void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
    playerAnimator = GetComponent<Animator>();
    bodyCollider = GetComponent<CapsuleCollider2D>();
    feetCollider = GetComponent<BoxCollider2D>();
    gravityScaleAtStart = rb2D.gravityScale;
    playerHealth = GetComponentInParent<Health>();
  }


  void Update()
  {
    if (!isAlive) { return; }
    Run();
    FlipSprite();
    ClimbLadder();
    Die();
  }

  void FixedUpdate()
  {
    ProcessDamage();
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
    PlaySFX(shootAudio);
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

  void ProcessDamage()
  {
    if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
    {
      ApplyKnockback(horizontalKnockback, 0);
      PlaySFX(hitAudio);
      playerHealth.TakeDamage();
    }
    if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
    {
      ApplyKnockback(0, verticalKnockback);
      PlaySFX(spikeAudio);
      playerHealth.TakeDamage();
    }
  }

  void ApplyKnockback(float x, float y)
  {
    if (Mathf.Sign(transform.localScale.x) > 0)
    {
      rb2D.velocity = new Vector2(-Math.Abs(x), y);
    }
    else
    {
      rb2D.velocity = new Vector2(Math.Abs(x), y);
    }
  }

  void PlaySFX(AudioClip clip)
  {
    AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
  }

  void Die()
  {
    if (playerHealth.GetHealth() <= 0)
    {
      isAlive = false;
      playerAnimator.SetTrigger("Dying");
      ApplyKnockback(deathKick.x, deathKick.y);
      FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
  }

}
