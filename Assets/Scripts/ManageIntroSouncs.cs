using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageIntroSouncs : MonoBehaviour {

    [SerializeField]
    AudioClip mainMusicClip;

    AudioSource audioSource;

    bool loopBreak = false;

	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(WaitForApplauseToFinish());
	}

    private IEnumerator WaitForApplauseToFinish()
    {
        while (loopBreak == false)
        {
            yield return new WaitForSeconds(.5f);

            if (!audioSource.isPlaying)
            {
                audioSource.volume = .4f;
                audioSource.clip = mainMusicClip;
                audioSource.Play();
                loopBreak = true;
            }
        }
    }
}
