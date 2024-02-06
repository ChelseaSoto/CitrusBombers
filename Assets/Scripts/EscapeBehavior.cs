using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBehavior : MonoBehaviour
{
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
