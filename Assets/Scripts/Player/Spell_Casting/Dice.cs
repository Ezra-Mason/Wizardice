using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
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
    public ElementType RolledValue => _rolledValue;
    [SerializeField] private ElementType _rolledValue;
    [SerializeField] private float _diceTorque;
    [SerializeField] private float _rerollForce;
    [SerializeField] private DiceFace[] _faces;
    [SerializeField] private Material _normal;
    [SerializeField] private Material _selected;
    [SerializeField] private MeshRenderer _mesh;
    // Start is called before the first frame update
    void OnEnable()
    {
        _rigidbody.useGravity = false;
        _rolledValue = ElementType.NULL;
    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetButtonDown("Fire1"))
        {
            RollDie();
        }*/

        if (_rigidbody.IsSleeping() && _wasRolled && !_hasLanded)
        {
            _hasLanded = true;
            _rigidbody.useGravity = false;
            CheckFaces();
        }
/*        else if (_rigidbody.IsSleeping() && _hasLanded && _rolledValue == ElementType.NULL)
        {
            Reroll();
        }*/
    }

    public void RollDie()
    {
        Debug.Log("Rolled dice");
        if (!_wasRolled && !_hasLanded)
        {
            _wasRolled = true;
            Debug.Log("was rolled = " + _wasRolled);
            Debug.Log("hasnt landed and hasnt been rolled");
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
        Debug.Log("Reset dice");
        _wasRolled = false;
        _hasLanded = false;
        _rigidbody.useGravity = false;
    }
    private void Reroll()
    {
        Debug.Log("Rerolled dice");
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
    public void SelectDice()
    {
        _mesh.material = _selected;
    }
    public void DeselectDice()
    {
        _mesh.material = _normal;
    }

}
