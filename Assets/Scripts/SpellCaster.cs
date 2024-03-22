using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public bool cast = true;
    
    public int mana;
    
    public Spell[] spells;
    [Min(0.1f)]
    public float scrollSpeed;
    
    
    private float _spellIndex;

    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
        if (!_camera) _camera = GetComponent<Controller>().camera;
    }

    public void Update()
    {
        if (!cast) return;
        _spellIndex += Input.mouseScrollDelta.y * scrollSpeed;
        if (!Input.GetMouseButton(1)) return;
        
        var spell = spells[(int) _spellIndex];
        if (mana < spell.manaCost) return; 
        if (Time.fixedTime - spell.Timer < spell.cooldown) return;
        
        mana -= spell.manaCost;
        spell.Timer = Time.fixedTime;
        var t = transform;
        spell.Cast(t.position, _camera.transform.rotation);
    }
}