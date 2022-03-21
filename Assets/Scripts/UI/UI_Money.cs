using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_Money : MonoBehaviour
{
    private TextMeshProUGUI _textComponent;

    [Inject]
    private Player _player;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textComponent.text = _player.Economics.Money.ToString();
    }
}
