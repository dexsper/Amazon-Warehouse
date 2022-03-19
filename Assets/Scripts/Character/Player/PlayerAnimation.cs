using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector]
    private PlayerMovement _playerMovement;
    private Animator _animator;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsMove", _playerMovement.IsMove);
    }
}
