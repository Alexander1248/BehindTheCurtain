using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : NPC
{
    [SerializeField] private Controller controller;
    [SerializeField] private Animator animatorFade;
    public bool died;

    public void die(){
        if (died) return;
        controller.Cutscene(Vector2.zero);
        died = true;
        animatorFade.Play("FadeIn", 0, 0);
        Invoke("dialoguestart", 2);
    }

    void dialoguestart(){
        base.StartDialog();
    }

    protected override void OnDialogEnd()
    {
        int gameStage = PlayerPrefs.GetInt("gameStage", 0);

        if (gameStage < 2){
            SceneManager.LoadScene(2);
        }
        else if (gameStage == 6){
            SceneManager.LoadScene(2);
        }
        else if (gameStage == 4){ // boss fight
            PlayerPrefs.SetInt("gameStage", 22); 
            SceneManager.LoadScene(1);
        }
        else {
            PlayerPrefs.SetInt("gameStage", -1); 
            SceneManager.LoadScene(0);
        }
    }

    protected override void OnTextLine(int line)
    {
        
    }
}
