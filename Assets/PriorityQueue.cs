using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T1>
{
    List<Node<T1>> queue;

    public PriorityQueue()
    {
        queue = new List<Node<T1>>();
    }

    public void enqueue(T1 item, float priority, Node<GraphNode> visitedBy, float g)
    {
        queue.Add(new Node<T1>(item, priority, visitedBy, g));
        queue.Sort();
    }

    public Node<T1> dequeue()
    {
        var ret = queue[0];
        queue.RemoveAt(0);
        return ret;
    }

    internal float peekG()
    {
        return queue[0].getG();
    }

    internal bool isEmpty()
    {
        return queue.Count == 0;
    }

    public T1 peek()
    {
        return queue[0].getItem();
    }


    public override string ToString()
    {
        string s = "[";
        foreach (Node<T1> n in queue)
        {
            s += n.getItem() + ", ";
        }
        s += "]";
        return s;
    }

}

public class Node<T> : System.IComparable
{
    Node<GraphNode> visitedBy;

    internal Node<GraphNode> getVisitedBy()
    {
        return visitedBy;
    }

    float g;
    T item;
    float priority;

    public Node(T item, float priority, Node<GraphNode> visitedBy, float g)
    {
        this.item = item;
        this.priority = priority;
        this.visitedBy = visitedBy;
        this.g = g;
    }

    public int CompareTo(object obj)
    {
        if (obj.GetType() == typeof(Node<T>))
        {
            Debug.Log(".");
            return (int)((priority - ((Node<T>)obj).getPriority()) * 1000);
        }
        else return -1;
    }

    internal float getG()
    {
        return g;
    }

    internal T getItem()
    {
        return item;
    }

    private float getPriority()
    {
        return priority;
    }
}
