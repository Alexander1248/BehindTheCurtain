using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Transform cam;
    private Vector3 startingPos;
    private float camT;
    [SerializeField] private float speedCam;
    [SerializeField] private float radiusCam;

    [SerializeField] private TMP_Text[] buttons;
    private float[] defaultSize;

    [SerializeField] private Animator animatorFade;

    [SerializeField] private GameObject[] menus;

    [SerializeField] private Slider[] sliders;
    [SerializeField] private TMP_Text[] slidersText;

    [SerializeField] private float[] sensRange;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource audioSourceButtons;
    [SerializeField] private AudioClip[] buttonClips;

    void Awake(){
        if (PlayerPrefs.GetInt("Language", -1) == -1)
            PlayerPrefs.SetInt("Language", 1);
    }

    void Start(){
        defaultSize = new float[buttons.Length];
        for(int i = 0; i < buttons.Length; i++)
            defaultSize[i] = buttons[i].fontSize;
        startingPos = cam.position;

        slidersText[1].text = "" + PlayerPrefs.GetFloat("PlayerSens", 3).ToString("F1");
        sliders[1].value = InverseLerp(sensRange[0], sensRange[1], PlayerPrefs.GetFloat("PlayerSens", 3));

        slidersText[0].text = "" + PlayerPrefs.GetFloat("PlayerVolume", 1).ToString("F1");
        sliders[0].value = PlayerPrefs.GetFloat("PlayerVolume", 1);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static float InverseLerp(float a, float b, float value)
    {
        if (a != b)
            return Mathf.Clamp01((value - a) / (b - a));
        else
            return 0f;
    }
    
    void Update()
    {
        camT += Time.deltaTime * speedCam;
        Vector3 pos = startingPos + Mathf.Cos(camT) * cam.right * radiusCam;
        pos += Mathf.Sin(camT) * Vector3.up * radiusCam;
        cam.transform.position = pos;
    }

    public void changeLanguage(int id){
        PlayerPrefs.SetInt("Language", id);
        UITranslator[] alluis = GameObject.FindObjectsOfType<UITranslator>();
        for(int i = 0; i < alluis.Length; i++)
            alluis[i].setLanguage(PlayerPrefs.GetInt("Language", 1));
    }

    public void hoverButton(int id){
        audioSourceButtons.clip = buttonClips[0];
        audioSourceButtons.pitch = Random.Range(0.7f, 1.2f);
        audioSourceButtons.Play();
        for(int i = 0; i < buttons.Length; i++)
            buttons[i].fontSize = defaultSize[i];
        
        buttons[id].fontSize += 20;
    }

    public void clickButton(int id){
        audioSourceButtons.clip = buttonClips[1];
        audioSourceButtons.pitch = Random.Range(0.7f, 1.2f);
        audioSourceButtons.Play();
        buttons[id].fontSize = defaultSize[id] + 30;
        Invoke("resetButtons", 0.3f);

        if (id == 0){
            PlayerPrefs.SetInt("gameStage", -1);
            animatorFade.Play("FadeIn", 0, 0);
            Invoke("StartGame", 3);
        }
        else if (id == 1){
            menus[0].SetActive(false);
            menus[1].SetActive(true);
        }
        else if (id == 2){
            Application.Quit();
        }
        else if (id == 3){
            menus[1].SetActive(false);
            menus[0].SetActive(true);
        }
    }

    void StartGame(){
        PlayerPrefs.DeleteKey("SCDI");
        SceneManager.LoadScene(1);
    }

    public void resetButtons(){
        for(int i = 0; i < buttons.Length; i++)
            buttons[i].fontSize = defaultSize[i];
    }

    public void changeSens(){
        float sens = Mathf.Lerp(sensRange[0], sensRange[1], sliders[1].value);
        PlayerPrefs.SetFloat("PlayerSens", sens);
        slidersText[1].text = "" + sens.ToString("F1");
    }

    public void changeVolume(){
        PlayerPrefs.SetFloat("PlayerVolume", sliders[0].value);
        slidersText[0].text = "" + sliders[0].value.ToString("F1");

        audioMixer.SetFloat("Volume", Mathf.Log10(sliders[0].value)*20);
    }

    public void resetButton(int id){
        buttons[id].fontSize = defaultSize[id];
    }
}
