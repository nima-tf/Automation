using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour {
    [SerializeField] private Image image;

    public void SetKitchenItemSO(KitchenItemSO kitchenItemSO) {
        image.sprite = kitchenItemSO.iconSprite;
    }
}
