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

            // Set new node
            if (root == null)
            {
                root = new Node();
                root.parent = parent;

                root.depth = depth;
                root.value = 0;

            }
            else
            {
                root.leftChild = insertNode(root.leftChild, root, depth, root.value, 'l');
                root.rightChild = insertNode(root.rightChild, root, depth, root.value, 'r');
            }
            return root;
        }

        public Node insertRoot(Node root, int depth, int val)
        {
            root = new Node();
            //root.parent = null;
            root.depth = depth;
            root.value = val;

            return root;
        }

        public void addValue(Node node, char pos)
        {
            if (node.value == 0)
            {
                if (node.parent == null)
                {
                    node.value = 1;
                }
                else
                {
                    node.value = node.parent.value + findNeighborValue(node, pos, node.depth);
                }
            }
            else
            {
                addValue(node.leftChild, 'l');
                addValue(node.rightChild, 'r');
            }
        }

        // Goes up the tree then down again to find value of parents neighbor
        int findNeighborValue(Node nodeParent, char pos, int depth)
        {
            bool stepOneDone = false;
            bool stepTwoDone = false;

            Node currentNode = nodeParent;

            // Loop that uses node's parent to go up the tree by 'hugging left/right wall'
            while (!stepOneDone)
            {
                if (currentNode.parent == null)
                {
                    return 0;
                }
                if (pos == 'l')         // if original node was a leftChild, goes up tree checking until parents leftchild is not the current node
                {

                    if (currentNode.parent.leftChild != currentNode)        // End loop if currentNode's parent's leftChild is currentNode
                    {
                        currentNode = currentNode.parent;
                        stepOneDone = true;
                    }
                    else if (currentNode.parent.leftChild == currentNode && currentNode.value == 1)     // Checks for node at the far left
                    {
                        return 0;
                    }
                    else if (currentNode.parent != null)
                    {
                        currentNode = currentNode.parent;
                    }
                    else return 0;
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
                    else if (currentNode.parent != null)
                    {
                        currentNode = currentNode.parent;
                    }
                    else return 0;
                }

            }

            // Loops back down tree, going checking opposite child, until original nodes parents depth is met
            while (!stepTwoDone)
            {
                if (pos == 'l')
                {
                    currentNode = currentNode.rightChild;
                    if (currentNode.depth == (depth - 1))
                    {
                        stepTwoDone = true;
                    }
                }
                else if (pos == 'r')
                {
                    currentNode = currentNode.leftChild;
                    if (currentNode.depth == (depth - 1))
                    {
                        stepTwoDone = true;
                    }
                }
            }
            // Parents neighbor's node value
            return currentNode.value;
        }


        public void traverseTree(Node root)
        {
            if (root == null)
            {
                return;
            }
            Console.WriteLine("Depth: " + root.depth + "    Value: " + root.value);

            traverseTree(root.leftChild);
            traverseTree(root.rightChild);
        }

    }


    class Program
    {
        static void Main(string[] args)
        {


            int desiredDepth;
            //Convert.ToInt32(Console.ReadLine());
            try
            {
                Console.Write("Enter how many levels the Tree should generate: ");
                desiredDepth = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Enter a number.");
                return;
            }

            Node root = null;
            Tree tree = new Tree();

            root = tree.insertRoot(root, 1, 1);

            for (int i = 2; i < desiredDepth + 1; i++)
            {
                // see if adding root as parent will set rootNode.parent as null
                root = tree.insertNode(root, root, i, 1, 'n');//, desiredDepth);
                // Method to add nodes value
                tree.addValue(root, 'n');
            }

            tree.traverseTree(root);
        }
    }
}
