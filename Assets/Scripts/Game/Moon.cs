using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    public float spinSpeed;
    public float spin = 0;

    void Start()
    {
        
    }

    void Update()
    {
        //transform.position += -transform.up * movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector2(transform.position.x, transform.position.y - 25f),
            movementSpeed * Time.deltaTime
            );

        RotateMoon();
    }

    //public void SpawnMoonChip(Vector2 hitPosition)
    //{
    //    MoonChip moonChip = Instantiate(moonChipPrefab, hitPosition, Quaternion.identity);
    //    moonChip.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f);
    //}

    private void RotateMoon()
    {
        spin += spinSpeed / 10;

        transform.eulerAngles = new Vector3(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            spin);
    }

}
