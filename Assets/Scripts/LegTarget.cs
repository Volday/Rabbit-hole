using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegTarget : MonoBehaviour
{
    public LegEnd legEnd;
    [HideInInspector] public bool cursorLock = false;
    [HideInInspector] public bool wallLock = false;
    Material material;
    public Color startColor;
    public Color wallColor;
    Vector3 startPosition;
    void Start()
    {
        material = GetComponent<Renderer>().materials[0];
    }
    void Update()
    {
        if (wallLock)
        {
            material.color = wallColor;
        }
        else
        {
            material.color = startColor;
        }
    }

    public void GameRestart() {
        transform.position = Vector3.ClampMagnitude(legEnd.transform.position -
            new Vector3(0, legEnd.transform.position.y, 0), 3) + new Vector3(0, legEnd.transform.position.y, 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        bool hasWall = false;
        if (collision.gameObject.layer == 10) {
            hasWall = true;
        }

        if (hasWall)
        {
            wallLock = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        wallLock = false;
    }
}
