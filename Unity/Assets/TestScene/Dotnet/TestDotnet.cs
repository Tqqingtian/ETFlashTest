using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDotnet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //print(Input.GetAxis("Horizontal"));
    }

    public void OnClick1() { print("按钮一"); }
    public void OnClick2() { print("按钮二"); }
}
