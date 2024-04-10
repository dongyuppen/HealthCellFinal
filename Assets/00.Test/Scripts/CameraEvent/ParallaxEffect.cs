using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    // Reference to the camera used for parallax effect
    public Camera cam;
    // Reference to the target the parallax effect follows
    public Transform followTarget;

    // Starting position for the parallax game object
    Vector2 startingPosition;

    // Start z value of the parallax game object
    float startingZ;

    // Calculates the distance that the camera has moved since the start
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    // Calculates the z distance between the parallax object and the target
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // Calculates the clipping plane distance based on camera position and z distance from the target
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // Calculates the parallax factor based on z distance from the target and clipping plane
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        // Store the starting position and z value of the parallax object
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position based on the parallax effect
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        // Apply the new position to the parallax object
        transform.position = new Vector3(newPosition.x, newPosition.y,startingZ);
    }
}
