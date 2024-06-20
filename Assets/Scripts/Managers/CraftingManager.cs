using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftingManager : MonoBehaviour
{

    #region Instance

    private static CraftingManager _instance;
    public static CraftingManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CraftingManager>();
            }
            return _instance;
        }
    }

    #endregion

    [SerializeField] private List<Recipe> listOfRecipes;
    [SerializeField] private GameObject recipesPrefab;
    [SerializeField] private Transform recipesContent;

    [Header("EVENTS")]
    [SerializeField] private UnityEvent onCraft;

    private List<GameObject> recipes = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < listOfRecipes.Count; i++)
        {
            GameObject recipe = GameObject.Instantiate(recipesPrefab, recipesContent);
            recipe.GetComponent<CraftingItemController>().SetRecipe(listOfRecipes[i]);
            recipes.Add(recipe);
        }
    }

    public void TryCraft(Recipe recipe)
    {
        if (GetCanCraft(recipe))
            Craft(recipe);
    }

    public void Craft(Recipe recipe)
    {
        for (int i = 0; i < recipe.needed.Count; i++)
        {
            InventoryManager.Instance.RemoveAmountOfItem(recipe.needed[i].itemNeeded, recipe.needed[i].amount);
        }

        if (recipe.craftingObj.itemType != Item.ItemType.Weapon)
        {
            InventoryManager.Instance.AddAmountOfItem(recipe.craftingObj, recipe.value);
        }
        else
        {
            EquipmentManager.Instance.EnableEquip(recipe.craftingObj);
            RemoveRecipe(recipe);
        }
        NotificationsManager.Instance.ShowNotification(recipe.craftingObj.icon, "NOTIFICATION02");

        onCraft.Invoke();

        UpdateRecipes();
    }

    public bool GetCanCraft(Recipe recipe)
    {
        bool canCraft = true;
        for (int i = 0; i < recipe.needed.Count; i++)
        {
            if (InventoryManager.Instance.ReturnAmountOfItem(recipe.needed[i].itemNeeded.name) < recipe.needed[i].amount)
                canCraft = false;
        }
        return canCraft;
    }

    public void UpdateRecipes()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            recipes[i].GetComponent<CraftingItemController>().UpdateCraftingItemController();
        }
    }

    public void AddRecipe(Recipe recipe) {

        listOfRecipes.Add(recipe);

        GameObject newRecipe = GameObject.Instantiate(recipesPrefab, recipesContent);
        newRecipe.GetComponent<CraftingItemController>().SetRecipe(recipe);
        recipes.Add(newRecipe);

        NotificationsManager.Instance.ShowNotification(recipe.craftingObj.icon, "NOTIFICATION01");
    }

    public void RemoveRecipe(Recipe recipe)
    {
        bool found = false;

        for (int i = 0; i < recipes.Count && !found; i++)
        {
            if(recipes[i].GetComponent<CraftingItemController>().GetRecipe() == recipe)
            {
                recipes[i].GetComponent<CraftingItemController>().DestroyRecipe();
                recipes.Remove(recipes[i]);
                found = true;
            }
        }
    }
}
