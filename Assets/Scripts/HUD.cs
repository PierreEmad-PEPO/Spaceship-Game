using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text textField;
    [SerializeField] int hitsNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        textField.text = hitsNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBounce()
    {
        hitsNum++;
        textField.text = hitsNum.ToString();
    }
}
