using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITranslator : MonoBehaviour
{
    [SerializeField] private TMP_Text[] texts;
    [SerializeField] private string[] rusVersion;
    [SerializeField] private string[] enVersion;

    void Awake(){
        PlayerPrefs.SetInt("Language", 0);
    }

    void Start(){
        setLanguage(PlayerPrefs.GetInt("Language", 1));
    }

    void setLanguage(int id){
        PlayerPrefs.SetInt("Language", id);

        if (id == 0){
            for(int i = 0; i < texts.Length; i++)
            {
                texts[i].text = rusVersion[i];
                texts[i].fontStyle = TMPro.FontStyles.Bold;
            }
        }
        else{
            for(int i = 0; i < texts.Length; i++)
                texts[i].text = enVersion[i];
        }
    }
}
