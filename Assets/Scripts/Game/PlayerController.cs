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

    [Header("Shooting")]
    public int moonChunkAmount;

    [SerializeField] float shootingTimerMax;
    float shootTimer;

    [SerializeField] float laserTimerMax;
    float laserTimer;


    [SerializeField] Transform[] Guns;
    [SerializeField] Transform[] GunTargets;

    [Header("Debug")]
    public bool godMode = true;
    public int moonChunkDebugStartAmount;

    private float angle;

    float xThrow, yThrow;

    Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        if(moonChunkDebugStartAmount > 0)
        {
            moonChunkAmount += moonChunkDebugStartAmount;
        }
        AmmoUI.Instance.SetAmmoText(moonChunkAmount);

        // AmmoUI.Instance.SetAmmoText(moonChunkAmount);

        camera = Camera.main;
        shootTimer = shootingTimerMax;
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
        shootTimer -= Time.deltaTime;
        laserTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && shootTimer <= 0)
        {
            shootTimer = shootingTimerMax;
            ShootGuns();
            AmmoUI.Instance.SetAmmoText(moonChunkAmount);
        }
        if (Input.GetMouseButton(1) && laserTimer <= 0)
        {
            laserTimer = laserTimerMax;
            LaserProjectile.Create(laserTransform.position, camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void ShootGuns()
    {
        if(moonChunkAmount > 0)
        {
            //MoonProjectile.Create(Guns[0].position, camera.ScreenToWorldPoint(Input.mousePosition) + 
            //    new Vector3(Guns[0].position.x, 0, 0));
            MoonProjectile.Create(Guns[0].position, GunTargets[0].position);
            moonChunkAmount--;
        }
        if (moonChunkAmount > 0)
        {
            //    MoonProjectile.Create(Guns[1].position, camera.ScreenToWorldPoint(Input.mousePosition) +
            //        new Vector3(Guns[1].position.x, 0, 0));
            //    moonChunkAmount--;
            MoonProjectile.Create(Guns[1].position, GunTargets[1].position);
            moonChunkAmount--;
        }
        if (moonChunkAmount > 0)
        {
            //    MoonProjectile.Create(Guns[2].position, camera.ScreenToWorldPoint(Input.mousePosition) +
            //        new Vector3(Guns[2].position.x, 0, 0));
            //    moonChunkAmount--;
            MoonProjectile.Create(Guns[2].position, GunTargets[2].position);
            moonChunkAmount--;
        }
        if (moonChunkAmount > 0)
        {
            //MoonProjectile.Create(Guns[3].position, camera.ScreenToWorldPoint(Input.mousePosition));
            //moonChunkAmount--;
            MoonProjectile.Create(Guns[3].position, GunTargets[3].position);
            moonChunkAmount--;
        }
    }

    private void OnDestroy()
    {
        GameOverUI.Instance.Show();
    }

}
