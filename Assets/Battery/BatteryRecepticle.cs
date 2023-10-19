using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryRecepticle : Interactable
{
    public GameObject currentBattery = null;
    public float MWh = 0f;

    void FixedUpdate() {
        if(MWh <= 0f) {
            MWh = 0f;
            currentBattery.GetComponent<Battery>().MWh = 0f;
        }
        currentBattery.GetComponent<Battery>().updateTexture();
        transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = string.Format("{0:N0}MWh", MWh);
    }

    public bool hasPower() {
        return MWh != 0;
    }

    public override void interact(GameObject go) {

        GameObject battery = null;
        GameObject oldBattery = null;
        try {
            go.GetComponent<Inventory>().inventory[go.GetComponent<Inventory>().selectIndex].GetComponent<Battery>();
            battery = go.GetComponent<Inventory>().requestSelectedItem();
        } catch {
            if(currentBattery != null) {
                currentBattery.GetComponent<Battery>().MWh = MWh;
                go.GetComponent<Inventory>().pickup(currentBattery);
                MWh = 0;
                currentBattery = null;
            }
            return;
        }

        if(currentBattery != null) {
            oldBattery = currentBattery;
            oldBattery.GetComponent<Battery>().MWh = MWh;
            go.GetComponent<Inventory>().pickup(oldBattery);
        }

        currentBattery = battery;
        currentBattery.transform.SetParent(transform);
        currentBattery.SetActive(true);
        currentBattery.transform.localPosition = new Vector3(0f, -0.4f, 0f);

        MWh = currentBattery.GetComponent<Battery>().MWh;
    }

    public int drawPower(float power) {
        if(currentBattery == null) {
            return 1;
        }

        if(power <= 0) {
            return 1;
        }

        MWh -= power;
        return 0;
    }
}
