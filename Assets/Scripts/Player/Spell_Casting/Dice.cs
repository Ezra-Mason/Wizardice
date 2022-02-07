using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    NULL,
    WATER,
    FIRE,
    AIR,
    EARTH,
    LIFE,
    DEATH
}
public class Dice : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private bool _wasRolled;
    [SerializeField] private bool _hasLanded;
    [SerializeField] private ElementType _rolledValue;
    [SerializeField] private float _diceTorque;
    [SerializeField] private float _rerollForce;
    [SerializeField] private DiceFace[] _faces;
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        _rigidbody.useGravity = false;
        _rolledValue = ElementType.NULL;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            RollDie();
        }

        if (_rigidbody.IsSleeping() && _wasRolled && !_hasLanded)
        {
            _hasLanded = true;
            _rigidbody.useGravity = false;
            CheckFaces();
        }
        else if (_rigidbody.IsSleeping() && _hasLanded && _rolledValue == ElementType.NULL)
        {
            Reroll();
        }
    }

    public void RollDie()
    {
        if (!_wasRolled && !_hasLanded)
        {
            _wasRolled = true;
            _rigidbody.useGravity = true;
            Vector3 randomTorque = new Vector3(Random.Range(0, _diceTorque), Random.Range(0, _diceTorque), Random.Range(0, _diceTorque));
            _rigidbody.AddTorque(randomTorque);
        }
        if (_wasRolled && _hasLanded)
        {
            ResetDie();
        }
    }


    private void ResetDie()
    {
        _wasRolled = false;
        _hasLanded = false;
        _rigidbody.useGravity = false;
    }
    private void Reroll()
    {
        ResetDie();
        _wasRolled = true;
        _rigidbody.useGravity = true;
        //fire upwards
        Vector3 upForce = new Vector3(0f, _rerollForce, 0f);
        _rigidbody.AddForce(upForce);
        //add spin
        Vector3 randomTorque = new Vector3(Random.Range(0, _diceTorque), Random.Range(0, _diceTorque), Random.Range(0, _diceTorque));
        _rigidbody.AddTorque(randomTorque);
    }

    private void CheckFaces()
    {
        for (int i = 0; i < _faces.Length; i++)
        {
            if (_faces[i].OnGround)
            {
                _rolledValue = _faces[i].Value;
                Debug.Log("Rolled value = " +_faces[i].Value);
                return;
            }
        }
    }
}