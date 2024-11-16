using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
  [SerializeField] Vector2 moveSpeed;
  [SerializeField] bool scrollLeft;

  Vector3 offset;
  float singleTextureWidth;

  void Start()
  {
    if (scrollLeft) { moveSpeed = -moveSpeed; }
    Sprite sprite = GetComponent<SpriteRenderer>().sprite;
    singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
  }

  void Update()
  {
    Scroll();
    ResetPosition();
  }

  void Scroll()
  {
    offset = moveSpeed * Time.deltaTime;
    transform.position += offset;
  }

  void ResetPosition()
  {
    if (Math.Abs(transform.position.x / transform.localScale.x) - singleTextureWidth > 0)
    {
      transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }
  }
}
