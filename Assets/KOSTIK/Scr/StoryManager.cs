using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private NPC stotyteller1;
    [SerializeField] private NPC stotyteller2;
    [SerializeField] private NPC stotyteller3;

    private int stage;

    void Start()
    {
        Invoke("startTalking", 1);
    }

    void startTalking(){
        stotyteller1.StartDialog();
    }

    public void nextStage(){
        stage++;
        if (stage == 1) stotyteller2.StartDialog();
        else if (stage == 2) stotyteller3.StartDialog();
        else SceneManager.LoadScene(2);
    }
}
