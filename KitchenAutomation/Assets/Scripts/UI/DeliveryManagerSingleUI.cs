using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DeliveryManagerSingleUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecupeSO(RecipeSO recipeSO) {
        recipeNameText.text = recipeSO.recipeName;

        // clean up of the UI template
        foreach (Transform child in iconContainer) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenItemSO kitchenItemSO in recipeSO.kitchenItemSOList) {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenItemSO.iconSprite;
        }
    }
}
