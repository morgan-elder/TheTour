using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

class SiteInfoController
{
    // UI element references
    VisualElement m_SiteImage;
    Label m_SiteTitle;
    Label m_SiteDescription;
    Button m_CloseButton;

    public void InitializeSiteInfo(
        VisualElement root,
        POI poi,
        Action closeMenu)
    {
        m_SiteImage = root.Q<VisualElement>("SiteImage");
        m_SiteTitle = root.Q<Label>("SiteTitle");
        m_SiteDescription = root.Q<Label>("SiteDescription");
        m_CloseButton = root.Q<Button>("CloseButton");

        m_CloseButton.clicked += closeMenu;
        if(poi == null)
        {
            return;
        }
        SetSiteData(poi);
    }

    private void SetSiteData(POI poi)
    {
        m_SiteTitle.text = poi.name;
        m_SiteDescription.text = poi.description;
        m_SiteImage.style.backgroundImage = poi.image;
    }
}
