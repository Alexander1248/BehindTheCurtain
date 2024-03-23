using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    
    public int mana = 100;
    
    public Spell[] spells;
    [Min(0.1f)]
    public float scrollSpeed;
    
    
    public float _spellIndex;

    private Camera _camera;


    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        _camera = Camera.main;
        if (!_camera) _camera = GetComponent<Controller>().camera;
    }

    public void Update()
    {
        var delta = Input.mouseScrollDelta.y * scrollSpeed;
        if (delta != 0)
        {
            var prev = (int)_spellIndex;
            _spellIndex += delta;
            while (_spellIndex < 0) _spellIndex += spells.Length;
            while (_spellIndex >= spells.Length) _spellIndex -= spells.Length;
            var curr = (int)_spellIndex;
            if (prev != curr)
            {
                spells[prev].Deselected();
                spells[curr].Selected();
            }
        }

        if (!Input.GetMouseButton(0)) return;
        
        var spell = spells[(int)_spellIndex];
        if (mana < spell.manaCost) return; 
        if (Time.fixedTime - spell.Timer < spell.cooldown) return;
        
        mana -= spell.manaCost;
        spell.Timer = Time.fixedTime;
        var t = transform;
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        spell.Cast(t.position, _camera.transform.rotation);

        audioSource.clip = spell.clip;
        audioSource.pitch = Random.Range(spell.randomPitch[0], spell.randomPitch[1]);
        audioSource.Play();
    }
}