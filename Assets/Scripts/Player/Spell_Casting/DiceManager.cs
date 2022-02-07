using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private float _randomRollPosition;
    [SerializeField] private int _diceAmount;
    [SerializeField] private List<Dice> _rolledDice;
    [SerializeField] private List<GameObject> _selectedDice;
    [SerializeField] private List<GameObject> _heldDice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnStartCasting()
    {
        for (int i = 0; i < _diceAmount; i++)
        {
            Vector3 randPosition = new Vector3(Random.Range(_spawnLocation.position.x -_randomRollPosition, _spawnLocation.position.x+ _randomRollPosition), _spawnLocation.position.y, Random.Range(_spawnLocation.position.z - _randomRollPosition, _spawnLocation.position.z -_randomRollPosition));
            GameObject instance = Instantiate(_dicePrefab, randPosition, Quaternion.identity, transform);
            if (instance.TryGetComponent<Dice>(out Dice die))
            {
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
}
