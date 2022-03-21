using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class WorldCanvas : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    private WorldUI _worldUIPrefab;

    [SerializeField]
    private TextMeshProUGUI _textPrefab;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public WorldUI SpawnIcon(Transform target, Vector3 offset, Sprite sprite)
    {
        var obj = Instantiate(_worldUIPrefab);
        obj.transform.SetParent(transform, false);
        obj.SetData(target, _camera, offset);
        obj.transform.position = new Vector3(-1000, -1000);
        obj.gameObject.AddComponent<Image>().sprite = sprite;

        return obj;
    }
    
    public IEnumerator ShowText(Transform target, Vector3 offset, string text, float delay)
    {
        var textObj = Instantiate(_textPrefab);
        textObj.transform.SetParent(transform, false);
        textObj.transform.position = new Vector3(-1000, -1000);
        textObj.text = text;

        textObj.gameObject.AddComponent<WorldUI>().SetData(target, _camera, offset);
        
        yield return new WaitForSeconds(delay);

        Destroy(textObj.gameObject);
    }
}
