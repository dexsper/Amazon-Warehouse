using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WorkSpace : PackageContainer, IInteractable
{
    [Header("Work Settings")]
    [SerializeField] private float _workTime = 5f;

    [Inject]
    private Player _player;

    [SerializeField]
    private List<Transform> _outputCells;

    public InteractionType InteractType => InteractionType.Table;

    private bool _isWork = false;

    public bool CanInteract()
    {
        if (_player.Interaction.HasInteraction && _player.Interaction.CurrentInteraction != (this as IInteractable)) return false;
        if (_isWork || Equipped) return false;
        if (_player.Interaction.HasPackage(PackageState.New) == false) return false;

        return true;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        if (_player.Interaction.Equipped == false) return;

        var package = _player.Interaction.GetPackage();

        AddPackage(package);
    }

    private void Update()
    {
        if(Equipped && _isWork == false)
        {
            _isWork = true;
        }
    }
}
