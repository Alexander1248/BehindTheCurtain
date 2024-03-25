using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WomanNPC_END : NPC
{
    public Animator animatorFade;

    protected override void OnDialogEnd()
    {
        Invoke("ENDGAME", 3);
    }

    void ENDGAME(){
        animatorFade.Play("FadeIn", 0, 0);
        Invoke("changeScene", 3);
    }

    void changeScene(){
        SceneManager.LoadScene(4);
    }

    protected override void OnTextLine(int line)
    {

    }
}
