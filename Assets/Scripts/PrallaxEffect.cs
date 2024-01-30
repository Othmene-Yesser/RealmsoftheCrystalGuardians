using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform followTarget;

    //starting position for the Prallax effect
    private Vector2 startingPosition;

    //start Z value of the Prallax effect
    private float startingZ;
    // '=>' means it updates itslfe every frame without being in Void Update
    //Distance that the camera moved from the starting position
    private Vector2 camMoveSinceStart => (Vector2)mainCamera.transform.position - startingPosition;

    float clippingPlane => (mainCamera.transform.position.z + (zDistanceFromTarget >0 ? 1000f : 0.3f));
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    //the further the object moves, the faster the Prallax object will move, "drag its Z value closer to the targetto make i t move slower  "
    float prallaxFactor => Mathf.Abs(zDistanceFromTarget) * clippingPlane;


    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //when the target moves, move the Prallax object the same distance times a muliplier
        Vector2 newPosition = startingPosition + camMoveSinceStart / prallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
