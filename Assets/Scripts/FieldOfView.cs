using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] [Range(0f, 180f)] float angle;
    [SerializeField] LayerMask targetMask, obstacleMask;

    void Update()
    {
        FindTarget();
    }

    void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, range, targetMask);
        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;

            float distanceTarget = Vector3.Distance(transform.position, collider.transform.position);
            if (Physics.Raycast(transform.position + transform.up, dirTarget, distanceTarget, obstacleMask))
                continue;

            Debug.DrawRay(transform.position + transform.up, dirTarget * distanceTarget, Color.red);
        }
    }

    void OnDrawGizmos()
    {
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position + transform.up, rightDir * range, Color.blue);
        Debug.DrawRay(transform.position + transform.up, leftDir * range, Color.blue);
    }

    Vector3 AngleToDir(float _angle)
    {
        float radian = _angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
