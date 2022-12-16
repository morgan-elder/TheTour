using System;
using UnityEngine;
using UnityEngine.UIElements;

class SiteInfoMainView : BaseMainView
{
    private POI poi;
    private Camera cam;

    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
    }

    public override void OpenUI()
    {
        base.OpenUI();
        var uiController = new SiteInfoController();
        uiController.InitializeSiteInfo(
            uiDocument.rootVisualElement,
            poi,
            CloseUI);
    }

    void FixedUpdate()
    {
        // == Check for POI zones ==

        // Check if left mouse button is pressed
        if(!Input.GetMouseButton(0) || PauseMenu.GameIsPaused)
        {
            return;
        }

        // Detect collision zones
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit) // If a collision happens
           || hit.distance > 20.0f // and if within 20 meters
           || !hit.collider.gameObject.TryGetComponent(out SiteCollisonZone zone))
              // and if it has a SiteCollisionZone component
        {
            return;
        }

        // If found, open the site info window
        poi = zone.poiData;
        OpenUI();

        // Mark as visited
        LocationListMainView.visistedPOIs[poi] = true;
        var waypoint = zone.gameObject.GetComponent<Waypoint>() as Waypoint;
        waypoint.hidden = true;
    }
}