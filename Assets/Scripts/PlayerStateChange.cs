using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateChange : MonoBehaviour {

    public enum PlayerState { normal, pose1, animationBeforePose2, pose2, pose3}

    [SerializeField]
    private Sprite normalSprite;

    [SerializeField]
    private Sprite pose1Sprite;

    [SerializeField]
    private Sprite pose2Sprite;

    [SerializeField]
    private Sprite pose3Sprite;

    private PlayerState state;
    public PlayerState State { get { return state; } }

    private string pose1Name;
    private string pose2Name;
    private string pose3Name;

    private bool animationIsStarted = false;

    private float pose1;
    private float pose2;
    private float pose3;  

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        state = PlayerState.normal;//starts out with normal expression/pose
        spriteRenderer = GetComponent<SpriteRenderer>();//get renderer
        spriteRenderer.sprite = normalSprite;//set default to normal;
        SetInputAxes();
    }

    private void Update()
    {
        GetButtonInput();
        ChangePlayerBasedOnState();
    }

    private void FixedUpdate()
    {
        SetStateBasedOnInput();
        Debug.Log(state.ToString());
        Debug.Log("animationIsStarted: " + animationIsStarted);
    }

    void SetInputAxes()//only call once
    {
        pose1Name = "pose1";
        pose2Name = "pose2";
        pose3Name = "pose3";
    }

    void GetButtonInput()
    {
        pose1 = Input.GetAxis(pose1Name);
        pose2 = Input.GetAxis(pose2Name);
        pose3 = Input.GetAxis(pose3Name);
    }

    void SetStateBasedOnInput()
    {
        if (pose1 == 1)//shouldn't get input if the animation is going or if player is holding pose2
            state = PlayerState.pose1;

        else if (pose2 == 1)//set state to animation state if it's not already running
        {
            state = PlayerState.animationBeforePose2;//this is set to animation instead of pose 2
            animationIsStarted = true;//set it right when the input comes in to avoid last minute input
        }

        else if (pose3 == 1)
            state = PlayerState.pose3;

        else if (!animationIsStarted)
            state = PlayerState.normal;
    }

    void ChangePlayerBasedOnState()//this is where the sprites are set
    {
        switch (state)
        {
            case PlayerState.pose1:
                spriteRenderer.sprite = pose1Sprite;
                break;

            case PlayerState.animationBeforePose2:
                StartCoroutine(AnimationBeforePose2());
                break;

            case PlayerState.pose2:
                spriteRenderer.sprite = pose2Sprite;
                break;

            case PlayerState.pose3:
                spriteRenderer.sprite = pose3Sprite;
                break;

            case PlayerState.normal:
                spriteRenderer.sprite = normalSprite;
                break;
        }

    }

    private IEnumerator AnimationBeforePose2()
    {
        //start animation here
        yield return new WaitForSeconds(2);//when animation is done
        StartCoroutine(ChangeToPose2AtEndOfAnimation());//after animation go to the pose 2 coroutine;
    }

    private IEnumerator ChangeToPose2AtEndOfAnimation()
    {
        //holdingPose2 = true;
        state = PlayerState.pose2;//set the pose to pose2
        yield return new WaitForSeconds(1);//hold pose 2 for 1 second
        animationIsStarted = false;//able to do the animation again!
    }
}
