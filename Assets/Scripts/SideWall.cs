using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    GameObject camera;

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 toLook = new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z);
        transform.LookAt(toLook);
    }
}
