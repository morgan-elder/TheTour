using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisibility : MonoBehaviour
{
    [SerializeField] GameObject _BingMapsAddon;
    public MeshRenderer _OGMapTexture;
    public MeshRenderer _OGMapBuilding;
    public bool rendStatus;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frameun
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.F))
        {
            rendStatus = _BingMapsAddon.activeSelf;
            Debug.Log(rendStatus);
            if (rendStatus == true)
            {
                _BingMapsAddon.SetActive(false);
                _OGMapTexture.enabled = true;
                _OGMapBuilding.enabled = true;
                
            }
            else if (rendStatus == false)
            {
                _BingMapsAddon.SetActive(true);
                _OGMapTexture.enabled = false;
                _OGMapBuilding.enabled = false;
            }

        }
        //if (Input.GetKeyUp(KeyCode.G))
        //{
 
        //        _BingMapsAddon.SetActive(true);
        //        _OGMapTexture.enabled = false;
        //        _OGMapBuilding.enabled = false;
            

        //}
    }
}
