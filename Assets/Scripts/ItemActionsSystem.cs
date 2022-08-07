using UnityEngine;

public class ItemActionsSystem : MonoBehaviour
{
    [Header("OTHER SCIPTS REFERENCES")]

    [SerializeField] Equipment equipment;

    [Header("ITEM ACTION SYSTEM VARIABLES")]

    public GameObject actionPanel;

    [SerializeField] private Transform dropPoint;

    [SerializeField] private GameObject useItemButton;
    [SerializeField] private GameObject equipItemButton;
    [SerializeField] private GameObject dropItemButton;
    [SerializeField] private GameObject destroyItemButton;

    [HideInInspector] public ItemData itemCurrentlySelected;

    public void OpenActionPanel(ItemData item, Vector3 slotPosition)
    {
        itemCurrentlySelected = item;

        if (item == null)
        {
            actionPanel.SetActive(false);
            return;
        }

        switch (item.itemType)
        {
            case ItemType.Ressource:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(false);
                break;
            case ItemType.Equipment:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(true);
                break;
            case ItemType.Consumable:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;
        }

        actionPanel.transform.position = slotPosition;
        actionPanel.SetActive(true);
    }

    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
        itemCurrentlySelected = null;
    }

    public void UseActionButton()
    {
        print("Use item : " + itemCurrentlySelected.name_);
        CloseActionPanel();
    }

    public void DropActionButton()
    {
        GameObject instantiatedItem = Instantiate(itemCurrentlySelected.prefab);
        instantiatedItem.transform.position = dropPoint.position;
        Inventory.Singleton.RemoveItem(itemCurrentlySelected);
        Inventory.Singleton.RefreshContent();
        CloseActionPanel();
    }

    public void DestroyActionButton()
    {
        Inventory.Singleton.RemoveItem(itemCurrentlySelected);
        Inventory.Singleton.RefreshContent();
        CloseActionPanel();
    }

    public void EquipActionButton()
    {
        equipment.EquipAction();
    }
}
