using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CardMover : MonoBehaviour
{
    private Transform thisTransfrom;
    float startingY;
    private float rotationSpeed = 0;
    private float movementspeed = 0;
    [SerializeField] private SpawnManager manager;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = manager.cardRotationSpeed;
        movementspeed = manager.cardMovementSpeed;
        thisTransfrom = this.transform;
        startingY = Random.Range(0, 361);
        thisTransfrom.rotation = Quaternion.Euler(0, startingY, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * movementspeed * Time.deltaTime);
        Vector3 targetRotation = transform.eulerAngles + Vector3.up * rotationSpeed * Time.deltaTime;

        // Apply the target rotation
        transform.rotation = Quaternion.Euler(targetRotation);
    }

    
}
