using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerBGM : MonoBehaviour
{
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
}
