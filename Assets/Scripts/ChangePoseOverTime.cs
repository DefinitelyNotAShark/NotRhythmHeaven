using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ChangePoseOverTime : MonoBehaviour {

    [SerializeField]
    private float timeToWait;

    [SerializeField]
    private float timeToReact;

    [SerializeField]
    private float timeBetweenAiChange;

    [SerializeField]
    private float timeBetweenAiAndPlayer;

    [SerializeField]
    private float timeForReactionExpression;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    AudioClip correctSound;

    [SerializeField]
    AudioClip wrongSound;

    AudioSource audioSource;

    [SerializeField]
    PostProcessingProfile ppProfile;

    VignetteModel.Settings vignetteSettings;

    public static bool canStartMainCoroutine;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();      
        StartCoroutine(MainCoroutine());
    }

    IEnumerator MainCoroutine()//this is the big function that will set all the moves for the game!//this is hard coded. Change to make better
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeEachAIStateOverTime(timeBetweenAiChange, SetSprite.SpriteState.pose1));
        yield return new WaitForSeconds(5);
        StartCoroutine(ChangeEachAIStateOverTime(timeBetweenAiChange, SetSprite.SpriteState.pose2));
        yield return new WaitForSeconds(5);
        StartCoroutine(ChangeEachAIStateOverTime(timeBetweenAiChange, SetSprite.SpriteState.pose3));
        yield return new WaitForSeconds(6);
        StartCoroutine(ChangeStatesOverTimeTwoPositions(timeBetweenAiChange, SetSprite.SpriteState.pose2, SetSprite.SpriteState.pose3));
        yield return new WaitForSeconds(6);
        StartCoroutine(ChangeStatesOverTimeTwoPositions(timeBetweenAiChange, SetSprite.SpriteState.pose1, SetSprite.SpriteState.pose2));
        yield return new WaitForSeconds(6);
        StartCoroutine(ChangeEachAIStateOverTime(timeBetweenAiChange, SetSprite.SpriteState.pose4));
        yield return new WaitForSeconds(6);
        StartCoroutine(ChangeStatesOverTimeTwoPositions(timeBetweenAiChange, SetSprite.SpriteState.pose1, SetSprite.SpriteState.pose3));
        yield return new WaitForSeconds(5);
        StartCoroutine(ChangeEachAIStateOverTime(timeBetweenAiChange, SetSprite.SpriteState.pose4));
        yield return new WaitForSeconds(6);
        StartCoroutine(ChangeStatesOverTimeTwoPositions(timeBetweenAiChange, SetSprite.SpriteState.pose3, SetSprite.SpriteState.pose1));
    }

    IEnumerator ChangeEachAIStateOverTime(float timeBetweenStates, SetSprite.SpriteState state)
    {
        for (int i = 0; i < AiStateChange.AiGameObjects.Count; i++)//for each sprite, change pose, then wait then change pose then wait
        {
            AiStateChange.AiGameObjects[i].GetComponent<SetSprite>().State = state;
            yield return new WaitForSeconds(timeBetweenStates);
            AiStateChange.AiGameObjects[i].GetComponent<SetSprite>().State = SetSprite.SpriteState.normal;
        }
        StartCoroutine(TimeToReact(timeToReact, timeBetweenAiAndPlayer, state));
    }

    IEnumerator TimeToReact(float timeforreact, float timeBetweenAiAndPlayer ,SetSprite.SpriteState aiState)
    {
        SetSprite.AiState = aiState;
        yield return new WaitForSeconds(timeBetweenAiAndPlayer);//this is short, but it acts as kind of a buffer to get the rhythm right
        CheckInput.check = true;
        yield return new WaitForSeconds(timeforreact);
        CheckInput.check = false;

        if(CheckInput.InputIsCorrect == 1)//this sees whether you pressed the right number or not
        {
            StartCoroutine(ReactExpression(timeForReactionExpression, SetSprite.SpriteState.happyExpression));
        }
        else
        {
            StartCoroutine(ReactExpression(timeForReactionExpression, SetSprite.SpriteState.angryExpression));
        }
        CheckInput.didPoints = false;//these just need to be reset afterwords
        CheckInput.InputIsCorrect = 0;
        CheckInput.restingAfterCorrect = false;
    }

    IEnumerator ReactExpression(float expressionTime, SetSprite.SpriteState aiState)
    {
        for (int i = 0; i < AiStateChange.AiGameObjects.Count; i++)//for each sprite, change pose to reaction pose
        {
            AiStateChange.AiGameObjects[i].GetComponent<SetSprite>().State = aiState;
        }

        yield return new WaitForSeconds(expressionTime); 
        
        for (int i = 0; i < AiStateChange.AiGameObjects.Count; i++)//set sprite back to normal
        {
            AiStateChange.AiGameObjects[i].GetComponent<SetSprite>().State = SetSprite.SpriteState.normal;
        }
    }


    IEnumerator ChangeStatesOverTimeTwoPositions(float timeBetweenStates, SetSprite.SpriteState state1, SetSprite.SpriteState state2)
    {//for each ai it shows 2 poses that are the same.
        for (int i = 0; i < AiStateChange.AiGameObjects.Count; i++)
        {
            AiStateChange.AiGameObjects[i].GetComponent<SetSprite>().State = state1;
            yield return new WaitForSeconds(timeBetweenStates - .3f);//this is the time between 2 states
            AiStateChange.AiGameObjects[i].GetComponent<SetSprite>().State = state2;
            yield return new WaitForSeconds(timeBetweenStates);
            AiStateChange.AiGameObjects[i].GetComponent<SetSprite>().State = SetSprite.SpriteState.normal;
        }
        StartCoroutine(TimeToReactTwoPositions(timeToReact, timeBetweenAiAndPlayer, state1, state2));
    }

    IEnumerator TimeToReactTwoPositions(float timeforreact, float timeBetweenAiAndPlayer, SetSprite.SpriteState aiState1, SetSprite.SpriteState aiState2)
    {
        SetSprite.AiState = aiState1;//check for first pose
        yield return new WaitForSeconds(timeBetweenAiAndPlayer);
        CheckInput.check = true;
        yield return new WaitForSeconds(timeforreact - .2f);//time between 2 poses
        SetSprite.AiState = aiState2;//check for first pose
        CheckInput.check = false;

        if (CheckInput.InputIsCorrect == 1)//this sees whether you pressed the right number or not
        {
            StartCoroutine(ReactExpression(timeForReactionExpression, SetSprite.SpriteState.happyExpression));
            audioSource.clip = correctSound;
            audioSource.Play();
        }
        else
        {
            StartCoroutine(ReactExpression(timeForReactionExpression, SetSprite.SpriteState.angryExpression));
            audioSource.clip = wrongSound;
            audioSource.Play();
        }
        CheckInput.didPoints = false;//these just need to be reset afterwords
        CheckInput.InputIsCorrect = 0;
        CheckInput.restingAfterCorrect = false;
    }
}
