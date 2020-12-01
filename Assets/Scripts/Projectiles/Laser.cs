using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] Transform laser;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (!laser.gameObject.activeSelf)
            {
                laser.gameObject.SetActive(true);
            }
        }
        else
        {
            if (laser.gameObject.activeSelf)
            {
                laser.gameObject.SetActive(false);
            }
        }
    }
}
