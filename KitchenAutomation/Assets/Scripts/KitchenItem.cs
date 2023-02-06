using UnityEngine;

public class KitchenItem : MonoBehaviour {
    [SerializeField] private KitchenItemSO kitchenItemSO;

    private IKitchenItemParent kitchenItemParent;

    public KitchenItemSO GetKitchenItemSO() {
        return kitchenItemSO;
    }

    public void SetKitchenItemParent(IKitchenItemParent kitchenItemParent) {
        if (this.kitchenItemParent != null) {
            this.kitchenItemParent.ClearKitchenItem();
        }

        this.kitchenItemParent = kitchenItemParent;

        if (kitchenItemParent.HasKitchenItem()) {
            Debug.LogError("KitchenItemParent already has a KitchenItem");
        }

        kitchenItemParent.SetKitchenItem(this);

        transform.parent = kitchenItemParent.GetKitchenItemFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenItemParent GetKitchenItemParent() {
        return kitchenItemParent;
    }
}
