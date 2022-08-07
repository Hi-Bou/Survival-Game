using System.Collections;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private float pickupRange = 2.6f;

    public PickupBehaviour playerPickupBehaviour;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private GameObject pickupText;

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, layerMask))
        {
            if(hit.transform.CompareTag("Item"))
            {
                pickupText.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    playerPickupBehaviour.DoPickup(hit.transform.gameObject.GetComponent<Item>());

                    if(Inventory.Singleton.isInventoryOpen == false)
                        StartCoroutine(PickUpItem());
                }
            }
        }
        else
        {
            pickupText.SetActive(false);
        }
    }

    private IEnumerator PickUpItem()
    {
        Inventory.Singleton.OpenInventory();
        yield return new WaitForSeconds(2f);
        TooltipSystem.Singleton.Hide();
        Inventory.Singleton.CloseInventory();
    }
}
