using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenItemSO kitchenItemSO;

    public override void Interact(Player player) {
        if (!HasKitchenItem()) {
            // There is no KitchenItem here
            if (player.HasKitchenItem()) {
                // PLayer carrying KitchenItem
                player.GetKitchenItem().SetKitchenItemParent(this);
            } else {
                // Player has nothing
            }
        } else {
            // There is no KitchenItem here
            if (player.HasKitchenItem()) {
                // Player is carrying something
            } else {
                // PLayer is not carrying anything
                GetKitchenItem().SetKitchenItemParent(player);
            }

        }
        
    }

    public override void InteractAlternate(Player player)
    {
        Debug.Log("Cannot cut here!");
    }
}
