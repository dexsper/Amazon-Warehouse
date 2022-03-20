using System.Collections;
using UnityEngine;

public enum PackageState
{
    New,
    Calculated,
    Sorted
}

public class Package : MonoBehaviour
{
    [SerializeField]
    private PackageState _state;

    private bool _isMove = false;
    public PackageState State
    {
        get { return _state; }
    }

    public void SetState(PackageState state)
    {
        _state = state; 
    }


    public IEnumerator MoveTo(Transform target, Transform parent, float duration)
    {
        _isMove = true;

        float elapsedTime = 0;
        float ratio = elapsedTime / duration;

        Vector3 startPos = transform.position;

        while (ratio < 1f)
        {
            elapsedTime += Time.deltaTime;
            ratio = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPos, target.position, ratio);
            yield return null;
        }

        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;

        _isMove = false;
    }
}
