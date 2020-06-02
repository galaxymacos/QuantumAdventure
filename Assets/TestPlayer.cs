using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<LaunchComponent>().Launch(new Vector3(0,1,0),100 );
    }

    // Update is called once per frame
    void Update()
    {
    }
}
