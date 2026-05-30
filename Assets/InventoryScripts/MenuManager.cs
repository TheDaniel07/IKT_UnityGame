using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private MonoBehaviour _currentOpen;

    public void RequestOpen(MonoBehaviour requester)
    {
        if (_currentOpen != null && _currentOpen != requester)
        {
            if (_currentOpen is InventoryMenuManager inv) inv.CloseMenu();
            if (_currentOpen is CraftingMenuManager cra) cra.CloseMenu();
            if (_currentOpen is SmeltingMenuManager sme) sme.CloseMenu();
        }
        _currentOpen = requester;
    }

    public void NotifyClosed(MonoBehaviour requester)
    {
        if (_currentOpen == requester)
            _currentOpen = null;
    }
}