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
    private Health hpPlayer;
    private string state = "Idle";
    private bool jumping;
    private Vector3 jumpStart;
    private Vector3 jumpEnd;
    private float jumpT;

    [SerializeField] private float damage;
    [SerializeField] private float kickForce;

    [SerializeField] private float maxDistJump;
    [SerializeField] private float hitDistance;
    
    [SerializeField] private AnimationClip[] attackClips;
    [SerializeField] private AnimationClip jumpClip;

    [SerializeField] private GameObject hitParticles;
    [SerializeField] private Transform hitParticlesPOS;

    [SerializeField] private ParticleSystem[] LnRSword;

    [SerializeField] private MilkShake.ShakePreset shakePresetLand;


    [SerializeField] private DeathManager deathManager;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource deathSound_MY;
    private bool killed;


    [SerializeField] private AudioClip[] clipsWalk;
    [SerializeField] private AudioSource audioSource_walk;

    [SerializeField] private AudioClip[] clipsSFX;
    [SerializeField] private AudioSource audioSource_sfx;

    void Start(){
        player = GameObject.FindWithTag("Player").transform;
        hpPlayer = player.GetComponent<Health>();
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
        if (Vector3.Distance(transform.position, player.position) > hitDistance){
            animator.CrossFade("Walk", 0.2f);
            state = "Run";
            audioSource_walk.clip = clipsWalk[0];
            audioSource_walk.Play();
            return;
        }
        int id = Random.Range(0, attackClips.Length);
        animator.CrossFade(attackClips[id].name, 0.2f);
        Invoke("HitAgain", attackClips[id].length);
    }

    void Update(){
        if (deathManager.died && !killed){
            state = "Idle";
            animator.CrossFade("Idle", 0.2f);
            CancelInvoke();
            audioSource_walk.Stop();
            audioSource_sfx.Stop();
            deathSound.Play();
            music.Stop();
            agent.enabled = false;
            killed = true;
        }
        if (killed) return;

        if (jumping){
            jumpT += Time.deltaTime * 0.85f;
            transform.position = Vector3.Lerp(jumpStart, jumpEnd, jumpT);
        }

        if (agent.enabled && state != "Hit"){
            agent.SetDestination(player.position);
            //agent.velocity = agent.desiredVelocity;
        }
        
        if (agent.velocity != Vector3.zero)
            agent.transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(agent.velocity ).eulerAngles.y, 0);
        if (state == "Hit"){
            Vector3 fwd = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;
            transform.forward = fwd.normalized;
        }
        if (Vector3.Distance(transform.position, player.position) <= hitDistance && state != "Jump" && state != "Hit"){
            int id = Random.Range(0, attackClips.Length);
            animator.CrossFade(attackClips[id].name, 0.2f);
            Invoke("HitAgain", attackClips[id].length);
            state = "Hit";
            audioSource_walk.Stop();
        }
    }

    void WantJump(){
        float jumpChance = 1 * Vector3.Distance(transform.position, player.position) / maxDistJump;
        if (Vector3.Distance(transform.position, player.position) > maxDistJump) jumpChance = 0;
        
        if (Random.value <= jumpChance && !jumping){
             CancelInvoke("HitAgain");
            agent.velocity = Vector3.zero;
            agent.enabled = false;
            audioSource_walk.Stop();
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

    public void DIEBITCH(){
        GetComponent<Rigidbody>().isKinematic = true;
        CancelInvoke();
        killed = true;
        state = "Idle";
        animator.CrossFade("Die", 0.2f);
        audioSource_walk.Stop();
        audioSource_sfx.Stop();
        deathSound_MY.Play();
        music.Stop();
        agent.enabled = false;
    }

    public void animationEvent(int type){
        if (type == 1){
            audioSource_sfx.clip = clipsSFX[0];
            audioSource_sfx.Play();
            MilkShake.Shaker.ShakeAll(shakePresetLand);
            Instantiate(hitParticles, hitParticlesPOS.position, quaternion.identity);
        }
        else if (type == 2){
            audioSource_walk.clip = clipsWalk[1];
            audioSource_walk.Play();
        }
        else if (type == 11){
            audioSource_sfx.clip = clipsSFX[1];
            audioSource_sfx.Play();
            LnRSword[0].Play();
        }
        else if (type == 12){
            audioSource_sfx.clip = clipsSFX[1];
            audioSource_sfx.Play();
            LnRSword[1].Play();
        }
        else if (type == 13){
            audioSource_sfx.clip = clipsSFX[1];
            audioSource_sfx.Play();
        }
        else if (type == 66){
            if (Vector3.Distance(transform.position, player.position) <= hitDistance){
                Vector3 dirforce = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
                hpPlayer.DealDamage(damage, dirforce.normalized * kickForce);
            }
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
