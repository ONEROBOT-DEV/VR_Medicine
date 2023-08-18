using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private float timeToSmoothStart;
    [SerializeField] private AudioSource audioSource;

    public void SmoothStartPlay()
    {
        StartCoroutine(SmoothPlay());
    }

    private IEnumerator SmoothPlay()
    {
        var timer = 0f;
        audioSource.volume = 0;
        audioSource.Play();

        while (timer < timeToSmoothStart)
        {
            yield return null;
            timer += Time.deltaTime;

            audioSource.volume = Mathf.Clamp01(timer / timeToSmoothStart);
        }
        audioSource.volume = 1;
    }
}
