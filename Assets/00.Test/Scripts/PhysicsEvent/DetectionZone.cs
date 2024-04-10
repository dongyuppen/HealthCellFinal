using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    // Event invoked when no colliders remain in the detection zone
    public UnityEvent noCollidersRemain;

    // List to store detected colliders
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    // Reference to the collider component attached to this GameObject
    Collider2D col;


    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Add the collided collider to the list of detected colliders
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remove the exited collider from the list of detected colliders
        detectedColliders.Remove(collision);

        // If no colliders remain in the detection zone, invoke the noCollidersRemain event
        if (detectedColliders.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }
    }
}
