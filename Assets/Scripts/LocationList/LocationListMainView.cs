using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LocationListMainView : BaseMainView
{
    public static Dictionary<POI, bool> visistedPOIs;

    [SerializeField]
    VisualTreeAsset m_ListEntryTemplate;

    static LocationListMainView()
    {
        visistedPOIs = new Dictionary<POI, bool>();
    }

    void Update()
    {
        base.Update();

        // Open/Close UI
        if(Input.GetKeyDown("e"))
        {
            if(!PauseMenu.GameIsPaused)
            {
                OpenUI();
            }
            else if(uiDocument.enabled)
            {
                CloseUI();
            }
        }
    }

    public override void OpenUI()
    {
        base.OpenUI();

        // Initialize the locations list
        var locationListController = new LocationListController();
        locationListController.InitializeLocationList(
            uiDocument.rootVisualElement,
            m_ListEntryTemplate);
    }

    public static void InitVisistedLocations(List<POI> pois)
    {
        visistedPOIs.Clear();
        foreach (var poi in pois)
        {
            visistedPOIs.Add(poi, false);
        }
    }

    public static float VisistedPercent()
    {
        int amount = 0;
        foreach (bool value in visistedPOIs.Values)
        {
            if(value)
            {
                amount++;
            }
        }

        return (float)amount / visistedPOIs.Count;
    }
}