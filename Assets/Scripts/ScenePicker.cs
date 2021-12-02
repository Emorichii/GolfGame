using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenePicker : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        int progress = PlayerPrefs.GetInt("Progress", 1);
        int totalChildren = this.transform.childCount;

        Button[] buttons = new Button[transform.childCount];

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = transform.GetChild(i).GetComponent<Button>();
            buttons[i].interactable = false;
        }
        if(progress > 12)
        {
            progress = 12;
        }
        for(int i = 0; i < progress; i++)
        {
            buttons[i].interactable = true;
        }
    }

    
}
