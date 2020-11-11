using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += -transform.up * movementSpeed * Time.deltaTime;

        // RotateMoon();
    }

    //public void SpawnMoonChip(Vector2 hitPosition)
    //{
    //    MoonChip moonChip = Instantiate(moonChipPrefab, hitPosition, Quaternion.identity);
    //    moonChip.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f);
    //}

    private void RotateMoon()
    {
        // gameObject.transform.Rotate(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime); // Goofy flipping
        // gameObject.transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        // gameObject.transform.Rotate(transform.position.x + Time.deltaTime, transform.position.y, transform.position.z);
    }

}
