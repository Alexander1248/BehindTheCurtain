using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Goblin : MonoBehaviour
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
    [SerializeField] private AnimationClip jumpClip;

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

    void Update(){
        if (agent.enabled){
            agent.SetDestination(player.position);
        
        }
        
        if (agent.velocity != Vector3.zero)
            agent.transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(agent.velocity ).eulerAngles.y, 0);
        if (state == "Hit"){
            Vector3 fwd = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;
            transform.forward = fwd.normalized;
        }
        if (isStaying() && state == "Run"){
            animator.CrossFade("GoblinHit", 0.2f);
            state = "Hit";
        }
        else if (!isStaying() && (state == "Hit" || state == "Idle")){
            animator.CrossFade("GoblinRunning", 0.2f);
            state = "Run";
        }

        if (jumping){
            jumpT += Time.deltaTime * 1.1f;
            transform.position = Vector3.Lerp(jumpStart, jumpEnd, jumpT);
        }
    }

    void WantJump(){
        float jumpChance = 1 * Vector3.Distance(transform.position, player.position) / maxDistJump;
        if (Vector3.Distance(transform.position, player.position) > maxDistJump) jumpChance = 0;
        
        if (Random.value <= jumpChance && !jumping){
            agent.velocity = Vector3.zero;
            agent.enabled = false;
            state = "Jump";
            jumpStart = transform.position;
            Vector3 fwd = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;
            jumpEnd = player.position - fwd.normalized * 1;
            jumpEnd.y = transform.position.y;
            jumpT = 0;
            Invoke("stopJump", jumpClip.length);
            animator.CrossFade("GoblinJump", 0.5f);
            jumping = true;
        }
    }

    void stopJump(){
        state = "Hit";
        jumping = false;
        agent.enabled = true;
        animator.CrossFade("GoblinHit", 0.5f);
    }
}
