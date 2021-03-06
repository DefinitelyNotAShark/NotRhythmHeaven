﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlayerStateChange : MonoBehaviour {

    private SetSprite setSprite;

    private string pose1Name;
    private string pose2Name;
    private string pose3Name;
    private string pose4Name;

    private bool animationIsStarted = false;
    private bool canStartCoroutine = true;
    public static bool canDoFourthPose = false;


    private float pose1Input;
    private float pose2Input;
    private float pose3Input;
    private float pose4Input;

    [SerializeField]
    PostProcessingProfile ppProfile;

    [SerializeField]
    AudioClip pose1Sound;

    [SerializeField]
    AudioClip pose2Sound;

    [SerializeField]
    AudioClip pose3Sound;

    [SerializeField]
    AudioClip pose4Sound;

    AudioSource audioSource;

    ChromaticAberrationModel.Settings chromaticSettings;

    private void Start()
    {
        setSprite = GetComponent<SetSprite>();
        audioSource = GetComponent<AudioSource>();
        SetInputAxes();
    }

    private void Update()
    {
        GetButtonInput();
    }

    private void FixedUpdate()
    {
        SetStateBasedOnInput();
    }

    void SetInputAxes()//only call once
    {
        pose1Name = "pose1";
        pose2Name = "pose2";
        pose3Name = "pose3";
        pose4Name = "pose4";
    }

    void GetButtonInput()
    {
        pose1Input = Input.GetAxis(pose1Name);
        pose2Input = Input.GetAxis(pose2Name);
        pose3Input = Input.GetAxis(pose3Name);
        pose4Input = Input.GetAxis(pose4Name);
    }

    void SetStateBasedOnInput()
    {
        if (pose1Input == 1)//shouldn't get input if the animation is going or if player is holding pose2
        {
            audioSource.clip = pose1Sound;

            if(!audioSource.isPlaying)
            audioSource.Play();
            setSprite.State = SetSprite.SpriteState.pose1;
        }

        else if (pose2Input == 1)//set state to animation state if it's not already running   
        {
            audioSource.clip = pose2Sound;


            if (!audioSource.isPlaying)
                audioSource.Play();
            setSprite.State = SetSprite.SpriteState.pose2;
        }

        //state = PlayerState.animationBeforePose2;//this is set to animation instead of pose 2

        else if (pose3Input == 1)
        {
            audioSource.clip = pose3Sound;

            if (!audioSource.isPlaying)
                audioSource.Play();
            setSprite.State = SetSprite.SpriteState.pose3;
        }

        else if (pose4Input == 1 && canDoFourthPose)
        {
            audioSource.clip = pose4Sound;

            setSprite.State = SetSprite.SpriteState.pose4;

            if (ppProfile != null)
                chromaticSettings = ppProfile.chromaticAberration.settings;
            Debug.Log("Trying to do it");

            if (chromaticSettings.intensity < 1)
                chromaticSettings.intensity += .1f;

            if (ppProfile != null)
                ppProfile.chromaticAberration.settings = chromaticSettings;

            if (!audioSource.isPlaying)
                audioSource.Play();
        }

        else if (!animationIsStarted)
        {
            setSprite.State = SetSprite.SpriteState.normal;

            if (ppProfile != null)
                chromaticSettings = ppProfile.chromaticAberration.settings;

            Debug.Log("Trying to do it");
            chromaticSettings.intensity = 0;

            if (ppProfile != null)
                ppProfile.chromaticAberration.settings = chromaticSettings;
        }
    }












    //private IEnumerator AnimationBeforePose2()
    //{
    //    //start animation here
    //    yield return new WaitForSeconds(2);//when animation is done
    //    StartCoroutine(ChangeToPose2AtEndOfAnimation());//after animation go to the pose 2 coroutine;
    //}

    //private IEnumerator ChangeToPose2AtEndOfAnimation()
    //{
    //    //holdingPose2 = true;
    //    state = PlayerState.pose2;//set the pose to pose2
    //    yield return new WaitForSeconds(1);//hold pose 2 for 1 second
    //    animationIsStarted = false;//able to do the animation again!
    //}
}
