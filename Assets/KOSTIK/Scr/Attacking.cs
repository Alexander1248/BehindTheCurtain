using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    [SerializeField] private float animationSpeed;
    [SerializeField] private float kulakKD;
    [SerializeField] private Animator handAnimator;

    [SerializeField] private GameObject lightningPref;
    [SerializeField] private Transform lightningSpawPoint;
    [SerializeField] private GameObject lightning;
    [SerializeField] private float speedBolt;
    [SerializeField] private float distThrow;

    void Start(){
        InvokeRepeating("Hit", kulakKD, kulakKD);
    }

    void Hit(){
        if (!handAnimator.enabled) handAnimator.enabled = true;
        handAnimator.speed = animationSpeed;

        handAnimator.CrossFade("HITV2", 0.1f);
    }

    public void throwLightning(){
        if (lightning != null)
        {
            if (!handAnimator.enabled)
            {
                handAnimator.enabled = true;
                handAnimator.Play("ThrowC2", 0, 0);
            }

            handAnimator.Play("ThrowC2", 0, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 dir = Vector3.zero;
            if (Physics.Raycast(ray, out hit, 100))
            {
                dir = -lightning.transform.position + hit.point;
                lightning.GetComponent<C>().getDirection(dir.normalized, speedBolt);
            }
            else
            {
                dir = ray.origin + ray.direction * 100;
                lightning.GetComponent<C>().getDirection(dir.normalized, speedBolt);
            }
            lightning.GetComponent<Collider>().enabled = true;

            lightning.transform.forward = dir.normalized;
        }
        
        // мне похуй паркуюсь где хочу я быдло
        float timerespawn = 0;
        AnimationClip[] clips = handAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "ThrowC":
                    timerespawn = clip.length;
                    break;
            }
        }
        Invoke("SpawnLightning", timerespawn);
    }
    void SpawnLightning(){
        lightning = Instantiate(lightningPref, lightningSpawPoint, false);
        lightning.transform.localPosition = Vector3.zero;
        lightning.transform.localEulerAngles = Vector3.zero;
    }
}
