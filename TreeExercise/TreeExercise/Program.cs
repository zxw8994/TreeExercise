using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeExercise
{


    class Node
    {
        public int value;
        public int depth;
        public Node parent;
        public Node leftChild;
        public Node rightChild;
    }

    class Tree
    {
        Program prog = new Program();
        public Node insertNode(Node root, Node parent, int depth, int val, char pos)
        {
            
            // Set new node's parent and depth in tree
            if(root == null)
            {
                root = new Node();
                root.parent = parent;
                root.depth = depth;
            }

            // Checks if node is the root node, set value to one
            if (pos == 'n') root.value = val;

            // Calls method to find parents neighbor's value based on if node is a left or right child
            if(pos == 'l')
            {
                root.value = val + findNeighborValue(root.parent, 'l', root.depth);
            }
            if(pos == 'r')
            {
                root.value = val + findNeighborValue(root.parent, 'r', root.depth);
            }

            // If max depth has not been met, add a new right and left node
            if (depth < prog.desiredDepth)
            {
                root.leftChild = insertNode(root.leftChild, root, depth + 1, root.value, 'l');
                root.rightChild = insertNode(root.rightChild, root, depth + 1, root.value, 'r');
            }
            return root;
        }

        // Goes up the tree then down again to find value of parents neighbor
        int findNeighborValue(Node nodeParent, char pos, int depth)
        {
            bool stepOneDone = false;
            //bool stepTwoDone = false;

            
            if(nodeParent.parent == null)
            {
                return 0;
            }

            Node currentNode = nodeParent;

            
            // Loop that uses node's parent to go up the tree by 'hugging left/right wall'
            while (!stepOneDone)
            {
                if (pos == 'l')         // if original node was a leftChild, goes up tree checking until parents leftchild is not the current node
                {
                    if (currentNode.parent.leftChild != currentNode)        // End loop if currentNode's parent's leftChild is currentNode
                    {
                        currentNode = currentNode.parent;
                        stepOneDone = true;
                    }else if(currentNode.parent.leftChild == currentNode && currentNode.value == 1)     // Checks for node at the far left
                    {
                        return 0;
                    }
                    else currentNode = currentNode.parent;
                }
                else if (pos == 'r')    // if original node was a rightChild, goes up tree checking until parents rightchild is not the current node
                {
                    if (currentNode.parent.rightChild != currentNode)       // End loop if currentNode's parent's rightChild is currentNode
                    {
                        currentNode = currentNode.parent;
                        stepOneDone = true;
                    }
                    else if (currentNode.parent.rightChild == currentNode && currentNode.value == 1)    // Checks for node at the far left
                    {
                        return 0;
                    }
                    else currentNode = currentNode.parent;
                }

            }

            // Loops back down tree, going checking opposite child, until original nodes parents depth is met
            while (currentNode.depth != depth - 1)
            {
                if (pos == 'l')
                {
                    currentNode = currentNode.rightChild;
                }
                else if (pos == 'r')
                {
                    currentNode = currentNode.leftChild;
                }
            }

            // Parents neighbor's node value
            return currentNode.value;
        }

    }


    class Program
    {
        public int desiredDepth;

        void Main(string[] args)
        {
            desiredDepth = 3;

            Node root = null;


            Tree tree = new Tree();


            root = tree.insertNode(root, null, 1, 1, 'n');

        }
    }
}
