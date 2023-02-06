using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenItemParent {

    [SerializeField] private KitchenItemSO kitchenItemSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenItem kitchenItem;

    public void Interact(Player player) {
        if (kitchenItem == null) {
        Transform kitchenItemTransform = Instantiate(this.kitchenItemSO.prefab, counterTopPoint);
        kitchenItemTransform.GetComponent<KitchenItem>().SetKitchenItemParent(this);
        } else {
            kitchenItem.SetKitchenItemParent(player);
        }
    }

    public Transform GetKitchenItemFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenItem(KitchenItem kitchenItem) {
        this.kitchenItem = kitchenItem;
    }
    
    public KitchenItem GetKitchenItem() {
        return kitchenItem;
    }

    public void ClearKitchenItem() {
        kitchenItem = null;
    }

    public bool HasKitchenItem() {
        return this.kitchenItem != null;
    }
}
