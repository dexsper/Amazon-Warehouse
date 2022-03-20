using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PackageState
{
    New,
    Calculated,
    Sorted
}

[System.Serializable]
public class PackageVisualObject
{
    public PackageState State;
    public List<GameObject> Objects;
}

public class Package : MoveObject
{
    [SerializeField]
    private PackageState _state;

    [Header("Visual Settings")]
    [SerializeField] private List<PackageVisualObject> _visualObjectsStates;

    public PackageState State
    {
        get { return _state; }
    }

    public void SetState(PackageState state)
    {
        _state = state;

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        for (int i = 0; i < _visualObjectsStates.Count; i++)
        {
            if(_state == _visualObjectsStates[i].State)
            {
                for (int j = 0; j < _visualObjectsStates[i].Objects.Count; j++)
                {
                    _visualObjectsStates[i].Objects[j].SetActive(true);
                } 
            }
        }
    }
}
