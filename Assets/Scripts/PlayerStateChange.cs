using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateChange : MonoBehaviour {

    public enum PlayerState { normal, pose1, pose2, pose3}

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
        SetState();
        Debug.Log(state.ToString());
        ChangePlayerBasedOnState();
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

    void SetState()
    {
        if (pose1 == 1)
            state = PlayerState.pose1;

        else if (pose2 == 1)
            state = PlayerState.pose2;

        else if (pose3 == 1)
            state = PlayerState.pose3;

        else
            state = PlayerState.normal;
    }

    void ChangePlayerBasedOnState()
    {
        if (state == PlayerState.pose1)
            spriteRenderer.sprite = pose1Sprite;

        else if (state == PlayerState.pose2)
            spriteRenderer.sprite = pose2Sprite;

        else if (state == PlayerState.pose3)
            spriteRenderer.sprite = pose3Sprite;

        else if (state == PlayerState.normal)
            spriteRenderer.sprite = normalSprite;
    }
}
