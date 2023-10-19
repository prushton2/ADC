using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] inventory = new GameObject[8];
    public int stackIndex = 0;
    public int selectIndex = 0;

    public RaycastHit hit;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("g")) {
            inventory[selectIndex].transform.localPosition = new Vector3(0, -1.25f, 0) - transform.forward;
            inventory[selectIndex].SetActive(true);
            inventory[selectIndex].transform.SetParent(null);
            inventory[selectIndex] = null;
        }

    }

    public int pickup(GameObject go) {
        if(stackIndex >= 9) {
            return 1;
        }

        for(int i = 0; i<inventory.Length; i++) {
            if(inventory[i] == null) {
                inventory[i] = go;
                go.transform.SetParent(transform);
                go.SetActive(false);
                return 0;
            }
        }
        return 1;
    }
    
    public GameObject requestSelectedItem() {
        GameObject invitem = inventory[selectIndex];
        inventory[selectIndex] = null;
        return invitem;
    }
}
