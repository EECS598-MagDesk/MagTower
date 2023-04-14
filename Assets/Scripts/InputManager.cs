using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class InputManager : MonoBehaviour
{

    public string inputDir = "";

    public GameObject controller_prefab;

    //List<GameObject> controllers;

    public List<Vector3> targets;
    public List<Vector3> targetRotations;

    public List<Vector3> testPos = new List<Vector3>();

    private float speed = 100000f;
    private float aiSpeed = 20f;

    private float originY = 0f;
    public float lengthMulti = 73f;
    public float widthMulti = 36f;
    private float heightMulti = 70f;

    private float prevTime = 0f;
    private float timeOffset = 0.01f;

    public communicationManager commManager;

    public bool useTest = false;
    public bool useCommManager = false;
    public bool useAI = false;
    
    // Start is called before the first frame update
    void Start()
    {
        /*        controllerOne = GameObject.Instantiate(controller_prefab, gameObject.transform);
                controllerOne.SetActive(false);
                controllerOne.GetComponent<sliderCheck>().score_manger = score;*/
        //controllers = new List<GameObject>();
        targets = new List<Vector3>();
        targetRotations = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > prevTime + timeOffset)
        {
            if (useCommManager)
            {
                readCommManager();
            }
            else
            {
                readInput(inputDir);
            }
            prevTime = Time.time;
        }
    }

    Vector3 SetYOffset(Vector3 pos)
    {
        return new Vector3(pos[0], pos[1], pos[2]);
    }

    void normalize(ref float x, ref float y, ref float z)
    {
        x = lengthMulti * x;
        y = originY + heightMulti * y;
        z = widthMulti * z;
    }

    void normalize(ref Vector3 v)
    {
        v[0] = lengthMulti * v[0];
        v[1] = originY + heightMulti * v[1];
        v[2] = widthMulti * v[2];
    }

    void readCommManager()
    {
        string data = commManager.Get();
        string pattern = @"\([^)]*\)";

        // Match the pattern in the input string from the end using RightToLeft option
        Match lastMatch = Regex.Match(data, pattern, RegexOptions.RightToLeft);

        // Check if a match was found
        if (lastMatch.Success)
        {
            Debug.Log(lastMatch.Value);
        }
        else
        {
            return;
        }

        string[] lines = lastMatch.Value.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );


        List<Vector3> onePos = new List<Vector3>();
        List<Vector3> twoPos = new List<Vector3>();

        targets.Clear();

        for (int i = 1; i < lines.Length - 1; i++)
        {
            string line = lines[i];
            string[] parsedLine = line.Split(char.Parse(" "));
            float x = float.Parse(parsedLine[0]);
            float y = float.Parse(parsedLine[2]);
            float z = float.Parse(parsedLine[1]);
            normalize(ref x, ref y, ref z);
            targets.Add(new Vector3(-x, y, -z));
        }
    }

    void readInput(string dir)
    {
        List<Vector3> onePos = new List<Vector3>();
        List<Vector3> twoPos = new List<Vector3>();

        /*
         * put file read and parse function here
         * 
         */

        StreamReader reader = new StreamReader(this.inputDir);

        List<string> lines = new List<string>();
        while(!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            lines.Add(line);
            
        }
        reader.Close();
        targets.Clear();
        targetRotations.Clear();
        foreach (string line in lines) {
            //Debug.Log(line);
            string[] parsedLine = line.Split(char.Parse(" "));
            float x = float.Parse(parsedLine[0]);
            float y = float.Parse(parsedLine[2]);
            float z = float.Parse(parsedLine[1]);
            float r = 0;
            if (parsedLine.Length > 3)
            {
                r = -float.Parse(parsedLine[3]) + 360f;
            }
            normalize(ref x, ref y, ref z);
            targets.Add(new Vector3(z, y, -x));
            targetRotations.Add(new Vector3(0, r, 0));
        }        
    }
}
