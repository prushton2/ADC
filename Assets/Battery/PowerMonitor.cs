using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerMonitor : MonoBehaviour
{
    // Start is called before the first frame update

    public BatteryManager bm;
    public string pretext;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<TMP_Text>().text = pretext + "\n" + (int)bm.totalMWh + "MWh";
    }
}