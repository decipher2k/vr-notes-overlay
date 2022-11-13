using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public static string note = "";
    public void onClickSet()
    {
        note=GameObject.Find("txtNote").GetComponent<TMPro.TextMeshProUGUI>().text;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
