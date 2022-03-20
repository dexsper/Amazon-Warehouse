using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInteraction _interaction;
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;
    private PlayerEconomics _playerEconimcs;

    public PlayerInteraction Interaction => _interaction;
    public PlayerAnimation Animation => _playerAnimation;
    public PlayerMovement Movement => _playerMovement;
    public PlayerEconomics Economics => _playerEconimcs;

    public void Awake()
    {
        _interaction = GetComponent<PlayerInteraction>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerEconimcs = GetComponent<PlayerEconomics>();
    }
}
