using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{
    [SerializeField] private string _tag;
    public ElementType Value => _value;
    [SerializeField] private ElementType _value;
    public bool OnGround => _onGround;
    [SerializeField] private bool _onGround;
    private void OnEnable()
    {
        _onGround = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag ==_tag)
        {
            _onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == _tag)
        {
            _onGround = false;
        }
    }

}
