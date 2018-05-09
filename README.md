# TreeExercise
# How To Use
1. Compile program (using Visual Studio 2015).
2. In the console the program will ask user to input a desired depth for the tree to generate.
3. After entering an integer, program will print all tree nodes with their depth and value on the console based on their position in the tree. Goes from the root down to far left of the tree then makes its way right.

# How It Works
Each node contains a reference to a left and right child node, parent node, value, and depth in the tree. 
The program works by looping through the InsertNode() and AddValue() functions for each tree depth there is. InsertNode() works by checking if the node parameter is null. If so it creates a new node, setting its parent, depth, and value=0 before it returns. If the node was not null, it instead calls InsertNode() twice, one for both its right and left child nodes. This how every node in the tree is created.

AddValue() checks if the node parameter's value is 0, if so it will call FindNeighborValue() in order to get the value of its parents neighbor. If the node's value is not 0 it means it already got its value so AddValue() is called using the nodes children. FindNeighborValue() is based on if the node was a left or right child and works by going up the tree using a nodes parents. An example being, if a leftChild node is its parents leftchild, then we need to go further up the tree. Same goes for the right. This loops through until the parents child of the same side is not the currentNode. Then we loop down the tree to a depth one above the original node using children of the side opposite to which we came up with.

This goes on until every node has a value. After every node with their depth and value is printed to the console.