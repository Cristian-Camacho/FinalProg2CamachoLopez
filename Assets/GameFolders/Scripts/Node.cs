using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node : MonoBehaviour
{
    public List<Node> nextNodes;
    public bool target;
    public bool avaliable = true;
    public float radiusNeight;
    public List<Material> materials;
    private Renderer _renderer;

    void Start()
    {
        /*
        if (!target && nextNodes.Count == 0 )
            nextNodes = GetNeightbords();*/

        NodeConteiner.instance.AddToConteiner(this);
    }

    private void OnEnable()
    {
        if(!target)
            CheckDispocition();
    }

    /*
    List<Node> GetNeightbords()
    {
        List<Node> temp = new List<Node>();
        var close = Physics.OverlapSphere(transform.position, radiusNeight);

        temp = close.Where(collide => collide.gameObject != this.gameObject)
                    .Where(objet => objet.GetComponent<Node>() != null)
                    .Select(flor => flor.GetComponent<Node>())
                    .ToList();

        if (temp.Any(node => node.target))
            temp = temp.Where(node => node.target)
                       .ToList();
        return temp;
    }
    */

    public void CheckDispocition()
    {
        if (GetComponent<Renderer>() != null)
            _renderer = GetComponent<Renderer>();

        if (avaliable)
            _renderer.material = materials[0];
        else
            _renderer.material = materials[1];
    }

    private void OnGUI()
    {
        
    }
}
