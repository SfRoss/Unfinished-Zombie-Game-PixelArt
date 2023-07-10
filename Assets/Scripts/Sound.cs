using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 3f)]
    public float volume;
    [Range(0f, 3f)]
    public float pitch;

    public bool looping = false;

    [HideInInspector]
    public AudioSource source;
}
