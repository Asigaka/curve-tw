using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurvePoint : MonoBehaviour
{
    [SerializeField] private float pointSpeed = 3;

    [HideInInspector] public UnityEvent onDestroy = new UnityEvent();

    private AnimationCurve currentCurve;
    private float timeOnCurve;

    public void Initialize(AnimationCurve curve)
    {
        currentCurve = curve;

        timeOnCurve = currentCurve.keys[0].time;
    }

    private void Update()
    {
        timeOnCurve += Time.deltaTime * pointSpeed;

        transform.localPosition = PosOnCurve(timeOnCurve);

        if (timeOnCurve >= currentCurve.keys[currentCurve.length - 1].time)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        onDestroy.Invoke();
    }

    private Vector2 PosOnCurve(float time) => new Vector2(time, currentCurve.Evaluate(time));
}
