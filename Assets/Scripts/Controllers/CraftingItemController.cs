using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingItemController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image craftImage;
    [SerializeField] private List<GameObject> neededs;
    [SerializeField] private TextIdiom textIdiom;

    private Color haveEnoughtItems;
    private Color haventEnoughtItems;

    private Recipe recipe;

    private void Awake()
    {

        haveEnoughtItems = new Color(0.5f, 1f, 0.5f, 0.5f);
        haventEnoughtItems = new Color(1f, 0.5f, 0.5f, 0.5f);
    }

    private void Start()
    {

        craftImage.sprite = recipe.craftingObj.icon;
        textIdiom.SetStringKey(recipe.craftingObj.stringKey);
        for (int i = 0; i < recipe.needed.Count; i++)
        {
            neededs[i].transform.GetChild(0).GetComponent<Image>().sprite = recipe.needed[i].itemNeeded.icon;
            neededs[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = recipe.needed[i].amount.ToString();
            neededs[i].GetComponent<NeededItemController>().SetItem(recipe.needed[i].itemNeeded);
            neededs[i].SetActive(true);
        }
    }

    private void OnEnable()
    {
        UpdateCraftingItemController();
    }

    public void UpdateCraftingItemController()
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (CraftingManager.Instance.GetCanCraft(recipe))
                gameObject.GetComponent<Image>().color = haveEnoughtItems;
            else
                gameObject.GetComponent<Image>().color = haventEnoughtItems;
        }
    }

    public void Click()
    {

        CraftingManager.Instance.TryCraft(recipe);
    }

    public void SetRecipe(Recipe r)
    {
        recipe = r;
    }

    public Recipe GetRecipe()
    {
        return recipe;
    }

    public void DestroyRecipe()
    {
        textIdiom.RemoveFromList();
        Destroy(this.gameObject);
    }
}
