using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanNPC_END : NPC
{
    public Animator animatorFade;

    protected override void OnDialogEnd()
    {
        Invoke("ENDGAME", 3);
    }

    void ENDGAME(){
        animatorFade.Play("FadeIn", 0, 0);
    }

    protected override void OnTextLine(int line)
    {

    }
}
