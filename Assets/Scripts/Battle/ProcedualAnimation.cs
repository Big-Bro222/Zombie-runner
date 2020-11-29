using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PositionInfo
{
    public PositionInfo(Vector3 position,Vector3 eulerAngle)
    {
        this.position = position;
        this.rotation= eulerAngle;
    }
    public Vector3 position;
    public Vector3 rotation;
}

public class ProcedualAnimation : MonoBehaviour
{
    public UnityEvent onSelected;
    public UnityEvent onDeselected;

    [SerializeField] List<PositionInfo> movementPositions;
    [SerializeField] float time;
    public bool islocal;
    private bool positivemovement;
    void Start()
    {
        positivemovement = true;
        PositionInfo positionlocalInfo = new PositionInfo(transform.localPosition,transform.localEulerAngles);
        //PositionInfo positionInfo = new PositionInfo(transform.position, transform.rotation.eulerAngles);
        if (islocal)
        {
            movementPositions.Insert(0, positionlocalInfo);
        }
        //else
        //{
        //    movementPositions.Insert(0, positionInfo);
        //}
    }

    private void OnMouseDown()
    {

        PositionInfo currentPositionlocalInfo= new PositionInfo(transform.localPosition, transform.localEulerAngles);
        if (positivemovement&&islocal)
        {
            onSelected.Invoke();
            StartCoroutine(SmoothLerp(currentPositionlocalInfo, movementPositions[movementPositions.Count - 1]));
        }
        else if (!positivemovement && islocal)
        {
            onDeselected.Invoke();
            StartCoroutine(SmoothLerp(currentPositionlocalInfo, movementPositions[0]));
        }
        else if (!positivemovement && !islocal)
        {
            Debug.LogError("world positive is not implemented");
        }
        else if (!positivemovement && !islocal)
        {
            Debug.LogError("world negative is not implemented");

        }


    }


    private IEnumerator SmoothLerp(PositionInfo startingPoint, PositionInfo finalPoint)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.localPosition = Vector3.Lerp(startingPoint.position, finalPoint.position, (elapsedTime / time));
            transform.localEulerAngles = Vector3.Slerp(startingPoint.rotation, finalPoint.rotation, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        positivemovement = !positivemovement;
    }

}
