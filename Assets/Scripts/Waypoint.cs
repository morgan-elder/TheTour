using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    // The maximum distance that the waypoint
    // will still be visible from, in meters.
    const float MAX_DISTANCE = 500;

    public bool hidden = false;
    public RectTransform prefab;
    private RectTransform waypoint;
    private Transform player;
    private Text distanceText;


    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.Find("Waypoints").transform;

        waypoint = Instantiate(prefab, canvas);
        distanceText = waypoint.GetComponentInChildren<Text>();

        player = GameObject.Find("First Person Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(hidden)
        {
            waypoint.gameObject.SetActive(false);
            return;
        }

        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        var dist = Vector3.Distance(player.position, transform.position);
        waypoint.position = screenPos;

        waypoint.gameObject.SetActive(dist <= MAX_DISTANCE && screenPos.z > 0);

        distanceText.text = dist.ToString("0.") + " m";
    }
}
