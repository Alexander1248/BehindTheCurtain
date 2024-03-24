using UnityEngine;
using UnityEngine.UI;

public class Lightning : Spell
{
    public Attacking attacking;
    public GameObject hand;
    public Animator animator;
    public Image icon;


    public override void Selected()
    {
        animator.Rebind();
        animator.Update(0f);
        animator.enabled = false;
        hand.SetActive(true);
        icon.enabled = true;
    }

    public override void Deselected()
    {
        animator.Rebind();
        animator.Update(0f);
        hand.SetActive(false);
        icon.enabled = false;
    }
    public override void Cast(Vector3 position, Quaternion rotation)
    {
        attacking.throwLightning();
    }
}