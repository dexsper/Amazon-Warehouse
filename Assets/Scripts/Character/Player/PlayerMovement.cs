using Assets.Scripts.Input;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed;

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
        IsMove = _input.Horizontal != 0 || _input.Vertical != 0;
        float h = _input.Horizontal * _moveSpeed * Time.deltaTime;
        float v = _input.Vertical * _moveSpeed * Time.deltaTime;
        _rigidbody.velocity = new Vector3(h, _rigidbody.velocity.y, v);

        if(_input.Horizontal != 0 || _input.Vertical != 0)
        {
            
        }
    }
}
