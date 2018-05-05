using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSprite : MonoBehaviour
{

    public enum SpriteState { normal, pose1, animationBeforePose2, pose2, pose3, pose4, happyExpression, angryExpression }

    private SpriteState state;
    public SpriteState State { get { return state; } set { state = value; } }

    public static SpriteState AiState;

    AudioSource audioSource;

    private Sprite normalSprite;//add these at start
    private Sprite pose1Sprite;
    private Sprite pose2Sprite;
    private Sprite pose3Sprite;
    private Sprite pose4Sprite;
    private Sprite happySprite;
    private Sprite angrySprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        state = SpriteState.normal;//starts out with normal expression/pose
        normalSprite = Resources.Load<Sprite>("MySonfilled");
        pose1Sprite =  Resources.Load<Sprite>("MySonPose1");
        pose2Sprite = Resources.Load<Sprite>("MySonPose2");
        pose3Sprite = Resources.Load<Sprite>("MySonPose3");
        pose4Sprite = Resources.Load<Sprite>("MySonPose4");
        happySprite = Resources.Load<Sprite>("MySonHappy");
        angrySprite = Resources.Load<Sprite>("MySonAngry");

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();//get renderer
        spriteRenderer.sprite = normalSprite;//set default to normal;

        if(this.GetComponent<AudioSource>() == null)
        {
            this.gameObject.AddComponent<AudioSource>();
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ChangePlayerSpriteBasedOnState();
    }

    public void ChangePlayerSpriteBasedOnState()//this is where the sprites are set
    {
        switch (state)
        {
            case SpriteState.pose1:
                spriteRenderer.sprite = pose1Sprite;
                break;

            case SpriteState.pose2:
                spriteRenderer.sprite = pose2Sprite;
                break;

            case SpriteState.pose3:
                spriteRenderer.sprite = pose3Sprite;
                break;

            case SpriteState.pose4:
                spriteRenderer.sprite = pose4Sprite;
                break;

            case SpriteState.normal:
                spriteRenderer.sprite = normalSprite;
                break;

            case SpriteState.angryExpression:
                spriteRenderer.sprite = angrySprite;
                break;

            case SpriteState.happyExpression:
                spriteRenderer.sprite = happySprite;
                break;
        }
    }
}
