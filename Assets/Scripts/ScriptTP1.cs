using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTP1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(63, 20, 500);
    }

}
