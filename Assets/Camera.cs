//See Graph.cs for an explanation of importing libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//See Graph.cs for explanation of MonoBehavior Inheritance
public class Camera : MonoBehaviour
{
//Create a variable that holds an object of type GameObject representing the overall set of nodes in the network
  public GameObject graph;

      //Create a floating point variable setting the speed of camera movement
  public float speed = 5.0f;
      
      //See Graph.cs for explaination of Start() method
    void Start(){}
      /*
      Pre-Condition: Unity has rendered the graph network visualization and a Unity camera component has been created
      and linked with the visualization.
      Post-Condition: When the project is run and the user is in "Game" mode in the Unity editor, the camera on the scene
      remains focused on the calculated "center" of the graph and zooms in or out depending on which key the user presses.
      Overview:
      This code is adapated as is from the cited open source force directed graph project.
      The Update function runs every "frame" of execution, analagous to a frame in a movie reel.
      During every execution of the method, once per frame, it calculates the center of the visualized
      graph by iterating through every Node's coordinates and ensures the camera focuses on the center.
      If the up or down arros on the keyboard are pressed, the camera zooms in or out.

      Caveat:
      This Script gets computationally expensive with larger networks, especially the order of magnitude expected by an
      installation wide computer network. Disabling the script and using the built-in camera in "Scene" mode in Unity is 
      a stop-gap solution. Besides making the actual graph visualization more efficient, the ultimate solution is to implement
      a camera script that computes center of the graph once and leaves the user on their own from there with the option to
      re-calcuate and refocus only when requested.
      */
    void Update()
    {
      // find centre of the graph
      float minx = 1000000.0f; float maxx = 0.0f;
      float miny = 1000000.0f; float maxy = 0.0f;
      float minz = 1000000.0f; float maxz = 0.0f;      
      foreach (Transform child in graph.transform){
	if (child.position.x < minx) minx = child.position.x;
	if (child.position.x > maxx) maxx = child.position.x;
	if (child.position.y < miny) miny = child.position.y;
	if (child.position.y > maxy) maxy = child.position.y;
	if (child.position.z < minz) minz = child.position.z;
	if (child.position.z > maxz) maxz = child.position.z;			
      }
      transform.LookAt(new Vector3((minx+maxx)/2,(miny+maxy)/2,(minz+maxz)/2));
      if (Input.GetKey("up")){
	transform.position += new Vector3(0,0,Time.deltaTime*speed);
      }
      if (Input.GetKey("down")){
	transform.position -= new Vector3(0,0,Time.deltaTime*speed);
      }      
    }
    
}
