using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateChange : MonoBehaviour
{
    //this script should be set to the parent of all ai game objects
    [SerializeField]
    GameObject AiGameObject1;

    [SerializeField]
    GameObject AiGameObject2;

    [SerializeField]
    GameObject AiGameObject3;

    List<GameObject> AiGameObjects;

    SetSprite setSprite;
    float timeBetweenSpriteState;

    private void Awake()
    {
        AiGameObjects = new List<GameObject>();//makes a list of gameObjects for each ai object

        AiGameObjects.Add(AiGameObject1);//adds all ai Objects to list
        AiGameObjects.Add(AiGameObject2);
        AiGameObjects.Add(AiGameObject3);

        foreach(GameObject a in AiGameObjects)
        {
            a.gameObject.AddComponent<SetSprite>();//adds a set sprite script to each spriteObject
        }
    }

    private void Start()
    {
        StartCoroutine(tempCoroutineForTimeTesting());
    }

    IEnumerator tempCoroutineForTimeTesting()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeEachAIStateOverTime(.5f, SetSprite.SpriteState.pose1));
        yield return new WaitForSeconds(2);
        StartCoroutine(ChangeEachAIStateOverTime(.5f, SetSprite.SpriteState.pose2));
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeEachAIStateOverTime(.5f, SetSprite.SpriteState.pose3));
    }

    IEnumerator ChangeEachAIStateOverTime(float time, SetSprite.SpriteState state)
    {
        for (int i = 0; i < AiGameObjects.Count; i++)//for each sprite, change pose, then wait then change pose then wait
        {
            AiGameObjects[i].GetComponent<SetSprite>().State = state;
            yield return new WaitForSeconds(time);
            AiGameObjects[i].GetComponent<SetSprite>().State = SetSprite.SpriteState.normal;
        }
    }

    IEnumerator TimeToReact()
    {
        yield return new WaitForSeconds(.5f);
    }
}
