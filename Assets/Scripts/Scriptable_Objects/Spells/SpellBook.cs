using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Spell Casting/Spellbook")]
public class SpellBook : ScriptableObject
{
    public List<Spell> Spells => _spells;
    [SerializeField] private List<Spell> _spells;

    public Spell CalculateSpell(List<ElementType> selectedDice)
    {
        for (int i = 0; i < _spells.Count; i++)
        {
            // does the spell have the same number of dice
            if (_spells[i].Recipie.Count == selectedDice.Count)
            {
                int counter = 0;
                for (int j = 0; j < selectedDice.Count; j++)
                {
                    if (_spells[i].Recipie.Contains(selectedDice[j]))
                    {
                        counter++;
                    }
                }
                if (counter == _spells[i].Recipie.Count)
                {
                    Debug.Log(_spells[i].name + " identified!");
                    return _spells[i];
                }
            }
        }
        return null;
    }

}
