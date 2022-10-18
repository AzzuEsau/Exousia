using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DecisionTree {
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
            Console.Write(rebuild.info + " ");
            PrintPrev (rebuild.left);
            PrintPrev (rebuild.right);
        }
    }

    public void PrintPrev ()
    {
        PrintPrev (root);
        Console.WriteLine();
    }

    private void printInside (Node rebuild)
    {
        if (rebuild != null)
        {    
            printInside (rebuild.left);
            Console.Write(rebuild.info + " ");
            printInside (rebuild.right);
        }
    }

    public void printInside ()
    {
        printInside (root);
        Console.WriteLine();
    }


    private void printNext (Node rebuild)
    {
        if (rebuild != null)
        {
            printNext (rebuild.left);
            printNext (rebuild.right);
            Console.Write(rebuild.info + " ");
        }
    }


    public void printNext ()
    {
        printNext (root);
        Console.WriteLine();
    }

    // static void Main(string[] args)
    // {
    //     DecisionTree abo = new DecisionTree ();
    //     abo.Insert (100);
    //     abo.Insert (50);
    //     abo.Insert (25);
    //     abo.Insert (75);
    //     abo.Insert (150);
    //     Console.WriteLine ("Impresion preorden: ");
    //     abo.PrintPrev ();
    //     Console.WriteLine ("Impresion entreorden: ");
    //     abo.printInside ();
    //     Console.WriteLine ("Impresion postorden: ");
    //     abo.printNext ();
    //     Console.ReadKey();
    // }
}