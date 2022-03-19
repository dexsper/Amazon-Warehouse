using Assets.Scripts.Input;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _velocitySensivity = 0.1f;

    [Inject] private IInput _input;

    private Rigidbody _rigidbody;

    [HideInInspector]
    public bool IsMove = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        IsMove = _rigidbody.velocity.magnitude >= _velocitySensivity;

        float h = _input.Horizontal * _moveSpeed;
        float v = _input.Vertical * _moveSpeed;
        _rigidbody.velocity = new Vector3(h, _rigidbody.velocity.y, v);

        if(_rigidbody.velocity.magnitude >= _velocitySensivity)
        {
            transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(_rigidbody.velocity).eulerAngles.y, 0);

        }
    }
}
