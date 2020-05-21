using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("There are "+GameObject.FindGameObjectsWithTag("Player").Length+" game object with the tag Player");
    }
}
