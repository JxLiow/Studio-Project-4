using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource buttonAudio;
    public void PlayButtonSound()
    {
        buttonAudio.Play();
    }
}

