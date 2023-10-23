using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Computer : Interactable 
{

    public GameObject interactor = null;
    public TMP_Text TMP = null;

    public string prevCommand = "";
    public string commandOutput = "";
    public string command = "";
    public string cursor = "_";

    private string cursorInternal = "";

    public int cursorTimer = 60;
    private int cursorTimerInternal = 0;

    private bool skipNext = false;
    public GameObject[] findables;

    public Executable[] executables;


    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        if(interactor == null) {
            return;
        }

        if(skipNext) {
            skipNext = false;
            return;
        }

        
        if(Input.GetKey(KeyCode.Escape)) {
            interactor.GetComponent<Player>().movementLocked = false;
            interactor = null;
            return;
        }


        //This is also shameful, but it works. If Unity isnt going to implement basic features, im not going to optimize my code.
        if(Input.anyKeyDown) {
            
            if(Input.GetKeyDown(KeyCode.Space)) {
                command = command + " ";
            } else if(Input.GetKeyDown(KeyCode.Return)) {
                enter();
                return;
            } else if(Input.GetKeyDown(KeyCode.Backspace)) {
                command = command.Remove(command.Length - 1);
            } else {
                command = command + getLetterPressed();
            }
        }

        if(prevCommand == "") {
            TMP.text = ">" + command + cursorInternal;
        } else {
            TMP.text = ">" + prevCommand + "\n" + commandOutput + ">" + command + cursorInternal;
        }
    }

    void FixedUpdate() {
        cursorTimerInternal++;

        if(cursorTimerInternal > cursorTimer) {
            cursorInternal = cursor;
        } else {
            cursorInternal = "";
        }

        cursorTimerInternal %= cursorTimer*2;
    }

    void enter() {
        string res = runCommand(command);
        // prevText += ">" + command + "\n" + res;
        commandOutput = res;
        prevCommand = command;

        if(res == "clear\n") {
            commandOutput = "";
        }

        // TMP.text = prevText + ">";
        command = "";
    }

    string runCommand(string command) {
        string[] cmd = command.Split(" ");
        string returnString = "";

        switch(cmd[0]) {
            case "ls":
                for(int i = 0; i<executables.Length; i++) {
                    returnString += executables[i].exeName + "  (" + executables[i].state + ")\n";
                }
                break;

            case "ping":
                returnString += fn_ping(cmd) + "\n";
                break;
            
            default:

                returnString = "Invalid Command\n";

                for(int i = 0; i<executables.Length; i++) {
                    if(cmd[0] == executables[i].exeName) {
                        returnString = executables[i].execute(cmd);
                    }
                }

                break;
        }

        return returnString;

    }

    
    // public static T[] SubArray<T>(this T[] data, int index, int length)
    // {
    //     T[] result = new T[length];
    //     Array.Copy(data, index, result, 0, length);
    //     return result;
    // }


    /*
    I am ashamed to write this. This is the code I would have wrote given this problem in 2018 (thats not a good thing)
    The reason I have to is because Unity provides no better way to make a custom textbox than this.
    Why dont I use a textbox?
        I dont want to.
    Anyways, Please do better Unity.
        even after the runtime fee
    */
    string getLetterPressed() {
        //Why cant i split a string on nothing like python?
        string[] lettersToRead = "q w e r t y u i o p a s d f g h j k l z x c v b n m ` 1 2 3 4 5 6 7 8 9 0 - = ~ ! @ # $ % ^ & * ( ) _ + [ ] { } ; ' : \" , . / < > ? \\".Split(" ");

        for(int i = 0; i<lettersToRead.Length; i++) {
            if(Input.GetKeyDown(lettersToRead[i])) {
                return lettersToRead[i];
            }
        }

        return null;
    }

    public override void interact(GameObject interactor) {
        interactor.GetComponent<Player>().movementLocked = true;
        this.interactor = interactor;
        skipNext = true;
    }


    private string fn_ping(string[] cmd) {

        string output;

        if(cmd.Length == 1) {
            output = "Objects you can locate:";
            for(int i = 0; i<findables.Length; i++) {
                output += "\n" + findables[i].name.ToLower().Replace(" ", "-");
            }
            return output;
        }
        
        output = "Object is close to:";

        GameObject findable = null;

        for(int i = 0; i<findables.Length; i++) {
            if(findables[i].name.ToLower().Replace(" ", "-") == cmd[1]) {
                findable = findables[i];
                break;
            }
        }

        if(findable == null) {
            output = "Object doesnt exist";
            return output;
        }

        for(int i = 0; i<executables.Length; i++) {
            output += "\n" + executables[i].exeName + " : " + (int)(Vector3.Distance(executables[i].getPos(), findable.transform.position)) + "m";
        }

        return output;
    }
    
}
