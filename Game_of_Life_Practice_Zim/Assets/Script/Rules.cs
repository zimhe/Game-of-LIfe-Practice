using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour {


    private int[] ruleInts = new int[4];

   
    public void setupRule(int _inst0, int _inst1, int _inst2, int _inst3)
    {
        ruleInts[0] = _inst0;
        ruleInts[1] = _inst1;
        ruleInts[2] = _inst2;
        ruleInts[3] = _inst3;
    }

    public int getInstruction(int _index)
    {
        return ruleInts[_index];
    }

    public void setInstruction(int _inst, int _index)
    {
        ruleInts[_index] = _inst;
    }

    public int[] getInstructions()
    {
        return ruleInts;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
