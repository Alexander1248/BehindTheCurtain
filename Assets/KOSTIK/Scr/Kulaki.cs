using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kulaki : Spell
{
    public GameObject obj;
    public Animator animatorK;
    private bool lastWasRight;
    public Image icon;

    private new void Awake()
    {
        base.Awake();
    }

    public override void Selected()
    {
        icon.enabled = true;
        obj.SetActive(true);
        animatorK.enabled = false;
    }

    public override void Deselected()
    {
        icon.enabled = false;
        obj.SetActive(false);
    }

    public override void Cast(Vector3 position, Quaternion rotation)
    {
        animatorK.enabled = true;
        if (lastWasRight) animatorK.Play("KulakLeft",0, 0);
        else animatorK.Play("KulakRight", 0, 0);
        lastWasRight = !lastWasRight;
    }
}
