using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public int maxMana = 100;
    public int mana;
    
    public Spell[] spells;
    [Min(0.1f)]
    public float scrollSpeed;

    [Space] 
    public ManaBar bar;

    public NPC[] manaFirstEmptyNPCs;
    public int disableIndex;
    
    private float _spellIndex;

    private Camera _camera;


    [SerializeField] private AudioSource audioSource;

    public bool paused;

    private void Start()
    {
        if ((int)_spellIndex == disableIndex)
        {
            _spellIndex++;
            while (_spellIndex < 0) _spellIndex += spells.Length;
            while (_spellIndex >= spells.Length) _spellIndex -= spells.Length;
        }
        spells[(int)_spellIndex].Selected();
        mana = maxMana;
        if (bar) bar.Initialize(maxMana);
        _camera = Camera.main;
        if (!_camera) _camera = GetComponent<Controller>().camera;
    }

    public void Update()
    {
        if (paused) return;
        
        var delta = Input.mouseScrollDelta.y * scrollSpeed;
        if (delta != 0)
        {
            var prev = (int)_spellIndex;
            _spellIndex += delta;
            while (_spellIndex < 0) _spellIndex += spells.Length;
            while (_spellIndex >= spells.Length) _spellIndex -= spells.Length;
            var curr = (int)_spellIndex;
            if (curr == disableIndex)
            {
                _spellIndex += Mathf.Sign(delta);
                while (_spellIndex < 0) _spellIndex += spells.Length;
                while (_spellIndex >= spells.Length) _spellIndex -= spells.Length;
                curr = (int)_spellIndex;
            }
            
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
        bar.Set(mana);
        spell.Timer = Time.fixedTime;
        var t = transform;
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        spell.Cast(t.position, _camera.transform.rotation);
        if (manaFirstEmptyNPCs != null && mana == 0)
        {
            foreach (var npc in manaFirstEmptyNPCs)
                npc.StartDialog();
            manaFirstEmptyNPCs = null;
        }

        audioSource.clip = spell.clip;
        audioSource.pitch = Random.Range(spell.randomPitch[0], spell.randomPitch[1]);
        audioSource.Play();
    }

    public void RestoreCells(int count)
    {
        mana = Mathf.Min(maxMana, mana + count);
        if (bar) bar.Set(mana);
    }

    public void SelectSpell(int index)
    {
        spells[(int)_spellIndex].Deselected();
        _spellIndex = index;
        spells[index].Selected();
    }
}