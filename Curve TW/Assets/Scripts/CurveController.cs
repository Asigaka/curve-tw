using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveController : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private CurveView view;
    [SerializeField] private CurvePoint curvePointPrefab;
    [SerializeField] private float timeBetweenSpawnPoints = 1;
    [SerializeField] private LineRenderer curveLine;

    private bool canSpawnPoint = true;
    private short destroyedPoints;
    private float holdTime;

    private void Start()
    {
        curveLine.positionCount = curve.length;

        for (int i = 0; i < curve.length; i++)
        {
            Vector2 linePos = new Vector2(curve.keys[i].time, curve.keys[i].value);
            curveLine.SetPosition(i, linePos);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SpawnPoint();
            holdTime += Time.deltaTime;
            view.SetHoldTime(holdTime);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            holdTime = 0;
            view.SetHoldTime(holdTime);
        }
    }

    private void SpawnPoint()
    {
        if (!canSpawnPoint) return;

        CurvePoint point = Instantiate(curvePointPrefab, new Vector2(curve.keys[0].time, curve.keys[0].value), Quaternion.identity, curveLine.transform);
        point.Initialize(curve);
        point.onDestroy.AddListener(IncreaseDestroyedPoints);

        canSpawnPoint = false;

        Invoke(nameof(ResetCurveSpawnTime), timeBetweenSpawnPoints);
    }

    private void ResetCurveSpawnTime()
    {
        canSpawnPoint = true;
    }

    private void IncreaseDestroyedPoints()
    {
        destroyedPoints++;
        view.SetDestroyedPoints(destroyedPoints);
    }
}
