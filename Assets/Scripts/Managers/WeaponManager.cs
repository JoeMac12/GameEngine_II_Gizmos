using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Camera playerCam;

    [SerializeField] float defaultDistance = 10f;
    [SerializeField] LayerMask cubeFilter;
    [SerializeField] LayerMask groundFilter;

    private Renderer lastHitRenderer = null;

    private void Start()
    {
        playerCam = Camera.main;
    }

    private void FixedUpdate()
    {
        float distance = defaultDistance;
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit groundHit, defaultDistance, groundFilter))
        {
            distance = groundHit.distance;
        }
        Debug.Log($"Raycast Distance {distance}");

        Debug.DrawLine(playerCam.transform.position, playerCam.transform.position + playerCam.transform.forward * distance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, distance, cubeFilter))
        {
            Debug.Log($"Hit Object: {hit.collider.name}, Distance: {hit.distance}");

            if (hit.collider.TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Color.red;

                if (lastHitRenderer != null && lastHitRenderer != renderer)
                {
                    lastHitRenderer.material.color = Color.blue;
                }

                lastHitRenderer = renderer;
            }
        }
        else
        {
            if (lastHitRenderer != null)
            {
                lastHitRenderer.material.color = Color.blue;
                lastHitRenderer = null;
            }
        }
    }
}
