using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenItem : KitchenItem {
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenItemSO kitchenItemSO;
    }

    [SerializeField] private List<KitchenItemSO> validKitchenItemList;

    private List<KitchenItemSO> kitchenItemSOList;

    private void Awake() {
        kitchenItemSOList = new List<KitchenItemSO>();
    }

    public bool TryAddIngredient(KitchenItemSO kitchenItemSO) {
        if (!validKitchenItemList.Contains(kitchenItemSO)) {
            // not a valid ingredient
            return false;
        }
        if (kitchenItemSOList.Contains(kitchenItemSO)) {
            // already has this type
            return false;
        } else {
            kitchenItemSOList.Add(kitchenItemSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                kitchenItemSO = kitchenItemSO
            });
            return true;
        }
    }

    public List<KitchenItemSO> GetKitchenItemSOList() {
        return kitchenItemSOList;
    }
}
