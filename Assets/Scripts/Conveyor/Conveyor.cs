using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Conveyor : MonoBehaviour
{
    [Header("Work Settings")]
    [SerializeField] private float _workTime = 5f;

    private ConveyorOutput _output;

    private List<ConveyorInput> _inputs = new List<ConveyorInput>();

    public void AddInput(ConveyorInput conveyorInput)
    {
        _inputs.Add(conveyorInput); 
    }

    public void SetOutput(ConveyorOutput conveyorOutput)
    {
        _output = conveyorOutput;
    }
}
