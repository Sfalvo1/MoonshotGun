using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private Vector2 moveDir;

    private float textureUnitSizeY;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;

        moveDir = ((((Vector2)transform.position - new Vector2(0, -50f).normalized * -1) / 200f));
    }

    void LateUpdate()
    {
        transform.Translate(moveDir);

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(0, deltaMovement.y, 0);
        lastCameraPosition = cameraTransform.position;

        if(cameraTransform.position.y - transform.position.y >= textureUnitSizeY)
        {
            float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
        }

    }
}
