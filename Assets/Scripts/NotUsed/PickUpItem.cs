using UnityEngine;

public class PickUpItem : Interactable
{

    public Item item;
    // public string itemName = "item";

    void Update()
    {
        if (Input.GetMouseButtonDown(1))  // 1 corresponds to the right mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast to detect items
            if (Physics.Raycast(ray, out hit, 1))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    PickUp();
                }
            }
        }
    }

    void PickUp()
    {

        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
