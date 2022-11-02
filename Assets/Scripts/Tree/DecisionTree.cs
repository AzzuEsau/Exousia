using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DecisionTree : MonoBehaviour
{
    [SerializeField]
    private DecisionList Decision;


    class Node
    {
        public DecisionList info;
        public Node left, right;
    }
    Node root;

    public DecisionTree() 
    {
        root=null;
    }
    
    public void Insert (DecisionList info)
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
                if (rebuild.info.response == "yes")
                    rebuild = rebuild.left;
                else
                    rebuild = rebuild.right;
            }
            if (info.response == "yes")
                before.left = newNode;
            else
                before.right = newNode;
        }
    }


    private void PrintPrev (Node rebuild)
    {
        if (rebuild != null)
        {
            Debug.Log(JsonUtility.ToJson(rebuild.info));
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
            Debug.Log(JsonUtility.ToJson(rebuild.info));
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
            Debug.Log(JsonUtility.ToJson(rebuild.info));
        }
    }


    public void printNext ()
    {
        printNext (root);
        Debug.Log("Line");
    }

    public void Start()
    {
        DecisionTree tree = new DecisionTree();
        FakeD1Initialize();
        tree.Insert(Decision);
        tree.Insert(Decision);
        tree.Insert(Decision);
        tree.Insert(Decision);
        tree.Insert(Decision);
        // Debug.Log("Impresion preorden: ");
        // tree.PrintPrev ();
        // Debug.Log("Impresion entreorden: ");
        // tree.printInside ();
        // Debug.Log("Impresion postorden: ");
        // tree.printNext ();
    }

    public void FakeD1Initialize()
    {
        Decision.name = "Poseidon keep trident";
        Decision.time = 4000;
        Decision.response = "yes";
        Decision.response = null;
    }
}