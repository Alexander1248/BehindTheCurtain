using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown("esc")) pause();
    }

    void pause(){
        Time.timeScale = 0.05f;
        pauseMenu.SetActive(true);
    }
}
