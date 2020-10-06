using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;
public class ScoreScript : MonoBehaviour {

    public static int scoreValue = 0;
    public static int barrelValue = 0;
    
    Text ValueScore;
    static Image[] Barrels;
    // Use this for initialization
    void Start () {
        ValueScore = GetComponentInChildren<Text>();
        Barrels = GetComponentsInChildren<Image>();
    }

    public static void UpdateBarrelValue(int add)
    {
        barrelValue += add;
        Barrels[barrelValue].color = Color.white;
        if (barrelValue >= 3)
        {
            EventManager.Instance.Raise(new AllBarrelEvent());
        }
    }
	
	// Update is called once per frame
	void Update () {
        ValueScore.text = "X "+scoreValue;
    }
}
