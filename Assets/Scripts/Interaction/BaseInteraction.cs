using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class BaseInteraction : MonoBehaviour, IInteractable
{

    [Header("Visual")]
    [SerializeField]
    protected GameObject _visualObject;
    [Header("Interaction Settings")]
    private float _delay = .1f;

    [Inject]
    protected Player _player;

    public InteractionType InteractType { get; }
    public abstract bool CanInteract();
    public abstract void Interact();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(_player.gameObject == other.gameObject)
        {
            _player.Interaction.SetInteract(this);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            _player.Interaction.SetInteract(null);
        }
    }


    float timer = 0f;
    
    protected virtual void OnTriggerStay(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            timer += Time.deltaTime;

            if (timer >= _delay)
            {
                Interact();
                timer = 0f;
            }
        }
    }

    protected virtual void Update()
    {
        if(_visualObject != null)
            _visualObject.SetActive(CanInteract());
    }
}
