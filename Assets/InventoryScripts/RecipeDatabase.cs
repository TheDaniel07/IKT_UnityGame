using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
{
    public string itemId;
    public int amount;
}

public class SmeltingIngredient
{
    public string[] itemId = {"coal_smelted", "wood_log", "wood_stick", "wood_plank"};
}

[System.Serializable]
public class Recipe
{
    public string recipeId;
    public string outputItemId;
    public int outputAmount;
    public List<Ingredient> ingredients = new();
}

public static class RecipeDatabase
{
    public static readonly List<Recipe> CraftingRecipes = new()
    {
        new Recipe
        {
            recipeId = "craft_wood_plank",
            outputItemId = "wood_plank",
            outputAmount = 4,
            ingredients = new() { new Ingredient { itemId = "wood_log", amount = 1 } }
        },
        new Recipe
        {
            recipeId = "craft_wood_stick",
            outputItemId = "wood_stick",
            outputAmount = 4,
            ingredients = new() { new Ingredient { itemId = "wood_plank", amount = 2 } }
        },
        new Recipe
        {
            recipeId = "craft_wood_sword",
            outputItemId = "sword_wooden",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "wood_plank", amount = 2 }
            }
        },
        new Recipe
        {
            recipeId = "craft_wood_pickaxe",
            outputItemId = "pickaxe_wooden",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "wood_plank", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_wood_hammer",
            outputItemId = "hammer_wooden",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "wood_plank", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_wood_axe",
            outputItemId = "axe_wooden",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "wood_plank", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_stone_sword",
            outputItemId = "sword_stone",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "stone", amount = 2 }
            }
        },
        new Recipe
        {
            recipeId = "craft_stone_pickaxe",
            outputItemId = "pickaxe_stone",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "stone", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_stone_hammer",
            outputItemId = "hammer_stone",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "stone", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_stone_axe",
            outputItemId = "axe_stone",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "stone", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_copper_sword",
            outputItemId = "sword_copper",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "copper_smelted", amount = 2 }
            }
        },
        new Recipe
        {
            recipeId = "craft_copper_pickaxe",
            outputItemId = "pickaxe_copper",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "copper_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_copper_hammer",
            outputItemId = "hammer_copper",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "copper_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_copper_axe",
            outputItemId = "axe_copper",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "copper_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_iron_sword",
            outputItemId = "sword_iron",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "iron_smelted", amount = 2 }
            }
        },
        new Recipe
        {
            recipeId = "craft_iron_pickaxe",
            outputItemId = "pickaxe_iron",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "iron_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_iron_hammer",
            outputItemId = "hammer_iron",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "iron_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_iron_axe",
            outputItemId = "axe_iron",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "iron_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_diamond_sword",
            outputItemId = "sword_diamond",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "diamond_smelted", amount = 2 }
            }
        },
        new Recipe
        {
            recipeId = "craft_diamond_pickaxe",
            outputItemId = "pickaxe_diamond",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "diamond_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_diamond_hammer",
            outputItemId = "hammer_diamond",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "diamond_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_diamond_axe",
            outputItemId = "axe_diamond",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "diamond_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_titan_sword",
            outputItemId = "sword_titan",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "titan_smelted", amount = 2 }
            }
        },
        new Recipe
        {
            recipeId = "craft_titan_pickaxe",
            outputItemId = "pickaxe_titan",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "titan_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_titan_hammer",
            outputItemId = "hammer_titan",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "titan_smelted", amount = 3 }
            }
        },
        new Recipe
        {
            recipeId = "craft_titan_axe",
            outputItemId = "axe_titan",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "wood_stick", amount = 2 },
                new Ingredient { itemId = "titan_smelted", amount = 3 }
            }
        },
    };

    public static readonly List<Recipe> SmeltingRecipes = new()
    {
        new Recipe
        {
            recipeId = "smelt_copper",
            outputItemId = "copper_smelted",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "copper_ore", amount = 1 },
                new Ingredient { itemId = "coal_smelted", amount = 1 }
            }
        },
        new Recipe
        {
            recipeId = "smelt_iron",
            outputItemId = "iron_smelted",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "iron_ore", amount = 1 },
                new Ingredient { itemId = "coal_smelted", amount = 1 }
            }
        },
        new Recipe
        {
            recipeId = "smelt_diamond",
            outputItemId = "diamond_smelted",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "diamond_ore", amount = 1 },
                new Ingredient { itemId = "coal_smelted", amount = 1 }
            }
        },
        new Recipe
        {
            recipeId = "smelt_gold",
            outputItemId = "gold_smelted",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "gold_ore", amount = 1 },
                new Ingredient { itemId = "coal_smelted", amount = 1 }
            }
        },
        new Recipe
        {
            recipeId = "smelt_titanium",
            outputItemId = "titanium_smelted",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "titanium_ore", amount = 1 },
                new Ingredient { itemId = "coal_smelted", amount = 2 }
            }
        },
        new Recipe
        {
            recipeId = "smelt_ruby",
            outputItemId = "ruby_smelted",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "ruby_ore", amount = 1 },
                new Ingredient { itemId = "coal_smelted", amount = 1 }
            }
        },
        new Recipe
        {
            recipeId = "smelt_uranium",
            outputItemId = "uranium_smelted",
            outputAmount = 1,
            ingredients = new()
            {
                new Ingredient { itemId = "uranium_ore", amount = 1 },
                new Ingredient { itemId = "coal_smelted", amount = 3 }
            }
        },
    };

    public static bool CanCraft(Recipe recipe)
    {
        foreach (var ing in recipe.ingredients)
            if (InventoryManager.Instance.GetQuantity(ing.itemId) < ing.amount)
                return false;
        return true;
    }

    public static void ExecuteRecipe(Recipe recipe)
    {
        if (!CanCraft(recipe))
        {
            Debug.LogWarning($"[RecipeDatabase] Cannot execute recipe: {recipe.recipeId}");
            return;
        }
        foreach (var ing in recipe.ingredients)
            InventoryManager.Instance.RemoveItem(ing.itemId, ing.amount);
        InventoryManager.Instance.AddItem(recipe.outputItemId, recipe.outputAmount);
        Debug.Log($"[RecipeDatabase] Crafted: {recipe.outputItemId} x{recipe.outputAmount}");
    }
}