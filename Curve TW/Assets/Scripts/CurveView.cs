using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurveView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI holdTimeValue;
    [SerializeField] private TextMeshProUGUI destroyedPointsValue;

    public void SetHoldTime(float holdTime) => holdTimeValue.text = holdTime.ToString("#.#");
    public void SetDestroyedPoints(float points) => destroyedPointsValue.text = points.ToString();

    private void Start()
    {
        SetHoldTime(0);
        SetDestroyedPoints(0);
    }
}
