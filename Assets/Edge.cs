//See Graph.cs script for explanation of importing libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
See Graph.cs for explanation of MonoBehavior Inheritance

Pre-Condition: A prefab shape to represent edges, usually some form of line connecter, exists in the Unity project
Post-Condition: An edge prefab is associated with the Node to which it connects.
Overview:
This is a trivial class that exists to attach to each Edge graphical object so that they can be programmatically
manipulated for establishing their Node linkage. The sole contents of the class are a public (globally visible and accessible)
variable that excepts a Node object representing the Edge's target.
*/
public class Edge : MonoBehaviour
{
    public Node targetNode;
}
