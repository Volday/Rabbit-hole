using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationSpeed;
    public Transform target, player;
    float mouseX, mouseY;
    Camera camera;
    public GameObject invWall;
    bool setLegTarget = false;
    GameObject legTarget;
    public LayerMask legTargetMask;
    public LayerMask invWallMask;
    bool firstTry = true;
    float maxtargetMovementDistance = 1f;
    Vector3 lastPosition;
    Vector3 currentHit;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        target.position = Vector3.Lerp(target.position ,player.position, 0.1f);
        if (Input.GetAxisRaw("Fire2") > 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            RotateCam();
        }
        else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void FixedUpdate()
    {
        if (legTarget != null) {
            lastPosition = legTarget.transform.position;
        }
        if (Input.GetAxisRaw("Fire2") == 0)
        {
            if (Input.GetAxisRaw("Fire1") == 0)
            {
                firstTry = true;
                setLegTarget = false;
            }

            if (!setLegTarget && Input.GetAxisRaw("Fire1") > 0) {
                RaycastHit hit;
                Vector2 mousePosition = Input.mousePosition;
                Ray ray = camera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out hit, float.MaxValue, legTargetMask)) {
                    if (legTarget == null ||
                        (player.GetComponent<Body>().legs[0].legEnd.legTarget.wallLock
                        && player.GetComponent<Body>().legs[1].legEnd.legTarget.wallLock
                        && player.GetComponent<Body>().legs[2].legEnd.legTarget.wallLock)
                        || !hit.transform.gameObject.GetComponent<LegTarget>().wallLock)
                    {
                        setLegTarget = true;
                        legTarget = hit.transform.gameObject;
                        invWall.transform.position = legTarget.transform.position;
                    }
                }
            }

            if (setLegTarget)
            {
                RaycastHit hit;
                Vector2 mousePosition = Input.mousePosition;
                Ray ray = camera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out hit, float.MaxValue, invWallMask))
                {
                    if (firstTry)
                    {
                        firstTry = false;
                    }
                    else {
                        Body body = player.GetComponent<Body>();
                        float distance = 0;

                        currentHit = legTarget.transform.position + 
                            Vector3.ClampMagnitude((hit.point - 
                            legTarget.transform.position), 
                            maxtargetMovementDistance * 0.4f);

                        for (int t = 0; t < body.legs.Count; t++) {
                            if (legTarget.GetComponent<LegTarget>().legEnd != body.legs[t].legEnd)
                            {
                                distance += (body.legs[t].legEnd.transform.position
                                    - body.legs[t].legEnd.legTarget.transform.position).magnitude;
                            }
                            else {
                                distance += (body.legs[t].legEnd.transform.position
                                    - currentHit).magnitude;
                            }
                        }
                        Debug.Log(distance);
                        if (distance < maxtargetMovementDistance)
                        {
                            legTarget.transform.position = currentHit;
                        }
                        else {
                            legTarget.transform.position = lastPosition;
                        }
                    }
                }
            }
        }
    }

    public void GameRestart() {
        legTarget = null;
        setLegTarget = false;
        firstTry = true;
    }

    void RotateCam() {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -85, 85);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
