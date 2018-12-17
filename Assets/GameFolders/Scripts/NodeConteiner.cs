using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NodeConteiner : MonoBehaviour
{

    public static NodeConteiner instance;
    public List<Node> _allNodes;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddToConteiner(Node node)
    {
        if (!_allNodes.Contains(node))
        {
            _allNodes.Add(node);
        }

        if (CheckAllNodeIn()) ClearColliders();
    }

    public Node TargetNode()
    {
        var nodeTarget = _allNodes.Where(x => x.target).First();
        return nodeTarget;
    }

    void ClearColliders()
    {
        var colliders = _allNodes.Select(node => node.gameObject.GetComponent<Collider>());
        foreach (var item in colliders)
        {
            Destroy(item);
        }
    }

    bool CheckAllNodeIn()
    {
        var nodes = FindObjectsOfType<Node>().ToList();
        if (_allNodes.Count == nodes.Count)
            return true;
        else
            return false;
    }
}
