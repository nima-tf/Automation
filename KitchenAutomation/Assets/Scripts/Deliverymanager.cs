using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {
    public static DeliveryManager Instance {get; private set;}

    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake() {
        Instance = this;
        
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                Debug.Log(waitingRecipeSO.recipeName);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenItem plateKitchenItem) {
        for (int i=0; i<waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenItemSOList.Count == plateKitchenItem.GetKitchenItemSOList().Count) {
                // plate has the same number of the ingredients as the recipe
                bool plateCountentsMatchRecipe = true;
                foreach (KitchenItemSO recipeKitchenItemSO in waitingRecipeSO.kitchenItemSOList) {
                    bool ingredientFound = false;
                    foreach (KitchenItemSO plateKitchenItemSO in plateKitchenItem.GetKitchenItemSOList()) {
                        if (plateKitchenItemSO == recipeKitchenItemSO) {
                            // ingredient matches
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // ingredient not found on the plate
                        plateCountentsMatchRecipe = false;
                    }
                }
                if (plateCountentsMatchRecipe) {
                    // correct recipe delivered
                    Debug.Log("Correcnt recipe delivered!");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        // no match found - correct recipe not delivered
        Debug.Log("Incorrecnt recipe delivered!");
    }
}
