using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BtnClick()
    {
        audioSource.Play(); 
    }
}
