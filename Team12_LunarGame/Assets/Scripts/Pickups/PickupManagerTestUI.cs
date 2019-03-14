using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupManagerTestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private const string TextTemplate = "Time until next pickup: {0}";
    private PickupManager _pickupManager;

    private void Start()
    {
        _pickupManager = GetComponentInParent<PickupManager>();
    }
    private void Update()
    {
        float _timeRemaining = _pickupManager.GetTimeRemaining();
        _text.text = string.Format(TextTemplate, _timeRemaining.ToString("00.00"));
    }
}
