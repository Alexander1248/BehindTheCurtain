using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

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
    [SerializeField] private PlayableDirector shelfDirector;

    [SerializeField] private Transform endCSPos;
    [SerializeField] private Vector2 endCSRot;

    [SerializeField] private GameObject GM;

    [SerializeField] private AudioSource[] audioToOffOnHide;

    [SerializeField] private AudioSource heartbeat;
    [SerializeField] private AudioSource hiding;

    [SerializeField] private PlayableDirector cathCS;
    [SerializeField] private Transform catchCSPos;
    [SerializeField] private Vector2 catchCSRot;

    [SerializeField] private AudioSource audioKashel;
    [SerializeField] private NPC npcDirector;

    [SerializeField] private Animator animatorFadeIn;

    private bool played;
    private int gameStage;

    [SerializeField] private GameObject womanEND;
    [SerializeField] private NPC womanNPC;

    void Start(){
        GM.SetActive(false);
        womanEND.SetActive(false);

        gameStage = PlayerPrefs.GetInt("gameStage", 0);
        if (gameStage == 2){
            // hiding from PREPOD
            InvokeRepeating("shakedoor", 0.65f, 0.65f);
            Invoke("firstSay", 1);
        }
        else if (gameStage == 4){
            // catched by PREPOD
            player.transform.position = catchCSPos.position;
            player.endCS(catchCSRot);
            hidePlaces[0].tag = "Untagged";
            hidePlaces[1].tag = "Untagged";
            arrows[0].SetActive(false);
            arrows[1].SetActive(false);
            animatorDoor.enabled = false;
            for(int i = 0; i < audioToOffOnHide.Length; i++)
                audioToOffOnHide[i].Stop();
            heartbeat.Stop();
            Invoke("startCSCathc", 1.5f);
        }
        else if (gameStage == 22){
            player.transform.position = catchCSPos.position;
            player.endCS(catchCSRot);
            hidePlaces[0].tag = "Untagged";
            hidePlaces[1].tag = "Untagged";
            arrows[0].SetActive(false);
            arrows[1].SetActive(false);
            animatorDoor.enabled = false;
            for(int i = 0; i < audioToOffOnHide.Length; i++)
                audioToOffOnHide[i].Stop();
            heartbeat.Stop();
            
            womanEND.SetActive(true);
            Invoke("DialogWomanEnd", 1);
        }
    }

    void DialogWomanEnd(){
        womanNPC.StartDialog();
    }

    void startCSCathc(){
        audioKashel.Play();
        cathCS.Play();
    }

    public void canContinueGameCatch(){
        npcDirector.StartDialog();
        if (PlayerPrefs.GetInt("Language", 0) == 1)
            hidePlaces[1].GetComponent<hidePlace>().tipName = "PLAY";
        else hidePlaces[1].GetComponent<hidePlace>().tipNameRUS = "ИГРАТЬ";
        hidePlaces[1].tag = "InteractMe";
    }
    
    void firstSay(){
        npcPlayer0.StartDialog();
    }

    void shakedoor(){
        animatorDoor.Play("LockedDoor", 0, 0);
    }

    void stage4(){
        SceneManager.LoadScene(4);
    }

    void stage2(){
        PlayerPrefs.SetInt("gameStage", 6);
        SceneManager.LoadScene(3);
    }

    public void hidePlayer(int id){
        if (gameStage == 4){
            animatorFadeIn.Play("FadeIn", 0, 0);
            Invoke("stage4", 2);
            return;
        }
        if (played){
            // continue
            animatorFadeIn.Play("FadeIn", 0, 0);
            Invoke("stage2", 2);
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
        if (id == 0) playableDirector.Play();
        else shelfDirector.Play();
    }

    public void endCS(){
        Invoke("getOut", 2);
    }

    void getOut(){
        heartbeat.Stop();
        player.endCS(endCSRot);
        player.transform.position = endCSPos.position;
        if (PlayerPrefs.GetInt("Language", 0) == 1)
            hidePlaces[1].GetComponent<hidePlace>().tipName = "PLAY";
        else hidePlaces[1].GetComponent<hidePlace>().tipNameRUS = "ИГРАТЬ";
        hidePlaces[1].tag = "InteractMe";

        npcPlayer.StartDialog();
        GM.SetActive(true);
    }
}
