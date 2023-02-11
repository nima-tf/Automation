using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {
    [Serializable]
    public struct KitchenItemSO_GameObject {
        public KitchenItemSO kitchenItemSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenItem plateKitchenItem;
    [SerializeField] private List<KitchenItemSO_GameObject> kitchenItemSOGameObjectList;

    private void Start() {
         plateKitchenItem.OnIngredientAdded += PlateKitchenItem_OnIngredientAdded;

         foreach (KitchenItemSO_GameObject kitchenItemSOGameObject in kitchenItemSOGameObjectList) {
            kitchenItemSOGameObject.gameObject.SetActive(false);
         }
    }

    private void PlateKitchenItem_OnIngredientAdded(object sender, PlateKitchenItem.OnIngredientAddedEventArgs e) {
        foreach (KitchenItemSO_GameObject kitchenItemSOGameObject in kitchenItemSOGameObjectList) {
            if (kitchenItemSOGameObject.kitchenItemSO == e.kitchenItemSO) {
                kitchenItemSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
