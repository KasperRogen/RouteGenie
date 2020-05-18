using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayBuilder : MonoBehaviour
{
    public int NodesToDraw = 0;
    public bool DrawAll;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DrawMap());
    }

    private void Update()
    {

    }

    private void DrawWay(Way way)
    {

        List<Node> nodes = new List<Node>();// XMLReader.Nodes.Where(node => way.Nodes.Contains(node.ID)).ToList();
        
        foreach(long id in way.Nodes)
        {
            nodes.Add(XMLReader.Nodes.Find(node => node.ID == id));
        }

        foreach(long id in way.Nodes.Except(nodes.Select(node => node.ID))){
            Debug.Log("missing: " + id);
        }
        


        for (int i = 0; i < (DrawAll ? nodes.Count-1 : NodesToDraw); i++)
        {
            Debug.DrawLine(CreatePos(nodes[i]), CreatePos(nodes[i+1]), Color.red, 10);
        }
    }

    IEnumerator DrawMap()
    {
        while (true)
        {

            if(XMLReader.Ways.Count > 0)
            {
                foreach (Way way in XMLReader.Ways)
                {
                    yield return new WaitForSeconds(5 / XMLReader.Ways.Count);
                    DrawWay(way);
                }
            } else
            {
                yield return new WaitForSeconds(1);
            }


        }
    }

    Vector2 CreatePos (Node node)
    {
        Vector2 MyPos = new Vector2(9.981820f, 57.016701f);
        return (MyPos - new Vector2((float)node.Lon, (float)node.Lat)) * 10000;
    }
}
