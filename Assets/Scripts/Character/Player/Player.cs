using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerInteraction), typeof(PlayerAnimation))]
public class Player : MonoBehaviour
{
    private PlayerInteraction _interaction;
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;

    public PlayerInteraction Interaction
    {
        get { return _interaction; }
    }

    public PlayerAnimation PlayerAnimation
    {
        get { return _playerAnimation; }
    }

    public PlayerMovement PlayerMovement
    {
        get { return _playerMovement; }
    }

    public void Awake()
    {
        _interaction = GetComponent<PlayerInteraction>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerMovement = GetComponent<PlayerMovement>();
    }
}
