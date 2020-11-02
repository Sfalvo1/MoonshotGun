using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float controlSpeed = 5f;
    [SerializeField] float adjustAngle = 90f;

    [SerializeField] Transform laserTransform;

    [Header("Debug")]
    public bool godMode = true;

    private float angle;

    float xThrow, yThrow;

    Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        Vector3 dir = Input.mousePosition - camera.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + adjustAngle, Vector3.forward);
    }

    private void ProcessMovement()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime; // How much to change
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset; // Without this will make the ship bounce back. This locks in the new position
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -10.5f, 10.5f); // Clamps rawNewXPos to the given range (-xRange, xRange)
        float clampedYPos = Mathf.Clamp(rawYPos, -5.8f, 5.8f);

        transform.localPosition = new Vector2(clampedXPos, clampedYPos);
    }
    private void ProcessFiring()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoonProjectile.Create(transform.position, camera.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetMouseButtonDown(1))
        {
            LaserProjectile.Create(laserTransform.position, camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
