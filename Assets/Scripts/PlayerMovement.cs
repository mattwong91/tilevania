using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [Header("Player Physics")]
  [SerializeField] float runSpeed = 5f;
  [SerializeField] float jumpSpeed = 5f;
  [SerializeField] float climbSpeed = 3f;
  [SerializeField] Vector2 deathKick = new Vector2(5f, 10f);
  [SerializeField] float horizontalKnockback = 20f;
  [SerializeField] float verticalKnockback = 20f;

  [Header("Weapon Objects")]
  [SerializeField] GameObject bullet;
  [SerializeField] Transform weapon;

  [Header("SFX files")]
  [SerializeField] AudioClip hitAudio;
  [SerializeField] AudioClip spikeAudio;
  [SerializeField] AudioClip shootAudio;
  [SerializeField] AudioClip jumpAudio;
  [SerializeField] AudioClip bounceAudio;

  Vector2 moveInput;
  Rigidbody2D rb2D;
  Animator playerAnimator;
  CapsuleCollider2D bodyCollider;
  BoxCollider2D feetCollider;
  Health playerHealth;
  float gravityScaleAtStart;

  bool isAlive = true;
  bool isShooting = false;
  bool isRunning = false;

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
    if (!isAlive || isShooting) { return; }
    moveInput = value.Get<Vector2>();
  }

  void OnJump(InputValue value)
  {
    if (!isAlive || isShooting) { return; }
    if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
    if (value.isPressed)
    {
      rb2D.velocity += new Vector2(0f, jumpSpeed);
      PlaySFX(jumpAudio);
    }
  }

  void OnFire(InputValue value)
  {
    if (!isAlive || isShooting || isRunning) { return; }
    isShooting = true;
    playerAnimator.SetTrigger("Shoot");
  }

  public void DoneShooting()
  {
    isShooting = false;
  }

  public void ShootArrow()
  {
    PlaySFX(shootAudio);
    Instantiate(bullet, weapon.position, transform.rotation);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    Collider2D sourceCollider = other.GetContact(0).otherCollider;
    if (sourceCollider == feetCollider)
    {
      if (other.gameObject.tag == "Bounce")
      {
        PlaySFX(bounceAudio);
      }
    }
  }

  void Run()
  {
    Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb2D.velocity.y);
    rb2D.velocity = playerVelocity;

    isRunning = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;
    playerAnimator.SetBool("isRunning", isRunning);

  }

  void FlipSprite()
  {
    isRunning = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;

    if (isRunning)
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
      playerHealth.ModifyScore();
    }
  }

}
