
using System;
using UnityEngine;


[RequireComponent(typeof(PackageContainer), typeof(AudioSource))]
public class PackageContainerAudio : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioClip _addedClip;

    [Header("Sounds Settings")]
    [SerializeField] private float _pitchMin = 0.9f;
    [SerializeField] private float _pitchMax = 1.2f;

    private AudioSource _audioSource;
    private PackageContainer _packageContainer;

    private void Awake()
    {
        _packageContainer = GetComponent<PackageContainer>();
        _audioSource = GetComponent<AudioSource>();

        _packageContainer.OnPackageAdded.AddListener(OnPackageAdded);
    }

    private void OnPackageAdded(Package package)
    {
        _audioSource.pitch = Mathf.Lerp(_pitchMin, _pitchMax, _packageContainer.PackagesCount / _packageContainer.Cells.Count);
        _audioSource.PlayOneShot(_addedClip);
    }
}

