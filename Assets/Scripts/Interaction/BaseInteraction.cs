using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class BaseInteraction : MonoBehaviour, IInteractable
{

    [Header("Visual")]
    [SerializeField] private bool _enableWorldIcon = false;
    [SerializeField] protected Sprite _iconSprite;
    [SerializeField] private Vector3Int _iconOffset;

    [Header("Interaction Settings")]
    private float _delay = .1f;

    [Inject]
    protected Player _player;

    [Inject]
    protected WorldCanvas _worldCanvas;

    public InteractionType InteractType { get; }
    public abstract bool CanInteract();
    public abstract void Interact();

    private float timer = 0f;
    private WorldUI _visualObject;

    protected virtual void Awake()
    {
        if(_iconSprite != null && _enableWorldIcon)
            _visualObject = _worldCanvas.SpawnIcon(transform, _iconOffset, _iconSprite);
    }

    protected virtual void Update()
    {
        if (_visualObject != null)
            _visualObject.gameObject.SetActive(CanInteract());
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_player.gameObject == other.gameObject)
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
    protected virtual void OnTriggerStay(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            if (CanInteract() == false)
            {
                OnTriggerExit(other);
            }
            else
            {
                timer += Time.deltaTime;

                if (timer >= _delay)
                {
                    Interact();
                    timer = 0f;
                }
            }
        }
    }
}
