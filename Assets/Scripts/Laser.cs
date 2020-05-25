using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    GameObject player;
    public LayerMask layer;
    LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, float.MaxValue, layer))
        {
            if (hit.transform.gameObject.layer == 11) {
                player.GetComponent<Body>().GameRestart();
            }
            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
            }
        }
        else {
            lineRenderer.SetPosition(1, transform.forward * 5000);
        }
    }
}
