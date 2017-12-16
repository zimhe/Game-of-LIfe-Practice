using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Cube_Practice : MonoBehaviour {
    //Variables
    int CurrentState = 0;
    int FutureState = 0;

	
	// Update is called once per frame
	void Update () {
        CurrentState = FutureState;
    }
    //Display the cube
   public  void DisplayCubesRed()
    {
        MaterialPropertyBlock Properties = new MaterialPropertyBlock();
        MeshRenderer Renderer;

        if (CurrentState == 0)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if(CurrentState == 1)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            Properties.SetColor("_Color", Color.red );

            Renderer = gameObject.GetComponent<MeshRenderer>();
            Renderer.SetPropertyBlock(Properties);
        }
    }
    public void DisplayCubesBlack()
    {
        MaterialPropertyBlock Properties = new MaterialPropertyBlock();
        MeshRenderer Renderer;

        if (CurrentState == 0)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if (CurrentState == 1)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            Properties.SetColor("_Color", Color.black );

            Renderer = gameObject.GetComponent<MeshRenderer>();
            Renderer.SetPropertyBlock(Properties);
        }
    }

    public void DisplayCubesDistance(float _distance,float _MaxDistance)
    {
        MaterialPropertyBlock Properties = new MaterialPropertyBlock();
        MeshRenderer Renderer;
        if (CurrentState == 0)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if (CurrentState  == 1)
        {
            // Remap the age value relative to maxage to range of 0,1
            float mappedvalue = Remap(_distance , 0, _MaxDistance  , 0.0f, 1.0f);
            //two colors to interpolate between
            Color color1 = new Color(1, 0, 0, 1);
            Color color2 = new Color(0, 1, 0, 1);
            //interpolate color from mapped value
            Color mappedcolor = Color.Lerp(color1, color2, mappedvalue);
            Properties.SetColor("_Color", mappedcolor);
            // Updated the mesh renderer color
            gameObject.GetComponent<MeshRenderer>().enabled = true;

            Renderer = gameObject.GetComponent<MeshRenderer>();
            Renderer.SetPropertyBlock(Properties);
        }
      
    }

    public void SetFutureState(int _FutureState)
    {

        FutureState = _FutureState;
    }
    public void SetState(int _State)
    {
        FutureState = _State;
    }
    public int GetState()
    {
        return CurrentState;

    }
    public int GetFutureState()
    {
        return CurrentState;
    }
    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
