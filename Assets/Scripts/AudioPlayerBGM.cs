using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerBGM : MonoBehaviour
{
  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioClip mainTheme;
  [SerializeField] AudioClip bossTheme;

  void Awake()
  {
    int numBGMPlayers = FindObjectsOfType<AudioPlayerBGM>().Length;
    if (numBGMPlayers > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }

  public void SetBossTheme()
  {
    audioSource.Stop();
    audioSource.clip = bossTheme;
    audioSource.Play();
  }

  public void SetMainTheme()
  {
    audioSource.Stop();
    audioSource.clip = mainTheme;
    audioSource.Play();
  }
}
