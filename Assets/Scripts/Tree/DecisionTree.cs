using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DecisionTree : MonoBehaviour
{
    class Node
    {
        public int info;
        public Node left, right;
    }
    Node root;

    public DecisionTree() 
    {
        root=null;
    }
    
    public void Insert (int info)
    {
        Node newNode;
        newNode = new Node ();
        newNode.info = info;
        newNode.left = null;
        newNode.right = null;
        if (root == null)
            root = newNode;
        else
        {
            Node before = null, rebuild;
            rebuild = root;
            while (rebuild != null)
            {
                before = rebuild;
                if (info < rebuild.info)
                    rebuild = rebuild.left;
                else
                    rebuild = rebuild.right;
            }
            if (info < before.info)
                before.left = newNode;
            else
                before.right = newNode;
        }
    }


    private void PrintPrev (Node rebuild)
    {
        if (rebuild != null)
        {
            Debug.Log(rebuild.info + " ");
            PrintPrev (rebuild.left);
            PrintPrev (rebuild.right);
        }
    }

    public void PrintPrev ()
    {
        PrintPrev (root);
        Debug.Log("Line");
    }

    private void printInside (Node rebuild)
    {
        if (rebuild != null)
        {    
            printInside (rebuild.left);
            Debug.Log(rebuild.info + " ");
            printInside (rebuild.right);
        }
    }

    public void printInside ()
    {
        printInside (root);
        Debug.Log("Line");
    }


    private void printNext (Node rebuild)
    {
        if (rebuild != null)
        {
            printNext (rebuild.left);
            printNext (rebuild.right);
            Debug.Log(rebuild.info + " ");
        }
    }


    public void printNext ()
    {
        printNext (root);
        Debug.Log("Line");
    }

    public void Start()
    {
        DecisionTree tree = new DecisionTree ();
        tree.Insert (100);
        tree.Insert (50);
        tree.Insert (25);
        tree.Insert (75);
        tree.Insert (150);
        Debug.Log("Impresion preorden: ");
        tree.PrintPrev ();
        Debug.Log("Impresion entreorden: ");
        tree.printInside ();
        Debug.Log("Impresion postorden: ");
        tree.printNext ();
    }
}