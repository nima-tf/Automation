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

    public void DestroySelf() {
        kitchenItemParent.ClearKitchenItem();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenItem plateKitchenItem) {
        if (this is PlateKitchenItem) {
            plateKitchenItem = this as PlateKitchenItem;
            return true;
        } else {
            plateKitchenItem = null;
            return false;
        }
    }

    public static KitchenItem SpawnKitchenItem(KitchenItemSO kitchenItemSO, IKitchenItemParent kitchenItemParent) {
            Transform kitchenItemTransform = Instantiate(kitchenItemSO.prefab);
            KitchenItem kitchenItem = kitchenItemTransform.GetComponent<KitchenItem>();
            kitchenItem.SetKitchenItemParent(kitchenItemParent);
            
            return kitchenItem;
    }
}
