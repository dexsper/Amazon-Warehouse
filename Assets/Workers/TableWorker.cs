using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TableWorker : MonoBehaviour
{
    [SerializeField]
    private Workspace _workspace;

    private Animator _animator;

    private void Awake()
    {
        if (_workspace != null)
            _workspace.OnWorkStateChanged.AddListener(UpdateAnimator);

        _animator = GetComponent<Animator>();
    }

    private void UpdateAnimator(bool working)
    {
        _animator.SetBool("IsWork", working);
    }
}
