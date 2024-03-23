using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HidePart : MonoBehaviour
{
    [SerializeField] private NPC npcPlayer0;
    [SerializeField] private NPC npcPlayer;
    [SerializeField] private Controller player;
    [SerializeField] private Transform[] poses;
    [SerializeField] private Vector2[] rotations;
    [SerializeField] private GameObject[] hidePlaces;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private Animator animatorDoor;
    [SerializeField] private PlayableDirector playableDirector;

    [SerializeField] private Transform endCSPos;
    [SerializeField] private Vector2 endCSRot;

    [SerializeField] private GameObject GM;

    [SerializeField] private AudioSource[] audioToOffOnHide;

    [SerializeField] private AudioSource heartbeat;
    [SerializeField] private AudioSource hiding;

    private bool played;

    void Start(){
        GM.SetActive(false);
        InvokeRepeating("shakedoor", 0.65f, 0.65f);
        Invoke("firstSay", 1);
    }

    void firstSay(){
        npcPlayer0.StartDialog();
    }

    void shakedoor(){
        animatorDoor.Play("LockedDoor", 0, 0);
    }

    public void hidePlayer(int id){
        if (played){
            // continue
            return;
        }
        hiding.Play();
        for(int i = 0; i < audioToOffOnHide.Length; i++)
            audioToOffOnHide[i].Stop();
        CancelInvoke("shakedoor");
        played = true;
        player.Cutscene(rotations[id]);
        player.transform.position = poses[id].position;
        hidePlaces[0].tag = "Untagged";
        hidePlaces[1].tag = "Untagged";
        arrows[0].SetActive(false);
        arrows[1].SetActive(false);

        // cut scene
        playableDirector.Play();
    }

    public void endCS(){
        Invoke("getOut", 2);
    }

    void getOut(){
        heartbeat.Stop();
        player.endCS(endCSRot);
        player.transform.position = endCSPos.position;
        hidePlaces[1].GetComponent<hidePlace>().tipName = "PLAY";
        hidePlaces[1].tag = "InteractMe";

        npcPlayer.StartDialog();
        GM.SetActive(true);
    }
}
