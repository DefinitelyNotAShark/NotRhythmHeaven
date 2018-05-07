using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void StartClick()
    {
        audioSource.Play();
        SceneManager.LoadScene("IntroScene");
    }

    public void CreditsClick()
    { 
        audioSource.Play();
        SceneManager.LoadScene("CreditsScene");
    }

    public void BackClick()
    {
        audioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgainClick()
    {
        audioSource.Play();
        CheckInput.points = 0;
        SceneManager.LoadScene("GameScene");
    }

    public void QuitClick()
    {
        audioSource.Play();
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
