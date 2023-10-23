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
    
    public bool typingEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Return)) {

            if(player.movementLocked && !typingEnabled) { //scuffed way to detect if the playerlock is caused by the text entry or not. If not, we return
                return;
            }
            
            typingEnabled = !typingEnabled;
            player.movementLocked = typingEnabled;

            if(!typingEnabled) {
                if(currentText == "/clear") {
                    allText = "";
                    currentText = "";
                }
                allText += "\\n" + currentText;
                currentText = "";
            }

        }

        history.text = allText.Replace("\\n", "\n");

        if(!typingEnabled) {
            currentText = "";
            cursorInternal = "";
            current.text = "";
            return;
        }

        current.text = ">" + currentText + cursorInternal;
        
        if(Input.anyKeyDown) {
            
            if(Input.GetKeyDown(KeyCode.Space)) {
                currentText = currentText + " ";
            } else if(Input.GetKeyDown(KeyCode.Backspace)) {
                try {
                    currentText = currentText.Remove(currentText.Length - 1);
                } catch {}
            } else {
                currentText = currentText + getLetterPressed();
            }
        }
    }

    void FixedUpdate() {
        if(!enabled) {
            cursorInternal = "";
            cursorTimerInternal = 0;
            return;
        }

        cursorTimerInternal++;

        if(cursorTimerInternal < cursorTimer) {
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
