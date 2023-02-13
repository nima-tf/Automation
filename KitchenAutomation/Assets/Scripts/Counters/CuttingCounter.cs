using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {
    public static event EventHandler OnAnyCut;
    new public static void ResetStaticDate() {
        OnAnyCut = null;
    }
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player) {
        if (!HasKitchenItem()) {
            // There is no KitchenItem here
            if (player.HasKitchenItem()) {
                // PLayer carrying KitchenItem
                if (HasRecipeWithInput(player.GetKitchenItem().GetKitchenItemSO())) {
                    player.GetKitchenItem().SetKitchenItemParent(this);

                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenItem().GetKitchenItemSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
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
                }
            } else {
                // PLayer is not carrying anything
                GetKitchenItem().SetKitchenItemParent(player);
            }

        }
        
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenItem() && HasRecipeWithInput(GetKitchenItem().GetKitchenItemSO())) {
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenItem().GetKitchenItemSO());
            
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
                KitchenItemSO outputKitchenItemSO = GetOutputForInput(GetKitchenItem().GetKitchenItemSO());
                GetKitchenItem().DestroySelf();
                KitchenItem.SpawnKitchenItem(outputKitchenItemSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenItemSO inputKitchenItemSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenItemSO);
        return cuttingRecipeSO != null;
    }

    private KitchenItemSO GetOutputForInput(KitchenItemSO inputKitchenItemSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenItemSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        } else {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenItemSO inputKitchenItemSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenItemSO) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
