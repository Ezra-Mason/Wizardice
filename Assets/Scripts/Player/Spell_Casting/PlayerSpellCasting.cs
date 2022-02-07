using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCasting : MonoBehaviour
{
    [SerializeField] private bool _isCasting;
    [SerializeField] private GameEvent _startedCasting;

    // Start is called before the first frame update
    void Start()
    {
        _isCasting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isCasting && Input.GetButtonDown("Jump"))
        {
            _startedCasting.Raise();
            _isCasting = true;
        }
    }


}
