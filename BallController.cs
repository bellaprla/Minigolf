using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BallController : MonoBehaviour
{
    [Header ("References")]
    public Rigidbody rb;
    Camera mainCamera;

    public float stopVelocity;
    public float shotPower;
    public float maxPower;

    [Header ("Ball Settings")]
    bool isAiming;
    bool isIdle;
    bool isShooting;

    Vector3? worldPoint;

    void Awake()
    {
        mainCamera = Camera.main;
        rb.maxAngularVelocity = 1000;
        isAiming = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < stopVelocity)
        {
            ProcessAim();

            if(Input.GetMouseButtonDown(0))
            {
                if(isIdle) isAiming = true;
            }
            if(Input.GetMouseButtonUp(0))
            {
                isShooting = true;
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
    }
     
    void FixedUpdate()
    {
        if(rb.velocity.magnitude < stopVelocity)
        {
            Stop();
        }
        if (isShooting)
        {
            Shoot(worldPoint.Value);
            isShooting = false;
        }
    }

    private void ProcessAim()
    {
        if(!isAiming && !isIdle) return;
        worldPoint = CastMouseClickRay();

        if(!worldPoint.HasValue) return;
    }

    private Vector3? CastMouseClickRay()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit)) return hit.point;
        else return null;
    }

    private void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        isIdle = true;
    }

    public void Shoot(Vector3 point)
    {
        isAiming = false;

        Vector3 horizontalWorldPoint = new Vector3(point.x, transform.position.y, point.z);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);
        rb.AddForce(direction * strength * shotPower);
        
    }

    

}