using System;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;
    public int itemID;
    public int stackCount = 1;
    public int maxStack = 99;

    private Image _image;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetVisible(bool visible)
    {
        if (_image == null) _image = GetComponent<Image>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_image != null)
        {
            Color c = _image.color;
            c.a = visible ? 1f : 0f;
            _image.color = c;
        }

        if (_spriteRenderer != null)
        {
            Color c = _spriteRenderer.color;
            c.a = visible ? 1f : 0f;
            _spriteRenderer.color = c;
        }
    }

    public Sprite GetSprite()
    {
        if (_image == null) _image = GetComponent<Image>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_image != null) return _image.sprite;
        if (_spriteRenderer != null) return _spriteRenderer.sprite;
        return null;
    }

    public void SetSprite(Sprite sprite)
    {
        if (_image == null) _image = GetComponent<Image>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_image != null) _image.sprite = sprite;
        if (_spriteRenderer != null) _spriteRenderer.sprite = sprite;
    }

    public int AddToStack(int amount)
    {
        int total = stackCount + amount;
        if (total <= maxStack)
        {
            stackCount = total;
            return 0;
        }
        stackCount = maxStack;
        return total - maxStack;
    }

    public int RemoveFromStack(int amount)
    {
        int removed = Mathf.Min(stackCount, amount);
        stackCount -= removed;
        return removed;
    }
}