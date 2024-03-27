/*
The "using" statement specifes pre-existing libraries the script imports for use.
In this case the script uses two libraries required for data structures and generics.
Data structures hold data in a structured manner like a list or dictionary.
Generics allow the specification of general types of objects without knowing what
specific kind it will be. In this case, generic data collection type objects.
UnityEngine is the library that provides the Unity specific features for the script.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;

/*
The class name is Graph and it "inherits" the capabilities of another class called
MonoBehavior. This class exists in a built-in libary that the script engine "knows"
about, so it doesn't need to be recreated here. MonoBehavior is a boiler plate
inheritance necessary for most classes that interact directly with Unit.
*/
public class Graph : MonoBehaviour
{
  //Declaration of certain variables and objects used within the rest of the script

  //Holds a basic text file with nodes and links in JSON format
  public TextAsset file;

  /*
  GameObjects refer to objects in the Unity engine. In this case they refer to some prefabricated
  shapes being used to graphically depict nodes and edges. These "prefabs" are specified in the
  Unity Editor rather than this script.
	*/
  public GameObject nodepf;
  public GameObject edgepf; 

  //Floating Point (decimal) variables used to manipulate position/size in 3-space
  public float width;
  public float length;
  public float height;
  
  /* 
  The Start method (collection of code, sometimes erroneously called functions) is run
	when the Unity project is first executed
	*/
  void Start()
  {
    //If no file with network data was specified display an error message and exit      
    if (file==null){	
      Debug.Log("No file");
    } else {	
      //Otherwise begin constructing the visualization
      LoadJSONFromFile(file);
    }      
  }

  //Another standard method that is run every single frame of execution
  void Update(){}

  /*
	This is open source code used from the source cited in the Appendix A: Inventory without modification.
	Pre-Condition: Text file using JSON format containing list of Nodes and Links for visualization
	Post-Condition: The network graph is visually encoded using prefab node and edge shapes in the Unity
	engine
	Algorithm Overview:
	
	1. Parse the JSON file into C# native data structures using the Newtonsoft.json library
  2. Iterate through the JSON objects looking for nodes and relationships
  3. For each Node:
    1. Create prefab shape to be rendered and set its location
    2. Scale the size based on number of times the underlying host was observed in network traffic
    3. Set the color depending on what ASN the host originates from - used as an alternative to IP address for 
      anonymization purposes.
  4. For each Edge:
    1. find its associated Nodes and create a connection between them to be rendered using a chosen prefab
	*/

	void LoadJSONFromFile(TextAsset f)
  {
        TextAsset placeholder = f;

        string path = Application.dataPath + "/Resources/finalDB.json";
        string temp = System.IO.File.ReadAllText(path);
        string jsonContent = temp.Replace("NaN", "0");

        JObject jsonObject = JObject.Parse(jsonContent); // Parse the JSON content to a JObject

        Dictionary<string,Node> nodes = new Dictionary<string,Node>();
        Dictionary<string, Color> colorCode = new Dictionary<string, Color>();

        Node n = null;

		    foreach (var node in jsonObject["nodes"])
        {
            string objectType = node["type"].ToString();
            string ID = node["id"].ToString();
            string label = node["labels"].ToString();

            var observationCount = 1;
            var asn = "";
            var properties = node["properties"];

            if (properties?["observationCount"] != null)
            {
                observationCount = (int)properties["observationCount"];
            }

            if(properties?["network"] != null)
            {
              asn = (String)properties["network"];
            }           

            if (objectType == "node")
            {
              GameObject go = Instantiate(nodepf, new Vector3(UnityEngine.Random.Range(-width/2, width/2), UnityEngine.Random.Range(-length/2, length/2), UnityEngine.Random.Range(-height/2, height/2)), Quaternion.identity);
              n = go.GetComponent<Node>();
              n.transform.parent = transform;
              n.SetEdgePrefab(edgepf);
              
              float scalePercentage = observationCount;

              Vector3 newScale = n.transform.localScale * (scalePercentage/10);

              n.transform.localScale = newScale;

              Renderer renderer = n.GetComponent<Renderer>();
              
              if (!colorCode.ContainsKey(asn))
              {
                Color nodeColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
                renderer.material.color = nodeColor;
                colorCode.Add(asn, nodeColor);
              }
              else
              {
                renderer.material.color = colorCode[asn];
              }
            }

            nodes.Add(ID, n);
        }

        foreach (var node in jsonObject["relationships"])
        {
              string objectType = node["type"].ToString();
              if (objectType == "relationship")
              {
                nodes[node["start"]["id"].ToString()].AddEdge(nodes[node["end"]["id"].ToString()]);
              }
        }
      }


}   

//The classes below outline the structure of JSON files that the parser expects
[Serializable]
public class GraphElement
{
    public string type;
    public string id;
    public List<string> labels;
    public Properties properties;
}

[Serializable]
public class Properties
{
    // Common properties
    public string name;
    public string firstObserved;
    public string lastObserved;

    // Node-specific properties
    public string address;
    public string version;
    public string network;

    // NetFlow-specific properties
    public string dst_geo_continent_name;
    public string src_geo_continent_name;
    public string dst_address;
    public int src_packets;
    public double src_longitude;
    public int src_bytes;
    public string flow_id;
    public string src_address;
    public string state;
    public int dst_bytes;
    public double dst_latitude;
    public int src_port;
    public int observationCount;
    public bool alerted;
    public string proto;
    public double dst_longitude;
    public int dst_port;
    public int dst_asn;
    public string src_as_organization_name;
    public string dst_geo_country_iso_code;
    public double src_latitude;
    public int dst_packets;
    public int age;
    public string src_geo_country_iso_code;
    public string dst_as_organization_name;
    public string sensor_name;

    // Add additional properties as needed
}

[Serializable]
public class Relationship
{
    public string id;
    public string type;
    public string label;
    public RelationshipNode start;
    public RelationshipNode end;
}

[Serializable]
public class RelationshipNode
{
    public string id;
    public List<string> labels;
}
