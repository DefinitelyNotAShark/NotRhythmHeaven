using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSprite : MonoBehaviour
{

    public enum SpriteState { normal, pose1, animationBeforePose2, pose2, pose3 }

    private SpriteState state;
    public SpriteState State { get { return state; } set { state = value; } }

    private Sprite normalSprite;//add these at start
    private Sprite pose1Sprite;
    private Sprite pose2Sprite;
    private Sprite pose3Sprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        state = SpriteState.normal;//starts out with normal expression/pose
        normalSprite = Resources.Load<Sprite>("NunNormal");
        pose1Sprite =  Resources.Load<Sprite>("NunPose1");
        pose2Sprite = Resources.Load<Sprite>("NunPose2");
        pose3Sprite = Resources.Load<Sprite>("NunPose3");


        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();//get renderer
        spriteRenderer.sprite = normalSprite;//set default to normal;
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

            case SpriteState.normal:
                spriteRenderer.sprite = normalSprite;
                break;
        }
    }
}
