using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LocationListController
{

    public static List<POI> allPOIs { set; private get; }
    VisualTreeAsset m_ListEntryTemplate;

    // UI element references
    ListView m_LocationList;
    Label m_CompletePercent;
    Label m_LocLabelName;
    Label m_LocLabelPosition;

    public void InitializeLocationList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        m_ListEntryTemplate = listElementTemplate;

        m_LocationList = root.Q<ListView>("LocationList");
        m_CompletePercent = root.Q<Label>("CompletePercent");
        m_LocLabelName = root.Q<Label>("LocationName");
        m_LocLabelPosition = root.Q<Label>("Position");

        FillList();
    }

    private void FillList()
    {
        string vistedString = (100.0 * LocationListMainView.VisistedPercent()).ToString("0.00");
        m_CompletePercent.text = $"{vistedString}% Complete";

        m_LocationList.makeItem = () =>
        {
            var newEntry = m_ListEntryTemplate.Instantiate();
            var newController = new LocationListEntryController();

            newEntry.userData = newController;
            newController.SetVisualElement(newEntry);

            return newEntry;
        };

        m_LocationList.bindItem = (item, index) =>
        {
            var poi = allPOIs[index];

            (item.userData as LocationListEntryController).
                SetLocationData(poi);

            item.AddToClassList(
                LocationListMainView.visistedPOIs[poi] ?
                "loc-done" : "loc-new"
            );
        };

        m_LocationList.fixedItemHeight = 32;
        m_LocationList.itemsSource = allPOIs;
    }
}
