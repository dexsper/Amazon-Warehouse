using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;


public class Conveyor : MonoBehaviour
{
    [Header("Work Settings")]
    [SerializeField] private float _delayPerPackage = .7f;

    [Header("Movement Settings")]
    [Range(0f, 10f)]
    [SerializeField] private int _moveTime = 3;

    [Header("Audio")]
    [SerializeField] private AudioClip _clip;

    public UnityEvent<bool> OnWorkStateChaged = new UnityEvent<bool>();

    private ConveyorOutput _output;
    private ConveyorInput _input;
    private BezierSpline _spline;
    private AudioSource _source;

    public bool IsWork { get; private set; } = false;

    private void Awake()
    {
        _output = GetComponentInChildren<ConveyorOutput>();
        _input = GetComponentInChildren<ConveyorInput>();
        _spline = GetComponentInChildren<BezierSpline>();
        _source = GetComponent<AudioSource>();
    }

    float timer = 0f;

    private void Update()
    {
        if (_input.Container.HasPackages && _output.Container.Equipped == false)
        {
            SetWork(true);

            timer += Time.deltaTime;

            if (timer >= _delayPerPackage)
            {
                timer = 0f;

                var package = _input.Container.GetPackage();
                package.SetState(PackageState.Sorted);

                var walker = package.gameObject.AddComponent<BezierWalkerWithTime>();
                walker.travelTime = _moveTime;
                walker.spline = _spline;
                walker.onPathCompleted.AddListener(() =>
                {
                    _output.Container.AddPackage(package);
                    Destroy(walker);
                });
            }
        }
        else
        {
            SetWork(false);
        }
    }

    private void SetWork(bool work)
    {
        IsWork = work;

        if (_source != null && _clip != null)
        {
            _source.clip = _clip;

            if (IsWork)
                _source.Play();
            else
                _source.Stop();
        }
    }

}
