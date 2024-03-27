# Importing

To import into Unity, create a new project and copy everything here to the "Assets" directory of your project. The scene with the graph is "GraphScene".

You might need to re-add the scripts to the objects if the paths are lost when importing. That includes the graph.cs script in the graph object, the node.cs script for the node prefab you want to use, the edge.cs script for storing edge target nodes, and the camera.cs script for the camera.

The node and edge prefabs might have lost their material too, so you might need to re-add that. 

# Using

The graph objects has a number of parameters:
   - The file to use (JSON file) - if no file is given, nothing will be rendered.
   - The prefab to use for nodes. It should be a RigidBody, preferably with frozen rotation and a child that is a 3D text to show the label.
   - The prefab for edges
   - The size of the space in which nodes will be initially placed.

The camera can go back and forth and will always look towards the centre of the graph. It has a:
   - a speed parameter
   - it needs to be provided with the graph object
   - Note that as currently implemented the camera used in Game Mode is not useful for larger networks with node counts on the order of magnitude of thousands or more.

The prefabs folder includes a number of node and edge styles from the article linked above. Some include a text for the label of the node, some don't.
To set the distance between nodes, scale the box collider of the node prefab you are using.

The JSON file used for this project is not included for privacy and proprietary reasons, however it's general structure is as follows:
{
   "nodes" : [*comma separated list of nodes as exported by neo4j*],
   "relationships" : [*comma separated list of edges as exported by neo4j*]
}
