using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Executable
{
    // public string exeName;

    //the super's state is what its doing. Opening, closing, open, closed
    // public string state;

    //this is the progress. 100 = open, 0 = closed. This allows for switching mid-open/close and the door not teleporting
    public int stateProgress = 0;

    public GameObject doorobject;

    public Vector3 closePos = new Vector3(0f, 0f, 0f);
    public Vector3 openPos = new Vector3(0f, 4.5f, 0f);

    private Vector3 delta;

    void Start() {
        state = "Closed";
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state) {
            case "Opening":
                stateProgress += 1;
                delta = openPos - closePos;

                delta *= 0.01f * stateProgress;
                doorobject.transform.localPosition = closePos + delta;

                if(stateProgress >= 100) {
                    state = "Open";
                    stateProgress = 100;
                }
                break;

            case "Open":
                stateProgress = 100;
                doorobject.transform.localPosition = openPos;
                break;

            case "Closed":
                stateProgress = 0;
                doorobject.transform.localPosition = closePos;
                break;


            case "Closing":
                stateProgress -= 1;
                delta = openPos - closePos;

                delta *= 0.01f * stateProgress;
                doorobject.transform.localPosition = closePos + delta;

                if(stateProgress <= 0) {
                    state = "Closed";
                    stateProgress = 0;
                }
                break;

            
            default:
                break;
        }
    }
    public void Open()  {state = "Opening";}
    public void Close() {state = "Closing";}

    public override string execute(string[] args) {
        Debug.Log(args);
        switch (args[1]) {
            case "open":
                Open();
                return "Opening Door\n";
            case "close":
                Close();
                return "Closing Door\n";
            default:
                return "Invalid Argument\n";
        }
    }
}
