using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScore : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;

    void Start()
    {
        scoreTxt.text = string.Format("" + BombBehavior1.score); 
    }
}
