using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITranslator : MonoBehaviour
{
    [SerializeField] private TMP_Text[] texts;
    [SerializeField] private string[] rusVersion;
    [SerializeField] private string[] enVersion;

    [SerializeField] private TMP_FontAsset rusFont;
    public Material rusFontMat;

    [SerializeField] private TMP_FontAsset enFont;
    public Material enFontMat;

    void Start(){
        setLanguage(PlayerPrefs.GetInt("Language", 1));
    }

    public void setLanguage(int id){
        PlayerPrefs.SetInt("Language", id);

        if (id == 0){
            for(int i = 0; i < texts.Length; i++)
            {
                texts[i].font = rusFont;
                texts[i].fontSharedMaterial = rusFontMat;
                texts[i].text = rusVersion[i];
                //texts[i].fontStyle = TMPro.FontStyles.Bold;
            }
        }
        else{
            for(int i = 0; i < texts.Length; i++)
            {
                 texts[i].font = enFont;
                texts[i].fontSharedMaterial = enFontMat;
                texts[i].text = enVersion[i];
            }
        }
    }
}
