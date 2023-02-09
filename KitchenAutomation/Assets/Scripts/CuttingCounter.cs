using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player) {
        if (!HasKitchenItem()) {
            // There is no KitchenItem here
            if (player.HasKitchenItem()) {
                // PLayer carrying KitchenItem
                if (HasRecipeWithInput(player.GetKitchenItem().GetKitchenItemSO())) {
                    player.GetKitchenItem().SetKitchenItemParent(this);
                }
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
        if (HasKitchenItem() && HasRecipeWithInput(GetKitchenItem().GetKitchenItemSO())) {
            KitchenItemSO outputKitchenItemSO = GetOutputForInput(GetKitchenItem().GetKitchenItemSO());
            GetKitchenItem().DestroySelf();
            KitchenItem.SpawnKitchenItem(outputKitchenItemSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenItemSO inputKitchenItemSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenItemSO) {
                return true;
            }
        }
        return false;
    }

    private KitchenItemSO GetOutputForInput(KitchenItemSO inputKitchenItemSo) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenItemSo) {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
