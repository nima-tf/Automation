using System;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenItemParent {
    public static event EventHandler OnAnyItemPlaced;

    [SerializeField] private Transform counterTopPoint;

    private KitchenItem kitchenItem;
    
    public abstract void Interact(Player player);

    public virtual void InteractAlternate(Player player) {
        Debug.Log("BaseCounter.InteractAlternate();");
    }

    public Transform GetKitchenItemFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenItem(KitchenItem kitchenItem) {
        this.kitchenItem = kitchenItem;

        if (kitchenItem != null) {
            OnAnyItemPlaced?.Invoke(this, EventArgs.Empty);
        }
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