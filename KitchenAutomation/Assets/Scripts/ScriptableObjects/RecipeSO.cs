using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject {
    public List<KitchenItemSO> kitchenItemSOList;
    public string recipeName;
}
