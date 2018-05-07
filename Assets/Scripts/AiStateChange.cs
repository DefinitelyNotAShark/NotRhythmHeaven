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

    [SerializeField]
    AudioClip pose1Clip;

    [SerializeField]
    AudioClip pose2Clip;

    [SerializeField]
    AudioClip pose3Clip;

    [SerializeField]
    AudioClip pose4Clip;

    public static List<GameObject> AiGameObjects;

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
}
