using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMind : MonoBehaviour
{
    public GameObject secondLeg;
    public LegEnd legEnd;
    public LegTarget target;
    private float legLenght = 2;
    private Quaternion startRotation;
    private Quaternion startSecondLegRotation;
    public float maxXAndle;

    void Start()
    {
        startRotation = transform.rotation;
        startSecondLegRotation = secondLeg.transform.rotation;
    }

    
    public void LateUpdate()
    {
        float distanceToTarget = GetDistanceToEnd();
        transform.rotation = startRotation;
        secondLeg.transform.rotation = startSecondLegRotation;
        transform.LookAt(target.transform.position);
        if (distanceToTarget < legLenght * 2)
        {
            float angleToRotate = Mathf.Acos(distanceToTarget / (2 * legLenght)) * Mathf.Rad2Deg;
            transform.Rotate(new Vector3(-angleToRotate, 0, 0));
            secondLeg.transform.Rotate(new Vector3(angleToRotate * 2, 0, 0));
        }
    }

    public float GetDistanceToEnd() {
        return (target.transform.position - transform.position).magnitude;
    }
}
