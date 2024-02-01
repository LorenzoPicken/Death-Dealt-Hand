using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    Vector3 mousePosition;
    private string cardslots = "cardslots";
    [SerializeField] GameManager gm;

    // Gets the vector3 position of the card 
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
    // Saves the position from the cursor to the card
    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }
    // Moves the card, updating its position while dragging
    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }
    // Detects if card is place in an available spot
    private void OnMouseUp()
    {
        Ray ray = new Ray(transform.position, -transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
             
           if(hit.collider.tag == cardslots)
            {
                transform.position = hit.collider.transform.position;
                //Two cards snap event
                GetComponent<Card>();
                hit.collider.GetComponent<Card>();
                //GameManager.instance.OnCardsPlayed();
            }
        }
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, -transform.forward * 15);
    }

}
