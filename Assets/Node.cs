//See Graph.cs script for explanation of importing libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//See Graph.cs for explanation of MonoBehavior Inheritance
public class Node : MonoBehaviour
{
    //Represents the GameObject in the Unity editor that this script is associated with; in this case the prefab shape for an Edge
    GameObject epf;

    // List (another Data Collection type) of GameObjects that will eventually contain the graphical links between Nodes
    List<GameObject> edges = new List<GameObject>();

    /*
    Pre-Condition: Edge relationships for the network graph programmatically established.
    Post-Condition: Each Node's connection (Edge) is rendered visually on screen using a prefab shape
    Overview:
    The Start method runs when the scene first executes. Other scripts will have populated the edges list and this 
    Start method iterates through it, calculates the midpoint between two Nodes that are connected, places a visual prefab
    indicating an edge, a line by default, at that midpoint, and scales its length to connect the two Nodes thus establishing
    a visual representation of a graph link.
    */
    void Start()
    {
        //Sets a text label for the component with which this script is associated equal to value of the variable "name"
        transform.GetChild(0).GetComponent<TextMesh>().text = name;

        for (int i = 0; i < edges.Count; i++)
        {
            GameObject edge = edges[i];
            // Assuming each edge knows its target Node, which should be set when the edge is created/added
            Node targetNode = edge.GetComponent<Edge>().targetNode; // Edge script needed

            // Calculate the mid-point and distance between the current node and the target node
            Vector3 midPoint = (transform.position + targetNode.transform.position) / 2f;
            float distance = Vector3.Distance(transform.position, targetNode.transform.position);

            // Adjust the edge's position and scale to connect the two nodes
            edge.transform.position = midPoint;
            edge.transform.LookAt(targetNode.transform.position);
            Vector3 currentScale = edge.transform.localScale;
            currentScale.z = distance;
            edge.transform.localScale = currentScale;
        }
    }

    //Placeholder for the standard Update method. Elements of this script do not need to be run every frame
    void Update()
    {
        
    }

    /*The "this" keyword refers to the context of whatever is running the script itself. In this case, its saying "whatever
    component is associated with this script, set its own visual prefab shape (what will actually display on screen) to the 
    visual prefab shape that was passed as a parameter.
    */
    public void SetEdgePrefab(GameObject epf)
    {
        this.epf = epf;
    }

    public void AddEdge(Node targetNode)
    {
        /*Instantiate (create) a GameObject called "edge" that uses the prefab shape held in the variable "epf", is visually rendered
        at the coordinates specified by a new 3D vector (x,y,z coordinates), and does not rotate (Quaternion.identity)
        */
        GameObject edge = Instantiate(epf, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        
        //Add the edge object to the List of Edges created earlier
        edges.Add(edge);

        //Using the Edge component on the edge prefab, created by Edge.cs when associated with a prefab, to store reference to the target node
        if (!edge.GetComponent<Edge>()) edge.AddComponent<Edge>();
        edge.GetComponent<Edge>().targetNode = targetNode;
    }
}
