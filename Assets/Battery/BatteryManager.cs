using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float totalMWh = 0;
    public BatteryRecepticle[] recepticles = new BatteryRecepticle[2];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float total = 0;
        for(int i = 0; i<recepticles.Length; i++) {
            total += recepticles[i].MWh;
        }
        totalMWh = total;
    }

    public int draw(float MW) {
        int recepticlesWithPower = recepticles.Length;
        
        for(int i = 0; i<recepticles.Length; i++) {
            if(recepticles[i].MWh == 0) {
                recepticlesWithPower -= 1;
            }
        }

        
        if(recepticlesWithPower == 0) {
            return 1;
        }


        for(int i = 0; i<recepticles.Length; i++) {
            if(recepticles[i].MWh == 0) {
                recepticles[i].drawPower(MW/recepticlesWithPower);
            }
        }

        return 0;

    }
}
