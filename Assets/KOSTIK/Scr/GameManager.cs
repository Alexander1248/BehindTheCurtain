using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator fadeAnimator;

    public AudioSource audioSource_knocking;
    public AudioSource khmkhmAudio;

    [SerializeField] private Transform player;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Vector2 rotPlayer;
    [SerializeField] private GameObject invisWall;
    
    [Space]
    [SerializeField] private GameObject house;
    [SerializeField] private GameObject fire;

    void Start()
    {
        if (PlayerPrefs.GetInt("gameStage", 0) == 6)
        {
            loadFromDark0();
            house.SetActive(true);
            fire.SetActive(true);
            
        }
    }

    public void triggerActivated(int id){
        if (id == 2){
            // interupt
            audioSource_knocking.Play();
            Invoke("startFade1", 2);
        }
        else if (id == 4){
            khmkhmAudio.Play();
            Invoke("startFade2", 2);
        }
    }

    void startFade1(){
        fadeAnimator.Play("FadeIn", 0, 0);
        Invoke("loadDark", 1.5f);
    }

    void loadDark(){
        PlayerPrefs.SetInt("gameStage", 2); 
        PlayerPrefs.SetInt("SCDI", -1); 
        SceneManager.LoadScene(2);
    }

    void startFade2(){
        fadeAnimator.Play("FadeIn", 0, 0);
        Invoke("loadDark2", 1.5f);
    }

    void loadDark2(){
        PlayerPrefs.SetInt("gameStage", 4); 
        PlayerPrefs.SetInt("SCDI", -1); 
        SceneManager.LoadScene(2);
    }

    void loadFromDark0()
    {
        player.transform.position = playerPos.position;
        player.GetComponent<Controller>().justRotate(rotPlayer);
        invisWall.SetActive(true);
    }
}
