using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMovement : MonoBehaviour
{
    public float xSpeed = 5;
    public float ySpeed = 5;

    public bool reverse;

    Material material;
    MeshRenderer meshRenderer;

    Vector2 offset;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;

        offset = material.GetTextureOffset("_BaseMap");
    }
    private void Update()
    {
        if (ReferenceKeeper.Instance.GameManager.gameStarted)
        {
            if (!reverse)
            {
                offset.y -= Time.deltaTime * ySpeed;
                offset.x -= Time.deltaTime * xSpeed;
            }
            else
            {
                offset.y += Time.deltaTime * ySpeed;
                offset.x += Time.deltaTime * xSpeed;
            }

            material.SetTextureOffset("_BaseMap", offset);
        }

    }
}
