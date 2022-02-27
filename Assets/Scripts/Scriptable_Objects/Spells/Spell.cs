using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Spell Casting/Spell")]
public class Spell : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;
    public List<ElementType> Recipie => _recipie;
    [SerializeField] private List<ElementType> _recipie;


}
