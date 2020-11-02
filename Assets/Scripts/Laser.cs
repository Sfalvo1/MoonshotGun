using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] Transform laser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
