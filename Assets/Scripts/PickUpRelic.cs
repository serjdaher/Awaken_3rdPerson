using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRelic : MonoBehaviour
{
    public MonoBehaviour script;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<PlayerControl>().enabled = false;
        }
    }
}
