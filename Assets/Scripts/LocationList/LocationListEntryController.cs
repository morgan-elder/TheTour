using UnityEngine.UIElements;

public class LocationListEntryController
{
    Label m_LabelName;

    public void SetVisualElement(VisualElement visualElement)
    {
        m_LabelName = visualElement.Q<Label>("LocationName");
    }

    public void SetLocationData(POI poi)
    {
        m_LabelName.text = poi.name;
    }
}