using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector]
    private Player _player;
    private Animator _animator;

    private void Start()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsMove", _player.Movement.IsMove);
        _animator.SetBool("HasPackages", _player.Interaction.Container.HasPackages);
    }
}
