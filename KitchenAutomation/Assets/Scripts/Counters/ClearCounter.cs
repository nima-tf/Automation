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
            // There is a KitchenItem here
            if (player.HasKitchenItem()) {
                // Player is carrying something
                if (player.GetKitchenItem().TryGetPlate(out PlateKitchenItem plateKitchenItem)) {
                    // player is holding a plate
                    if (plateKitchenItem.TryAddIngredient(GetKitchenItem().GetKitchenItemSO())) {
                        GetKitchenItem().DestroySelf();
                    }
                } else {
                    // player is holding something else
                    if (GetKitchenItem().TryGetPlate(out plateKitchenItem)) {
                        // counter has a plate
                        if (plateKitchenItem.TryAddIngredient(player.GetKitchenItem().GetKitchenItemSO())) {
                            player.GetKitchenItem().DestroySelf();
                        }
                    }
                }
            } else {
                // PLayer is not carrying anything
                GetKitchenItem().SetKitchenItemParent(player);
            }

        }
        
    }
}
