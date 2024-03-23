using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginningManager : MonoBehaviour
{
    [SerializeField] private Animator animatorFade;

    public void startGame(){
        animatorFade.enabled = true;
        animatorFade.Play("FadeIn", 0, 0);
        Invoke("changeScene", 3);
    }

    void changeScene(){
        SceneManager.LoadScene(1);
    }
}
