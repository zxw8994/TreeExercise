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
        // Set new node if null, otherwise insert a left and right child node
        public Node insertNode(Node root, Node parent, int depth, char pos)
        {
            if (root == null)
            {
                root = new Node();
                root.parent = parent;

                root.depth = depth;
                root.value = 0;
            }
            else
            {
                root.leftChild = insertNode(root.leftChild, root, depth, 'l');
                root.rightChild = insertNode(root.rightChild, root, depth, 'r');
            }
            return root;
        }


        // Sets the root node and its value
        public Node insertRoot(Node root, int val)
        {
            root = new Node();

            root.depth = 1;
            root.value = val;

            return root;
        }


        // Checks if node needs a value, if not call again for its children
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
        int findNeighborValue(Node node, char pos, int depth)
        {
            bool stepOneDone = false;
            bool stepTwoDone = false;

            Node currentNode = node;
            Node childNode = new Node();


            // Loop that uses node's parent to go up the tree by 'hugging left/right wall'
            while (!stepOneDone)
            {
                if (currentNode.parent == null)
                {
                    return 0;
                }
                // if original node was a leftChild, goes up tree checking until parents leftchild is not the current node
                if (pos == 'l')         
                {
                    // End loop if currentNode's parent's leftChild isn't currentNode
                    if (currentNode.parent.leftChild != currentNode)        
                    {
                        childNode = currentNode;
                        currentNode = currentNode.parent;
                        stepOneDone = true;
                    }
                    else if (currentNode.parent != null)
                    {
                        currentNode = currentNode.parent;
                    }
                    else return 0;
                }
                // if original node was a rightChild, goes up tree checking until parents rightchild is not the current node
                else if (pos == 'r')
                {
                    // End loop if currentNode's parent's rightChild isn't currentNode
                    if (currentNode.parent.rightChild != currentNode)       
                    {
                        childNode = currentNode;
                        currentNode = currentNode.parent;
                        stepOneDone = true;
                    }
                    else if (currentNode.parent != null)
                    {
                        currentNode = currentNode.parent;
                    }
                    else return 0;
                }
            }


            // Loops back down tree, cheching child nodes on the opposite side when going up, until original nodes parents depth is met
            while (!stepTwoDone)
            {
                if (pos == 'l')
                {
                    if (childNode == currentNode.rightChild)
                    {
                        currentNode = currentNode.leftChild;
                    }
                    else
                    {
                        currentNode = currentNode.rightChild;
                    }

                    if (currentNode.depth == (depth - 1))
                    {
                        stepTwoDone = true;
                    }
                }
                else if (pos == 'r')
                {
                    if (childNode == currentNode.leftChild)
                    {
                        currentNode = currentNode.rightChild;
                    }
                    else
                    {
                        currentNode = currentNode.leftChild;
                    }

                    if (currentNode.depth == (depth - 1))
                    {
                        stepTwoDone = true;
                    }
                }
            }
            // Parents neighbor's node value
            return currentNode.value;
        }


        // Traverses tree from far left to right
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
            Node root = null;
            Tree tree = new Tree();
            int desiredDepth;


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
            

            // Inserts root node
            root = tree.insertRoot(root, 1);

            for (int i = 2; i < desiredDepth + 1; i++)
            {
                root = tree.insertNode(root, root, i, 'n');
                // Method to add in node's value
                tree.addValue(root, 'n');
            }

            tree.traverseTree(root);
        }
    }
}
