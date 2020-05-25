using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public List<LegMind> legs;
    [Range(0, 1)]
    public float squatRatio;

    private void Awake()
    {
        GameRestart();
    }
    void Update()
    {
        Vector3 midlePosition = Vector3.zero;
        for (int t = 0; t < legs.Count; t++)
        {
            midlePosition += legs[t].legEnd.transform.position;
        }
        midlePosition /= legs.Count;

        float distaceSumm = 0;
        for (int t = 0; t < legs.Count; t++)
        {
            distaceSumm += legs[t].GetDistanceToEnd();
        }
        distaceSumm /= legs.Count;
        float toUp = 0;
        if (distaceSumm < 4)
        {
            toUp = Mathf.Sqrt(4 - distaceSumm);
        }

        transform.position = new Vector3(midlePosition.x, midlePosition.y + toUp * squatRatio, midlePosition.z);

        Vector3 newRotation = legs[0].legEnd.transform.up;
        for (int t = 1; t < legs.Count; t++)
        {
            newRotation += legs[t].legEnd.transform.up;
        }
        newRotation /= legs.Count;
        transform.LookAt(newRotation + transform.position, transform.up);
    }

    public void GameRestart() {
        transform.position = new Vector3(0, 52, 0);
        for (int t = 0; t < legs.Count; t++) {
            legs[t].legEnd.legTarget.GameRestart();
        }
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().GameRestart();
    }
}
