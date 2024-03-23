using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator fadeAnimator;

    public AudioSource audioSource_knocking;

    public void triggerActivated(int id){
        if (id == 2){
            // interupt
            audioSource_knocking.Play();
            Invoke("startFade1", 2);
        }
    }

    void startFade1(){
        fadeAnimator.Play("FadeIn", 0, 0);
        Invoke("loadDark", 1.5f);
    }

    void loadDark(){
        PlayerPrefs.SetInt("gameStage", 2); 
        SceneManager.LoadScene(1);
    }
}
