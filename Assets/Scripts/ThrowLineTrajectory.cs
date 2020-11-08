using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class ThrowLineTrajectory : MonoBehaviour
{
    [SerializeField] int TrajectorySegment;
    [SerializeField] GameObject ThrowPoint;
    [SerializeField] float timeInterval = 0.05f;
    [SerializeField] LayerMask layerMask;
    private float throwSpeed;
    void Start()
    {
        GetComponent<LineRenderer>().positionCount = TrajectorySegment;
    }

    void Update()
    {
        Debug.DrawRay(ThrowPoint.transform.position, ThrowPoint.transform.forward,Color.red);

        DrawLineRender();
    }


    private void DrawLineRender()
    {
        throwSpeed = GetComponent<ThrowWeapon>().throwSpeed;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        Vector3[] trajectoryArra = CalculateTrajectory(throwSpeed, TrajectorySegment);
        lineRenderer.positionCount = trajectoryArra.Length;
        lineRenderer.SetPositions(trajectoryArra);
    }

    private Vector3[] CalculateTrajectory(float throwSpeed, int SegmentCount)
    {
        List<Vector3> trajectoryList = new List<Vector3>();
        Vector3 Speed = ThrowPoint.transform.forward* throwSpeed;
        Vector3 linerendererPoint = ThrowPoint.transform.position;
        Vector3 previousPoint;
        for(int i = 0; i < SegmentCount; i++)
        {
            trajectoryList.Add(linerendererPoint);
            previousPoint = linerendererPoint;
            linerendererPoint += timeInterval * Speed;
            Speed += Physics.gravity * timeInterval;

            if (DetectCollider(previousPoint, linerendererPoint)!= new Vector3(-999, -999, -999))
            {
                trajectoryList[i] = DetectCollider(previousPoint, linerendererPoint);
                break;
            }
        }
        return trajectoryList.ToArray();
    }

    private Vector3 DetectCollider(Vector3 start, Vector3 end)
    {
        Vector3 collisionPoint;

        float rayCastDistance = Vector3.Distance(start, end);
        RaycastHit hitInfo;
        if(Physics.Raycast(new Ray(start,(end-start)),out hitInfo,rayCastDistance,layerMask))
        {
            collisionPoint = hitInfo.point;
            return collisionPoint;
        }
        else
        {
            return new Vector3(-999,-999,-999);
        }
    }

}
