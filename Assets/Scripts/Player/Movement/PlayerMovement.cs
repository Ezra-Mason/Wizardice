using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _moveDirection;
    private bool _canMove;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _canMove = true;   
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        if (_moveDirection.magnitude >= 0.1 && _canMove)
        {
            float targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            Vector3 move = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(move.normalized * _speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), Time.deltaTime * _turnSpeed);
        }

    }
}
