using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Practice : MonoBehaviour
{
    //Variables
    GameObject[,] grid;

    int alive = 0;
    public GameObject Obj;
    private int Columns = 50;
    private int Rows = 50;
    public float Spacing = 1.0f;


    private Rules RuleInUse;
    private Rules[] RuleStore = new Rules[2];
    private Rules Rule0 = new Rules();
    private Rules Rule1 = new Rules();

    private int inst0;
    private int inst1;
    private int inst2;
    private int inst3;

    public Texture2D seedimage;

    private bool Back = false;

    private int Live_x = 25;

    private int LIve_y = 25;

    private float moveTrace = 0.3f;

    private Vector3 Twice;
    private Vector3 OriginalSize;

    private float MaxDistance;

    

    // Use this for initialization.
    void Start()
    {
        Twice = new Vector3(2, 1, 2);
        OriginalSize = Obj.transform.localScale;

        MaxDistance  = Mathf.Sqrt(Columns  * Columns  + Rows  * Rows );
        //Columns = seedimage.width;
        //Rows = seedimage.height;
        CreateGrid();
        Rule0.setupRule(2, 3, 3, 3);
        Rule1.setupRule(3, 3, 2, 2);
        RuleStore[0] = Rule0;
        RuleStore[1] = Rule1;

        // setup a new instance of the grid
        RuleInUse = RuleStore[0];

        inst0 = RuleInUse.getInstruction(0);
        inst1 = RuleInUse.getInstruction(1);
        inst2 = RuleInUse.getInstruction(2);
        inst3 = RuleInUse.getInstruction(3);

        print(inst0);
        print(inst1);
        print(inst2);
        print(inst3);
    }

    // Update is called once per frame
    void Update()
    {
        CauculateCa();

        KeyPress();

        DynamicCube();

        GetDIstance();

    }

    void CreateGrid()
    {
        grid = new GameObject[Columns, Rows]; //设置Array 长度
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                // Create a new caCube
                Vector3 position = new Vector3(i * Spacing, 0, j * Spacing); // Unity Y axis is the Z axis
                Quaternion rotation = Quaternion.identity;
                GameObject newObj = Instantiate(Obj, position, rotation);
                newObj.transform.parent = gameObject.transform; // Attach the caCube to the caGrid
                // Assign a random type to the caCube

                /*float t = seedimage.GetPixel(i, j).grayscale;

                if (t > 0.8f)
                {
                    newObj.GetComponent<CA_Cube_Practice>().SetState(0);
                }
                else
                {*/

                newObj.GetComponent<CA_Cube_Practice>().SetState(Random.Range(0, 2));


                // Store the caCube in the data structure
                grid[i, j] = newObj;
            }
        }
    }

    // Get how nay cells are alive


    void CauculateCa()
    {
       
        alive = 0;
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                GameObject CurrentCube = grid[i, j];
                int CurrentNeighborCount = 0;
                int FutureState = 0;
                int CurrentCubeState = CurrentCube.GetComponent<CA_Cube_Practice>().GetState();


                //Regular Conditions
                if (i > 0 && i < Columns - 1 && j > 0 && j < Rows - 1)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            GameObject currentNeigbour = grid[i + x, j + y];
                            int NeighborState = currentNeigbour.GetComponent<CA_Cube_Practice>().GetState();
                            CurrentNeighborCount += NeighborState;
                        }
                    }
                    CurrentNeighborCount -= CurrentCubeState;
                }

                #region sideConditions

                //4 Corner Conditions
                if (i == 0 && j == 0)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        for (int y = 0; y < 2; y++)
                        {
                            GameObject currentNeigbour = grid[i + x, j + y];
                            int NeighborState = currentNeigbour.GetComponent<CA_Cube_Practice>().GetState();
                            CurrentNeighborCount += NeighborState;
                        }
                    }
                    CurrentNeighborCount -= CurrentCubeState;
                }
                if (i == 0 && j == Rows - 1)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        for (int y = 0; y < 2; y++)
                        {
                            GameObject currentNeigbour = grid[i + x, j - y];
                            int NeighborState = currentNeigbour.GetComponent<CA_Cube_Practice>().GetState();
                            CurrentNeighborCount += NeighborState;
                        }
                    }
                    CurrentNeighborCount -= CurrentCubeState;
                }
                if (i == Columns - 1 && j == 0)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        for (int y = 0; y < 2; y++)
                        {
                            GameObject currentNeigbour = grid[i - x, j + y];
                            int NeighborState = currentNeigbour.GetComponent<CA_Cube_Practice>().GetState();
                            CurrentNeighborCount += NeighborState;
                        }
                    }
                    CurrentNeighborCount -= CurrentCubeState;
                }
                if (i == Columns - 1 && j == Rows - 1)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        for (int y = 0; y < 2; y++)
                        {
                            GameObject currentNeigbour = grid[i - x, j - y];
                            int NeighborState = currentNeigbour.GetComponent<CA_Cube_Practice>().GetState();
                            CurrentNeighborCount += NeighborState;
                        }
                    }
                    CurrentNeighborCount -= CurrentCubeState;
                }



                //4 Side Conditions
                if (i == 0 && j > 0 && j < Rows - 1)
                {
                    for (int x = 0; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            GameObject currentNeigbour = grid[i + x, j + y];
                            int NeighborState = currentNeigbour.GetComponent<CA_Cube_Practice>().GetState();
                            CurrentNeighborCount += NeighborState;
                        }
                    }
                    CurrentNeighborCount -= CurrentCubeState;
                }
                if (i == Columns - 1 && j > 0 && j < Rows - 1)
                {
                    for (int x = 0; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            GameObject currentNeigbour = grid[i - x, j + y];
                            int NeighborState = currentNeigbour.GetComponent<CA_Cube_Practice>().GetState();
                            CurrentNeighborCount += NeighborState;
                        }
                    }
                    CurrentNeighborCount -= CurrentCubeState;
                }
                if (i > 0 && i < Columns - 1 && j == 0)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = 0; y <= 1; y++)
                        {
                            GameObject currentNeigbour = grid[i + x, j + y];
                            int NeighborState = currentNeigbour.GetComponent<CA_Cube_Practice>().GetState();
                            CurrentNeighborCount += NeighborState;
                        }

                    }
                    CurrentNeighborCount -= CurrentCubeState;
                }
                if (i > 0 && i < Columns - 1 && j == Rows - 1)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = 0; y <= 1; y++)
                        {
                            GameObject currentNeigbour = grid[i + x, j - y];
                            int NeighborState = currentNeigbour.GetComponent<CA_Cube_Practice>().GetState();
                            CurrentNeighborCount += NeighborState;
                        }

                    }
                    CurrentNeighborCount -= CurrentCubeState;
                }

                #endregion

                if (CurrentCubeState == 1)
                {
                    if (CurrentNeighborCount < inst0)
                    {
                        grid[i, j].GetComponent<CA_Cube_Practice>().SetFutureState(0);
                        FutureState = 0;
                    }
                    if (CurrentNeighborCount >= inst0 && CurrentNeighborCount <= inst1)
                    {
                        grid[i, j].GetComponent<CA_Cube_Practice>().SetFutureState(1);
                        FutureState = 1;
                    }
                    if (CurrentNeighborCount > inst1)
                    {
                        grid[i, j].GetComponent<CA_Cube_Practice>().SetFutureState(0);
                        FutureState = 0;
                    }
                }

                // Rule 2: for cells that are dead
                if (CurrentCubeState == 0)
                {
                    if (CurrentNeighborCount >= inst2 && CurrentNeighborCount <= inst3)
                    {
                        grid[i, j].GetComponent<CA_Cube_Practice>().SetFutureState(1);
                        FutureState = 1;
                    }
                }

                float DistanceToBox = 0;
                int dis_x = Live_x - i;
                int dis_y = LIve_y - j;

                if (dis_x < 0)
                {
                    dis_x = -dis_x;
                }
                if (dis_y < 0)
                {
                    dis_y = -dis_y;
                }
                DistanceToBox = Mathf.Sqrt(dis_x * dis_x + dis_y * dis_y);



                // Cube Color & Size
                CurrentCube.GetComponent<CA_Cube_Practice>().DisplayCubesDistance(DistanceToBox, MaxDistance);
                CurrentCube.transform.localScale = OriginalSize;

                // Update how many are alive
                alive += FutureState;
            }
        }
    }


    public int GetAlive()
    {
        return alive;
    }

    void KeyPress()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Back == false)
            {
                Back = true;
            }
            else
            {
                Back = false;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (LIve_y < Rows - 1)
            {
                LIve_y += 1;


            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (LIve_y > 0)
            {
                LIve_y -= 1;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Live_x > 0)
            {
                Live_x -= 1;
            }

        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Live_x < Columns - 1)
            {
                Live_x += 1;
            }
        }

        /* for (int i = -1; i <= 1; i++)
         {
             for (int j = -1; j <= 1; j++)
             {
                 int a = Random.Range(0, 2);
                 GameObject BackToLifeNB = grid[Live_x + i, LIve_y + j];
                 BackToLifeNB.GetComponent<CA_Cube>().SetState(a);
             }
         }*/

    }

    void DynamicCube()
    {
        if (Back == true)
        {
            grid[Live_x, LIve_y].GetComponent<CA_Cube_Practice>().SetFutureState(1);
            grid[Live_x, LIve_y].transform.localScale = Twice;

            if (Live_x - 1 >= 0 && Live_x + 1 <= Columns - 1)
            {
                grid[Live_x + 1, LIve_y].GetComponent<CA_Cube_Practice>().SetFutureState(1);
                grid[Live_x - 1, LIve_y].GetComponent<CA_Cube_Practice>().SetFutureState(1);
                grid[Live_x + 1, LIve_y].GetComponent<MeshRenderer>().enabled = false;
                grid[Live_x - 1, LIve_y].GetComponent<MeshRenderer>().enabled = false;
            }
            if (LIve_y - 1 >= 0 && LIve_y + 1 <= Rows - 1)
            {
                grid[Live_x, LIve_y + 1].GetComponent<CA_Cube_Practice>().SetFutureState(1);
                grid[Live_x, LIve_y - 1].GetComponent<CA_Cube_Practice>().SetFutureState(1);
                grid[Live_x, LIve_y + 1].GetComponent<MeshRenderer>().enabled = false;
                grid[Live_x, LIve_y - 1].GetComponent<MeshRenderer>().enabled = false;
            }
            if (Live_x - 1 >= 0 && Live_x + 1 <= Columns - 1 && LIve_y - 1 >= 0 && LIve_y + 1 <= Rows - 1)
            {
                grid[Live_x + 1, LIve_y + 1].GetComponent<CA_Cube_Practice>().SetFutureState(1);
                grid[Live_x - 1, LIve_y - 1].GetComponent<CA_Cube_Practice>().SetFutureState(1);
                grid[Live_x + 1, LIve_y - 1].GetComponent<CA_Cube_Practice>().SetFutureState(1);
                grid[Live_x - 1, LIve_y + 1].GetComponent<CA_Cube_Practice>().SetFutureState(1);
                grid[Live_x + 1, LIve_y + 1].GetComponent<MeshRenderer>().enabled = false;
                grid[Live_x - 1, LIve_y - 1].GetComponent<MeshRenderer>().enabled = false;
                grid[Live_x + 1, LIve_y - 1].GetComponent<MeshRenderer>().enabled = false;
                grid[Live_x - 1, LIve_y + 1].GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            grid[Live_x, LIve_y].transform.localScale = OriginalSize;
        }

    }

    void GetDIstance()
    {
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                int dis_x = Live_x - i;
                int dis_y = LIve_y - j;

                if (dis_x < 0)
                {
                    dis_x = -dis_x;
                }
                if (dis_y < 0)
                {
                    dis_y = -dis_y;
                }

               // DistanceToBox = Mathf.Sqrt(dis_x * dis_x + dis_y * dis_y);
            }
        }
    }
}

