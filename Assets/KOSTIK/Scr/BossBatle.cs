using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBatle : MonoBehaviour
{
    [SerializeField] private Controller controller;
    
    [SerializeField] private Boss boss;
    [SerializeField] private Animator fadein;
    private bool gameEnded;

    void Start(){
        controller.justRotate(new Vector2(180, 0));
    }
    public void playerWon(){
        if (gameEnded) return;
        gameEnded = true;
        boss.DIEBITCH();
        Invoke("CloseFade", 5);
    }
    void CloseFade(){
        fadein.Play("FadeIn", 0, 0);
        Invoke("ChangeScene", 2);
    }
    void ChangeScene(){
        PlayerPrefs.SetInt("gameStage", 11); 
        SceneManager.LoadScene(0);
    }

    void Update(){
        if (Input.GetKeyDown("l")) playerWon();
    }
}
