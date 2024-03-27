/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
  //Script is attached to the Node prefab

//GameObject to which this script is associated in Unity
  GameObject epf;

//List of GameObjects that will eventually contain the graphical links between nodes
  List<GameObject>  edges  = new List<GameObject> ();

//List of GameObjects that will eventually represent the gravitational "spring" element of the links 
  List<SpringJoint> joints = new List<SpringJoint>();  
  
  void Start(){
    //Sets one of the overall labels equal to the name parameter, possibly set in the Unity editor?
    transform.GetChild(0).GetComponent<TextMesh>().text = name;
  }
  

  void Update(){    
    int i = 0;
    //Iterates through the edges list, which implies it is already populated when run during Update(), i.e. every frame
    foreach (GameObject edge in edges){
      //Set the current edge's position equal to a new vector pulling coordinates from the parent transform... not sure where these are coming from yet
      edge.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
      //Creates a new SpringJoint from the SpringJoint at array position i in the SpringJoint list; this also implies its pre-populated somehow before this runs
      SpringJoint sj = joints[i];
      //Creates a blank GameObject that is populated with the current SpringJoint's target (unsure if the target is a SpringJoint itself)
      GameObject target = sj.connectedBody.gameObject;
      //Unsure what "LookAt" does but it sets current edge to the target gameObject (from the springjoint list) 
      edge.transform.LookAt(target.transform);
      Vector3 ls = edge.transform.localScale;
      ls.z = Vector3.Distance(transform.position, target.transform.position);
      edge.transform.localScale = ls;
      edge.transform.position = new Vector3((transform.position.x+target.transform.position.x)/2,
					    (transform.position.y+target.transform.position.y)/2,
					    (transform.position.z+target.transform.position.z)/2);
      i++;
    }
  }

  public void SetEdgePrefab(GameObject epf){
    this.epf = epf;
  }
  
  public void AddEdge(Node n){
    SpringJoint sj = gameObject.AddComponent<SpringJoint> ();  
    sj.autoConfigureConnectedAnchor = false;
    sj.anchor = new Vector3(0,0.5f,0);
    sj.connectedAnchor = new Vector3(0,0,0);    
    sj.enableCollision = true;
    sj.connectedBody = n.GetComponent<Rigidbody>();
    GameObject edge = Instantiate(this.epf, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    edges.Add(edge);
    joints.Add(sj);
  }
    
}
*/