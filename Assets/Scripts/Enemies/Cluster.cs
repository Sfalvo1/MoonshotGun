using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluster : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    public int childCount = 4;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Enemy>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
    }
}
