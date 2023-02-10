using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenItemParent {
    [SerializeField] private Transform counterTopPoint;

    private KitchenItem kitchenItem;
    
    public abstract void Interact(Player player);

    public abstract void InteractAlternate(Player player);

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