using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;


public class Workspace : MonoBehaviour
{
    [Header("Work Settings")]
    [SerializeField] private float _workTime = 5f;

    [Header("Containers")]
    [SerializeField] private WorkspaceInput _input;
    [SerializeField] private WorkspaceOutput _output;


    public InteractionType InteractType => InteractionType.Table;
    public bool IsWork { get; private set; } = false;

    public UnityEvent<bool> OnWorkStateChanged = new UnityEvent<bool>();

    float workTimer = 0f;
    float secondsPerPackage;

    private void Awake()
    {
        _input = GetComponentInChildren<WorkspaceInput>();
        _output = GetComponentInChildren<WorkspaceOutput>();  

        secondsPerPackage = _workTime / _input.Container.Cells.Count;
    }

    private void Update()
    {
        if (_input.Container.Equipped && IsWork == false && _output.Container.Equipped == false)
        {
            SetWork(true);
        }

        if (IsWork)
        {
            if(_input.Container.PackagesCount == 0)
            {
                SetWork(false);
            }

            workTimer += Time.deltaTime;

            if(workTimer >= secondsPerPackage)
            {
                workTimer = 0;

                var package = _input.Container.GetPackage();
                package.SetState(PackageState.Calculated);

                _output.Container.AddPackage(package);
            }
        }
    }

    private void SetWork(bool work)
    {
        IsWork = work;
        OnWorkStateChanged?.Invoke(work);
    }
}
