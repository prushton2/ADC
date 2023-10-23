using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update

    public Player player;

    public GameObject[] inventory = new GameObject[8];
    public GameObject hotbar;
    public int selectIndex = 0;

    public RaycastHit hit;

    public GameObject selectionBox;

    void Start()
    {
        selectionBox = hotbar.transform.GetChild(0).GetChild(0).gameObject;
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.movementLocked) {
            return;
        }

        if(Input.GetKeyDown("g")) {
            inventory[selectIndex].transform.localPosition = new Vector3(0, -1.25f, 0) - transform.forward;
            inventory[selectIndex].SetActive(true);
            inventory[selectIndex].transform.SetParent(null);
            inventory[selectIndex] = null;
            updateInventoryText();
        }
        
        if(Input.GetKeyDown("1")) {
            selectSlot(0);
        }

        if(Input.GetKeyDown("2")) {
            selectSlot(1);
        }

        if(Input.GetKeyDown("3")) {
            selectSlot(2);
        }

        if(Input.GetKeyDown("4")) {
            selectSlot(3);
        }

        if(Input.GetKeyDown("5")) {
            selectSlot(4);
        }

        if(Input.GetKeyDown("6")) {
            selectSlot(5);
        }

        if(Input.GetKeyDown("7")) {
            selectSlot(6);
        }

        if(Input.GetKeyDown("8")) {
            selectSlot(7);
        }


    }

    public int pickup(GameObject go) {

        for(int i = 0; i<inventory.Length; i++) {
            if(inventory[i] == null) {
                inventory[i] = go;
                go.transform.SetParent(transform);
                go.SetActive(false);
                go.transform.position = new Vector3(0,0,0);
                updateInventoryText();
                return 0;
            }
        }
        return 1;
    }
    
    public GameObject requestSelectedItem() {
        GameObject invitem = inventory[selectIndex];
        inventory[selectIndex] = null;
        updateInventoryText();
        return invitem;
    }

    void selectSlot(int slot) {
        selectIndex = slot;
        selectionBox.transform.SetParent(hotbar.transform.GetChild(selectIndex));
        selectionBox.transform.SetSiblingIndex(0);
        selectionBox.transform.localPosition = new Vector3(0, 0, 0);
    }

    void updateInventoryText() {
        for(int i = 0; i<inventory.Length; i++) {
            if(inventory[i] == null) {
                hotbar.transform.GetChild(i).GetChild(hotbar.transform.GetChild(i).childCount - 1).gameObject.GetComponent<TMP_Text>().text = "";
                continue;
            }
            hotbar.transform.GetChild(i).GetChild(hotbar.transform.GetChild(i).childCount - 1).gameObject.GetComponent<TMP_Text>().text = inventory[i].GetComponent<Interactable>().itemname;
        }
    }
}
