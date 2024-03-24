using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginningManager : MonoBehaviour
{
    [SerializeField] private Animator animatorFade;
    private bool started;

    [SerializeField] private Transform player;
    [SerializeField] private GameObject doorObj;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Vector2 rot;
    [SerializeField] private GameObject woman;
    [SerializeField] private NPC womanNPC;
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject table;

    void Start(){
        woman.SetActive(false);
        player.GetComponent<Controller>().justRotate(rot);
        if (PlayerPrefs.GetInt("gameStage", 0) == 11){
            trigger.SetActive(false);
            table.tag = "Untagged";
            doorObj.tag = "Untagged";
            player.position = playerPos.position;
            woman.SetActive(true);
            Invoke("dialogue", 1f);
        }
    }

    void dialogue(){
        womanNPC.StartDialog();
    }

    public void startGame(){
        if (started) return;
        started = true;
        animatorFade.enabled = true;
        animatorFade.Play("FadeIn", 0, 0);
        Invoke("changeScene", 3);
    }

    void changeScene(){
        SceneManager.LoadScene(2);
    }
}
