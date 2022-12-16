using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCSV : MonoBehaviour
{
    public RectTransform m_WaypointPrefab;
    public Material m_CylinderMaterial;
    public TextAsset poi_data;
    List<POI> pois = new List<POI>();
    // Start is called before the first frame update
    void Start()
    {

        LatLon MapCenter = GetComponent<LatLon>();
       // TextAsset poi_data = Resources.Load<TextAsset>("csv/test");

        string[] data = poi_data.text.Split(new char[] { '\n' });
        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { '|' });
            pois.Add(new POI(row, MapCenter));
        }

        foreach (POI p in pois)
        {
            // GameObject
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.SetParent(this.gameObject.transform);
            cylinder.transform.position = new Vector3(p.point.x, 20f, p.point.y);
            cylinder.transform.localScale = new Vector3(0.5f, 1f, .5f);
            //Debug.Log($"Added POI at: ({p.point.x}, {p.point.y})");

            // POI data
            var zone = cylinder.AddComponent(typeof(SiteCollisonZone)) as SiteCollisonZone;
            zone.poiData = p;

            // Material
            var renderer = cylinder.GetComponent<MeshRenderer>();
            renderer.material = m_CylinderMaterial;

            // Waypoint
            var waypoint = cylinder.AddComponent(typeof(Waypoint)) as Waypoint;
            waypoint.prefab = m_WaypointPrefab;
        }

        LocationListController.allPOIs = pois;
        LocationListMainView.InitVisistedLocations(pois);
    }
}
