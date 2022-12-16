/* 
`````````````````````````````````````
AudioManager.cs

Play menu music during game.

`````````````````````````````````````
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static GameObject previousAudio;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(previousAudio)
        {
            Destroy(previousAudio);
        }
        previousAudio = gameObject;
    }
}
