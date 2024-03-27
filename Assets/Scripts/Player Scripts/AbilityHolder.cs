using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Spells spell; 
    float cooldownTime; 
    float activeTime; 

    enum SpellState
    {
        ready,
        active,
        cooldown
    }

    SpellState state = SpellState.ready;

    public KeyCode key; 

    void Update()
    {
        switch (state)
        {
            case SpellState.ready:
                if (Input.GetKeyDown(key))
                {
                    spell.Activate(gameObject); 
                    state = SpellState.active; 
                    activeTime = spell.activeTime; 
                }
                break;
            case SpellState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime; 
                }
                else
                {
                    state = SpellState.cooldown;  
                    cooldownTime = spell.cooldownTime; 
                }
                break;
            case SpellState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime; 
                }
                else
                {
                    state = SpellState.ready;  
                }

                break;
        }

        
    }
}
