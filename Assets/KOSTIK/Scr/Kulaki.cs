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

    public float distHit;
    public float damage;
    public float kickForce;
    public AudioSource audioClipHit;
    public float[] randomPitch0;
    public LayerMask layerMask;
    public float thickness = 1f;

    private new void Awake()
    {
        base.Awake();
    }

    public override void Selected()
    {
        icon.enabled = true;
        obj.SetActive(true);
        animatorK.Rebind();
        animatorK.Update(0f);
        animatorK.enabled = false;
    }

    public override void Deselected()
    {
        animatorK.Rebind();
        animatorK.Update(0f);
        icon.enabled = false;
        obj.SetActive(false);
    }

    public override void Cast(Vector3 position, Quaternion rotation)
    {
        animatorK.enabled = true;
        if (lastWasRight) animatorK.Play("KulakLeft",0, 0);
        else animatorK.Play("KulakRight", 0, 0);
        lastWasRight = !lastWasRight;

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, distHit, layerMask))
        //{
        //    var health = hit.collider.gameObject.GetComponent<Health>();
        //    if (health != null) {
        //        Vector3 dir = (hit.collider.gameObject.transform.position - transform.position).normalized;
        //        health.DealDamage(damage, dir * kickForce);
        //        audioClipHit.pitch = Random.Range(randomPitch0[0], randomPitch0[1]);
        //        audioClipHit.Play();
        //    }
        //}

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.SphereCast(ray.origin - ray.direction * 1, thickness, ray.direction, out hit)) {
            if (hit.distance > distHit) return;
            var health = hit.collider.gameObject.GetComponent<Health>();
            if (health != null) {
                Vector3 dir = (hit.collider.gameObject.transform.position - transform.position).normalized;
                health.DealDamage(damage, dir * kickForce, 1);
                audioClipHit.pitch = Random.Range(randomPitch0[0], randomPitch0[1]);
                audioClipHit.Play();
            }
        }
    }
}
