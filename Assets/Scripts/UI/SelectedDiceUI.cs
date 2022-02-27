using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedDiceUI : MonoBehaviour
{
    [SerializeField] private ElementTypeRuntimeCollection _typeRuntimeCollection;
    [SerializeField] private Text _text;
    [SerializeField] private SpellBook _spellBook;
    private int _counter;
    // Start is called before the first frame update
    void Start()
    {
        _counter = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        if (_typeRuntimeCollection.Count() != _counter)
        {
            _counter = _typeRuntimeCollection.Count();
            if (_spellBook.CalculateSpell(_typeRuntimeCollection.List()) != null)
            {
                _text.text = _spellBook.CalculateSpell(_typeRuntimeCollection.List()).Name;
            }
            else
            {
                _text.text = "~";
            }
        }

    }


    public void Cast()
    {
        for (int i = 0; i < _typeRuntimeCollection.Count(); i++)
        {
            Debug.Log(_spellBook.CalculateSpell(_typeRuntimeCollection.List()).Name);
        }
        Debug.Log(" Cast!");
    }
}
