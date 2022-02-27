using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private float _randomRollPosition;
    [SerializeField] private int _diceAmount;
    [SerializeField] private List<GameObject> _rolledDiceObjects;
    [SerializeField] private List<Dice> _rolledDice;
    [SerializeField] private List<GameObject> _selectedDice;
    [SerializeField] private ElementTypeRuntimeCollection _typeRuntimeCollection;
    [SerializeField] private List<GameObject> _heldDice;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _isValidMousePosition;
    [SerializeField] private Vector3 _target;
    [SerializeField] private Dice _mouseOverDie;
    [SerializeField] private SpellBook _spellbook;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse input for selecting dice
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, _layerMask))
        {
            _isValidMousePosition = true;
            _target = hit.point;
            if (hit.rigidbody != null)
            {
                if (hit.rigidbody.gameObject.TryGetComponent<Dice>(out Dice die))
                {
                    _mouseOverDie = die;
                }
                else
                {
                    _mouseOverDie = null;
                }

            }
            Debug.DrawLine(ray.origin, _target, Color.cyan);
        }
        else
        {
            _isValidMousePosition = false;
        }

        //toggle dice selection on click
        if (_isValidMousePosition && _mouseOverDie !=null && Input.GetButtonDown("Fire1"))
        {
            if (_selectedDice.Contains(_mouseOverDie.gameObject))
            {
                _selectedDice.Remove(_mouseOverDie.gameObject);
                _typeRuntimeCollection.Remove(_mouseOverDie.RolledValue);
                Debug.Log("removed " + _mouseOverDie.RolledValue);
                CalculateSpell();

            }
            else
            {
                _selectedDice.Add(_mouseOverDie.gameObject);
                _typeRuntimeCollection.Add(_mouseOverDie.RolledValue);
                Debug.Log("added " + _mouseOverDie.RolledValue);
                CalculateSpell();
            }
        }
    }
    public void OnStartCasting()
    {
        for (int i = 0; i < _diceAmount; i++)
        {
            Vector3 randPosition = new Vector3(Random.Range(_spawnLocation.position.x -_randomRollPosition, _spawnLocation.position.x+ _randomRollPosition), _spawnLocation.position.y, Random.Range(_spawnLocation.position.z - _randomRollPosition, _spawnLocation.position.z -_randomRollPosition));
            GameObject instance = Instantiate(_dicePrefab, randPosition, Quaternion.identity, transform);
            if (instance.TryGetComponent<Dice>(out Dice die))
            {
                _rolledDiceObjects.Add(instance);
                _rolledDice.Add(die);
            }
            else
            {
                Debug.Log("Failed to get die script");
            }
        }
        RollDice();
    }

    private void RollDice()
    {
        for (int i = 0; i < _rolledDice.Count; i++)
        {
            _rolledDice[i].RollDie();
        }
    }

    private void CalculateSpell()
    {
        for (int i = 0; i < _spellbook.Spells.Count; i++)
        {
            // does the spell have the same number of dice
            if (_spellbook.Spells[i].Recipie.Count == _selectedDice.Count)
            {
                int counter = 0;
                for (int j = 0; j < _typeRuntimeCollection.Count(); j++)
                {
                    if (_spellbook.Spells[i].Recipie.Contains(_typeRuntimeCollection.List()[j]))
                    {
                        counter++;
                    }
                }
                if (counter == _spellbook.Spells[i].Recipie.Count)
                {
                    Debug.Log(_spellbook.Spells[i].name + " cast!");
                    return;
                }
            }
        }
    }
}
