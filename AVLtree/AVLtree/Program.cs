using System;

namespace AVLtree
{   
    /* CIUCUR DANIEL Uvt - Timisoara informatica-engleza anul 1
     * 
     * ADS 2 Homework 2 contruct an AVL tree that computes the word frequency */
    class Node
    {
        public int height;
        public string key;
        public int count; // used in order to count the appearances of each word
        public Node left, right;

        public Node(string d)
        {
            key = d;
            height = 1;
            count = 1;
        }
    }

    public class AVLTree
    {

        Node root;

        int height(Node N)
        {
            if (N == null)
                return 0;

            return N.height;
        }

        int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        Node rightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            x.right = y;
            y.left = T2;

            y.height = max(height(y.left),
                        height(y.right)) + 1;
            x.height = max(height(x.left),
                        height(x.right)) + 1;

            return x;
        }

        Node leftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;

            y.left = x;
            x.right = T2;

            x.height = max(height(x.left),
                        height(x.right)) + 1;
            y.height = max(height(y.left),
                        height(y.right)) + 1;

            return y;
        }

        int getBalance(Node N)
        {
            if (N == null)
                return 0;

            return height(N.left) - height(N.right);
        }

        Node insert(Node node, string key)
        {

            if (node == null)
                return (new Node(key));

            if (String.Compare(key, node.key) < 0)
                node.left = insert(node.left, key);
            else if (String.Compare(key, node.key) > 0)
                node.right = insert(node.right, key);
            else // We do not allow duplicate keys, we just count the word frequency here with our "count" variable
            {
                node.count++;
                return node;
            }

            node.height = 1 + max(height(node.left),
                                height(node.right));

            int balance = getBalance(node);

            if (balance > 1 && String.Compare(key, node.left.key) < 0)
                return rightRotate(node);


            if (balance < -1 && String.Compare(key, node.right.key) > 0)
                return leftRotate(node);


            if (balance > 1 && String.Compare(key, node.left.key) > 0)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            if (balance < -1 && String.Compare(key, node.right.key) < 0)
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            return node;
        }

        void preOrder(Node node)
        {
            if (node != null)
            {
                Console.Write(node.key + " ");
                Console.WriteLine(node.count + " ");
                preOrder(node.left);
                preOrder(node.right);
            }
        }

        public static void Main(String[] args)
        {
            AVLTree tree = new AVLTree();

            tree.root = tree.insert(tree.root, "The".ToLower());
            tree.root = tree.insert(tree.root, "quick".ToLower());
            tree.root = tree.insert(tree.root, "brown".ToLower());
            tree.root = tree.insert(tree.root, "fox".ToLower());
            tree.root = tree.insert(tree.root, "jumps".ToLower());
            tree.root = tree.insert(tree.root, "over".ToLower());
            tree.root = tree.insert(tree.root, "the".ToLower());
            tree.root = tree.insert(tree.root, "lazy".ToLower());
            tree.root = tree.insert(tree.root, "dog".ToLower());
            //The quick brown fox jumps over the lazy dog//
            
            Console.WriteLine("The words and their number of appearances is : ");
            tree.preOrder(tree.root);
            Console.ReadKey(); //this just prevents the console from shuting down right after it's oppened 
        }
    }
}
