using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortalIDFetcher : MonoBehaviour
{

    public PortalController portalController;
    public int IDToFetch;
    public string pretext;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(portalController.portalHashes[IDToFetch] != "") {
            gameObject.GetComponent<TMP_Text>().text = pretext + portalController.portalHashes[IDToFetch];
        }        
    }
}
