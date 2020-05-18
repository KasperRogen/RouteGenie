using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class XMLReader : MonoBehaviour
{

    public static List<Node> Nodes = new List<Node>();
    public static List<Way> Ways = new List<Way>();
    // Start is called before the first frame update
    void Start()
    {
        XmlTextReader reader = new XmlTextReader("C:\\Users\\kaspe\\Desktop\\Roads.xml");

        Way way = new Way();
        while (reader.Read())
        {
            if (reader.Name == "node")
            {
                Node node = new Node();
                try { 
                    while (reader.MoveToNextAttribute())
                    {
                        switch (reader.Name)
                        {
                            case "id":
                                node.ID = long.Parse(reader.Value);
                                break;
                            case "lat":
                                node.Lat = double.Parse(reader.Value);
                                break;
                            case "lon":
                                node.Lon = double.Parse(reader.Value);
                                break;
                        }

                    }
                } catch(Exception ex)
                {
                    Debug.Log("tried to convert: " + reader.Name + " " + reader.Value + ". " + ex);
                }
                Nodes.Add(node);
            }

            if (reader.Name == "way")
            {
                way = new Way();
                try
                {
                    while (reader.MoveToNextAttribute())
                    {
                        switch (reader.Name)
                        {
                            case "id":
                                way.ID = long.Parse(reader.Value);
                                break;
                        }

                    }
                    
                }
                catch (Exception ex)
                {
                    Debug.Log("tried to convert: " + reader.Name + " " + reader.Value + ". " + ex);
                }
                Ways.Add(way);
            }

            if (reader.Name == "nd")
            {
                try
                {
                    while (reader.MoveToNextAttribute())
                    {
                        switch (reader.Name)
                        {
                            case "ref":
                                way.Nodes.Add(long.Parse(reader.Value));
                                break;
                        }

                    }

                }
                catch (Exception ex)
                {
                    Debug.Log("tried to convert: " + reader.Name + " " + reader.Value + ". " + ex);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class Node
{
    public long ID;
    public double Lon, Lat;

    public override string ToString()
    {
        return ID.ToString();
    }
}

public class Way
{
    public enum RoadType
    {
        foot, bike, car
    }

    public long ID;
    public List<long> Nodes;
    RoadType Type;

    public Way()
    {
        Nodes = new List<long>();
    }
}