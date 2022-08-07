using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("OTHER SCRIPT REFERENCES")]

    [SerializeField] private Equipment equipment;
    [SerializeField] private ItemActionsSystem itemActionsSystem;

    [Header("INVENTORY SYSTEM VARIABLES")]

    [SerializeField] private List<ItemData> content = new List<ItemData>();
    
    [SerializeField] private GameObject inventoryPanel;

    [SerializeField] private Transform inventorySlotsParent;

    public Sprite emptySlotVisual;


    const int InventorySize = 24;
    public bool isInventoryOpen = false;

    #region Singleton
    private static Inventory _singleton;

    public static Inventory Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(Inventory)} instance already exists, destroying duplicated!");
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        Singleton = this;
    }
    #endregion Singleton

    private void Start()
    {
        CloseInventory();
        RefreshContent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isInventoryOpen)
                CloseInventory();
            else
                OpenInventory();
        }
    }

    public void AddItem(ItemData item)
    {
        content.Add(item);
        RefreshContent();
    }

    public void RemoveItem(ItemData item)
    {
        content.Remove(item);
        RefreshContent();
    }

    public void OpenInventory()
    {
        isInventoryOpen = true;
        inventoryPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        itemActionsSystem.actionPanel.SetActive(false);
        TooltipSystem.Singleton.Hide();
        isInventoryOpen = false;
    }

    public void RefreshContent()
    {
        //vider tout les slot
        for (int i = 0; i < inventorySlotsParent.childCount; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = null;
            currentSlot.itemVisual.sprite = emptySlotVisual;
        }

        //Mettre le contenu dans les slot
        for (int i = 0; i < content.Count; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = content[i];
            currentSlot.itemVisual.sprite = content[i].visual;
        }

        equipment.UpdateEquipmentsDesequipButton();
    }

    public bool IsFull()
    {
        return InventorySize == content.Count;
    }
}
