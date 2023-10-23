using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEntry : MonoBehaviour
{
    public TMP_Text history;
    public TMP_Text current;
    public Player player;
    public string allText = "";
    public string currentText = "";
    
    public string cursor = "_";
    private string cursorInternal = "";

    public int cursorTimer = 60;
    private int cursorTimerInternal = 0;
    
    public bool enabled = false;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Return)) {
            enabled = !enabled;
            player.movementLocked = enabled;

            if(!enabled) {
                if(currentText == "/clear") {
                    allText = "";
                    currentText = "";
                }
                allText += "\\n" + currentText;
                currentText = "";
            }

        }

        current.text = currentText + cursorInternal;
        history.text = allText.Replace("\\n", "\n");

        if(!enabled) {
            currentText = "";
            cursorInternal = "";
            return;
        }

        
        if(Input.anyKeyDown) {
            
            if(Input.GetKeyDown(KeyCode.Space)) {
                currentText = currentText + " ";
            } else if(Input.GetKeyDown(KeyCode.Backspace)) {
                currentText = currentText.Remove(currentText.Length - 1);
            } else {
                currentText = currentText + getLetterPressed();
            }
        }
    }

    void FixedUpdate() {
        if(!enabled) {
            cursorInternal = "";
            return;
        }

        cursorTimerInternal++;

        if(cursorTimerInternal > cursorTimer) {
            cursorInternal = cursor;
        } else {
            cursorInternal = "";
        }

        cursorTimerInternal %= cursorTimer*2;
    }

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
}
