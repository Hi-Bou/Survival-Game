using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    [Header("OTHER SCRIPTS REFERENCES")]

    [SerializeField] private ItemActionsSystem itemActionsSystem;

    [Header("EQUIPMENT SYSTEM VARIABLES")]

    [SerializeField] private EquipmentLibrary equipmentLibrary;

    //visuel
    [SerializeField] private Image headSlotImage;
    [SerializeField] private Image shoulderSlotImage;
    [SerializeField] private Image chestSlotImage;
    [SerializeField] private Image handsSlotImage;
    [SerializeField] private Image legsSlotImage;
    [SerializeField] private Image feetSlotImage;

    //élément actuel
    private ItemData equipedHeadItem;
    private ItemData equipedShoulderItem;
    private ItemData equipedChestItem;
    private ItemData equipedHandsItem;
    private ItemData equipedLegsItem;
    private ItemData equipedFeetItem;

    [SerializeField] private Button headSlotDesequipButton;
    [SerializeField] private Button shoulderSlotDesequipButton;
    [SerializeField] private Button chestSlotDesequipButton;
    [SerializeField] private Button handsSlotDesequipButton;
    [SerializeField] private Button legsSlotDesequipButton;
    [SerializeField] private Button feetSlotDesequipButton;

    private void DisablePreviousEquipedEquipment(ItemData itemToDisable)
    {
        if (itemToDisable == null)
        {
            return;
        }

        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemToDisable).First();

        if (equipmentLibraryItem != null)
        {
            for (int i = 0; i < equipmentLibraryItem.elementToDisable.Length; i++)
            {
                equipmentLibraryItem.elementToDisable[i].SetActive(true);
            }

            equipmentLibraryItem.itemPrefab.SetActive(false);
        }

        Inventory.Singleton.AddItem(itemToDisable);
    }

    public void DesequipEquipment(EquipementType equipmentType)
    {
        if (Inventory.Singleton.IsFull())
        {
            Debug.Log("L'inventaire est plein");
            return;
        }

        ItemData currentItem = null;

        switch (equipmentType)
        {
            case EquipementType.Head:
                currentItem = equipedHeadItem;
                equipedHeadItem = null;
                headSlotImage.sprite = Inventory.Singleton.emptySlotVisual;
                break;
            case EquipementType.Shoulders:
                currentItem = equipedShoulderItem;
                equipedShoulderItem = null;
                shoulderSlotImage.sprite = Inventory.Singleton.emptySlotVisual;
                break;
            case EquipementType.Chest:
                currentItem = equipedChestItem;
                equipedChestItem = null;
                chestSlotImage.sprite = Inventory.Singleton.emptySlotVisual;
                break;
            case EquipementType.Hands:
                currentItem = equipedHandsItem;
                equipedHandsItem = null;
                handsSlotImage.sprite = Inventory.Singleton.emptySlotVisual;
                break;
            case EquipementType.Legs:
                currentItem = equipedLegsItem;
                equipedLegsItem = null;
                legsSlotImage.sprite = Inventory.Singleton.emptySlotVisual;
                break;
            case EquipementType.Feets:
                currentItem = equipedFeetItem;
                equipedFeetItem = null;
                handsSlotImage.sprite = Inventory.Singleton.emptySlotVisual;
                break;
        }

        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == currentItem).First();

        if (equipmentLibraryItem != null)
        {
            for (int i = 0; i < equipmentLibraryItem.elementToDisable.Length; i++)
            {
                equipmentLibraryItem.elementToDisable[i].SetActive(true);
            }

            equipmentLibraryItem.itemPrefab.SetActive(false);
        }

        Inventory.Singleton.AddItem(currentItem);
        Inventory.Singleton.RefreshContent();
    }

    public void UpdateEquipmentsDesequipButton()
    {
        headSlotDesequipButton.onClick.RemoveAllListeners();
        headSlotDesequipButton.onClick.AddListener(delegate { DesequipEquipment(EquipementType.Head); });
        headSlotDesequipButton.gameObject.SetActive(equipedHeadItem);
        
        shoulderSlotDesequipButton.onClick.RemoveAllListeners();
        shoulderSlotDesequipButton.onClick.AddListener(delegate { DesequipEquipment(EquipementType.Shoulders); });
        shoulderSlotDesequipButton.gameObject.SetActive(equipedShoulderItem);

        chestSlotDesequipButton.onClick.RemoveAllListeners();
        chestSlotDesequipButton.onClick.AddListener(delegate { DesequipEquipment(EquipementType.Chest); });
        chestSlotDesequipButton.gameObject.SetActive(equipedChestItem);

        handsSlotDesequipButton.onClick.RemoveAllListeners();
        handsSlotDesequipButton.onClick.AddListener(delegate { DesequipEquipment(EquipementType.Hands); });
        handsSlotDesequipButton.gameObject.SetActive(equipedHandsItem);

        legsSlotDesequipButton.onClick.RemoveAllListeners();
        legsSlotDesequipButton.onClick.AddListener(delegate { DesequipEquipment(EquipementType.Legs); });
        legsSlotDesequipButton.gameObject.SetActive(equipedLegsItem);

        feetSlotDesequipButton.onClick.RemoveAllListeners();
        feetSlotDesequipButton.onClick.AddListener(delegate { DesequipEquipment(EquipementType.Feets); });
        feetSlotDesequipButton.gameObject.SetActive(equipedFeetItem);
    }

    public void EquipAction()
    {
        print("Equip item : " + itemActionsSystem.itemCurrentlySelected.name_);

        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemActionsSystem.itemCurrentlySelected).First();

        if (equipmentLibraryItem != null)
        {
            switch (itemActionsSystem.itemCurrentlySelected.equipementType)
            {
                case EquipementType.Head:
                    DisablePreviousEquipedEquipment(equipedHeadItem);
                    headSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedHeadItem = itemActionsSystem.itemCurrentlySelected;
                    break;
                case EquipementType.Shoulders:
                    DisablePreviousEquipedEquipment(equipedShoulderItem);
                    shoulderSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedShoulderItem = itemActionsSystem.itemCurrentlySelected;
                    break;
                case EquipementType.Chest:
                    DisablePreviousEquipedEquipment(equipedChestItem);
                    chestSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedChestItem = itemActionsSystem.itemCurrentlySelected;
                    break;
                case EquipementType.Hands:
                    DisablePreviousEquipedEquipment(equipedHandsItem);
                    handsSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedHandsItem = itemActionsSystem.itemCurrentlySelected;
                    break;
                case EquipementType.Legs:
                    DisablePreviousEquipedEquipment(equipedLegsItem);
                    legsSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedLegsItem = itemActionsSystem.itemCurrentlySelected;
                    break;
                case EquipementType.Feets:
                    DisablePreviousEquipedEquipment(equipedFeetItem);
                    feetSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedFeetItem = itemActionsSystem.itemCurrentlySelected;
                    break;
            }

            for (int i = 0; i < equipmentLibraryItem.elementToDisable.Length; i++)
            {
                equipmentLibraryItem.elementToDisable[i].SetActive(false);
            }

            equipmentLibraryItem.itemPrefab.SetActive(true);

            Inventory.Singleton.RemoveItem(itemActionsSystem.itemCurrentlySelected);
        }
        else
            Debug.LogError("Equipment : " + itemActionsSystem.itemCurrentlySelected.name + "non existant dans la librairie des equipements");

        itemActionsSystem.CloseActionPanel();
    }
}
