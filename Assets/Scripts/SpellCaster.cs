using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    
    public int mana = 100;
    
    public Spell[] spells;
    [Min(0.1f)]
    public float scrollSpeed;
    
    
    public float _spellIndex;

    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
        if (!_camera) _camera = GetComponent<Controller>().camera;
    }

    public void Update()
    {
        _spellIndex += Input.mouseScrollDelta.y * scrollSpeed;
        if (!Input.GetMouseButton(0)) return;
        while (_spellIndex < 0) _spellIndex += spells.Length;
        while (_spellIndex >= spells.Length) _spellIndex -= spells.Length;
        
        var spell = spells[(int) _spellIndex];
        if (mana < spell.manaCost) return; 
        if (Time.fixedTime - spell.Timer < spell.cooldown) return;
        
        mana -= spell.manaCost;
        spell.Timer = Time.fixedTime;
        var t = transform;
        spell.Cast(t.position, _camera.transform.rotation);
    }
}