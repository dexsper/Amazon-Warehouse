using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class WorkSpace : BaseInteraction
{
    [Header("Work Settings")]
    [SerializeField] private float _workTime = 5f;

    [Header("Containers")]
    [SerializeField] private PackageContainer _inputContainer;
    [SerializeField] private PackageContainer _outputContainer;

    public InteractionType InteractType => InteractionType.Table;

    private bool _isWork = false;

    public override bool CanInteract()
    {
        if (_player.Interaction.HasInteraction && _player.Interaction.CurrentInteraction != (this as IInteractable)) return false;

        if (_isWork) return false;

        if (_outputContainer.HasPackages)
        {
            if (_player.Interaction.Container.PackagesCount > 0 && _player.Interaction.Container.HasPackage(PackageState.Calculated) == false) return false;
        }
        else
        {
            if ( _inputContainer.Equipped) return false;
            if(_player.Interaction.Container.HasPackages == false) return false;
            if (_player.Interaction.Container.PackagesCount > 0 && _player.Interaction.Container.HasPackage(PackageState.New) == false) return false;
        }

        return true;
    }

    public override void Interact()
    {
        if (!CanInteract()) return;

        if (_outputContainer.HasPackages)
        {
            var package = _outputContainer.GetPackage();

            _player.Interaction.Container.AddPackage(package);
        }
        else
        {
            var package = _player.Interaction.Container.GetPackage();

            _inputContainer.AddPackage(package);
        }
    }


    private void Awake()
    {
        secondsPerPackage = _workTime / _inputContainer.Cells.Count;
    }

    float workTimer = 0f;

    float secondsPerPackage;

    protected override void Update()
    {
        base.Update();

        if (_inputContainer.Equipped && _isWork == false)
        {
            _isWork = true;
        }

        if(_isWork)
        {
            if(_inputContainer.PackagesCount == 0)
            {
                _isWork = false;
            }

            workTimer += Time.deltaTime;

            if(workTimer >= secondsPerPackage)
            {
                workTimer = 0;

                var package = _inputContainer.GetPackage();
                package.SetState(PackageState.Calculated);
                _outputContainer.AddPackage(package);
            }
        }
    }
}
