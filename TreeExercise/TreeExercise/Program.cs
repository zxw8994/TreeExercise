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
        public Node leftChild;
        public Node rightChild;
    }

    class Tree
    {

        public Node insertNode(Node root,int val)
        {

            if(root == null)
            {
                root = new Node();
                root.value = val;
            }

            return root;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            Node root = null;

            Tree tree = new Tree();



        }
    }
}
