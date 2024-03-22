using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using Unity.Mathematics;

public class Boss : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    private Transform player;
    private string state = "Idle";
    private bool jumping;
    private Vector3 jumpStart;
    private Vector3 jumpEnd;
    private float jumpT;

    [SerializeField] private float maxDistJump;
    
    [SerializeField] private AnimationClip[] attackClips;
    [SerializeField] private AnimationClip jumpClip;

    [SerializeField] private GameObject hitParticles;
    [SerializeField] private Transform hitParticlesPOS;

    [SerializeField] private ParticleSystem[] LnRSword;

    void Start(){
        player = GameObject.FindWithTag("Player").transform;
        agent.updateRotation = false;
        InvokeRepeating("WantJump", 0.5f, 0.5f);
    }

    bool isStaying()
    {
        if (agent.enabled && !agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    return true;
                }
            }
        }
        return false;
    }

    void HitAgain(){
        int id = Random.Range(0, attackClips.Length);
        animator.CrossFade(attackClips[id].name, 0.2f);
        Invoke("HitAgain", attackClips[id].length);
    }

    void Update(){
        if (agent.enabled){
            agent.SetDestination(player.position);
            //agent.velocity = agent.desiredVelocity;
        }
        
        if (agent.velocity != Vector3.zero)
            agent.transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(agent.velocity ).eulerAngles.y, 0);
        if (state == "Hit"){
            Vector3 fwd = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;
            transform.forward = fwd.normalized;
        }
        if (isStaying() && state == "Run"){
            int id = Random.Range(0, attackClips.Length);
            animator.CrossFade(attackClips[id].name, 0.2f);
            Invoke("HitAgain", attackClips[id].length);
            state = "Hit";
        }
        else if (!isStaying() && (state == "Hit" || state == "Idle")){
            animator.CrossFade("Walk", 0.2f);
            state = "Run";
            CancelInvoke("HitAgain");
        }

        if (jumping){
            jumpT += Time.deltaTime * 0.85f;
            transform.position = Vector3.Lerp(jumpStart, jumpEnd, jumpT);
        }
    }

    void WantJump(){
        float jumpChance = 1 * Vector3.Distance(transform.position, player.position) / maxDistJump;
        if (Vector3.Distance(transform.position, player.position) > maxDistJump) jumpChance = 0;
        
        if (Random.value <= jumpChance && !jumping){
             CancelInvoke("HitAgain");
            agent.velocity = Vector3.zero;
            agent.enabled = false;
            state = "Jump";
            jumpStart = transform.position;
            Vector3 fwd = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;
            jumpEnd = player.position - fwd.normalized * 1;
            jumpEnd.y = transform.position.y;
            jumpT = 0;
            Invoke("stopJump", jumpClip.length);
            animator.CrossFade("JumpAttack", 0.2f);
            jumping = true;
        }
    }

    public void animationEvent(int type){
        if (type == 1){
            Instantiate(hitParticles, hitParticlesPOS.position, quaternion.identity);
        }
        else if (type == 11){
            LnRSword[0].Play();
        }
        else if (type == 12){
            LnRSword[1].Play();
        }
    }

    void stopJump(){
        state = "Hit";
        jumping = false;
        agent.enabled = true;
        int id = Random.Range(0, attackClips.Length);
        animator.CrossFade(attackClips[id].name, 0.2f);
        Invoke("HitAgain", attackClips[id].length);
    }
}
