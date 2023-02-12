using UnityEngine;

public class PlateIconsUI : MonoBehaviour {
    [SerializeField] private PlateKitchenItem plateKitchenItem;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        plateKitchenItem.OnIngredientAdded += PlateKitchenItem_OnIngredientAdded;
    }

    private void PlateKitchenItem_OnIngredientAdded(object sender, PlateKitchenItem.OnIngredientAddedEventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in transform) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenItemSO kitchenItemSO in plateKitchenItem.GetKitchenItemSOList()) {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenItemSO(kitchenItemSO);
        }
    }
}
