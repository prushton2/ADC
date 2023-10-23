using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executable : MonoBehaviour
{
    // Start is called before the first frame update
    
    public string exeName;
    public string state;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual string execute(string[] args) {
        return "";
    }

    public virtual Vector3 getPos() {
        return transform.position;
    }
}
