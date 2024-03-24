using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class NPC : MonoBehaviour
{
    public TMP_Text container;
    
    
    public NPCText[] text;
    public NPCText[] textEN;
    private bool _dialogStarted;
    private int _line;
    private float _time;
    private bool _printStep;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float[] pitchRange;

    void Start(){
        if (PlayerPrefs.GetInt("Language", 0) == 1)
            text = textEN;
    }

    protected virtual void Update()
    {
        if (!_dialogStarted) return;

        _time += Time.unscaledDeltaTime;
        if (_printStep)
        {
            if (text[_line].text == "" || text[_line].printingTime <= 0)
            {
                container.text = "";
                _printStep = false;
                return;
            }
            container.text = text[_line].text[..(int)(text[_line].text.Length * Math.Min(1, _time /  text[_line].printingTime))];
            if (audioSource != null && container.text.Length % 2 == 0 && !audioSource.isPlaying){
                audioSource.pitch = Random.Range(pitchRange[0], pitchRange[1]);
                audioSource.Play();
            }
            if (!(_time > text[_line].printingTime)) return;
            _time = 0;
            _printStep = false;
        }
        else
        {
            if (!(_time > text[_line].waitTime)) return;
            _time = 0;
            _printStep = true;
            _line++;
            if (_line == text.Length)
            {
                OnDialogEnd();
                _dialogStarted = false;
                container.enabled = false;
                container.text = "";
                return;
            }
            OnTextLine(_line);
        }
    }

    protected abstract void OnTextLine(int line);

    protected abstract void OnDialogEnd();

    public void StartDialog()
    {
        _dialogStarted = true;
        _line = 0;
        _time = 0;
        _printStep = true;
        OnTextLine(_line);
        container.enabled = true;
        container.text = "";
    }
}