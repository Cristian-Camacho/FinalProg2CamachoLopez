using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ConstrucctionManager : MonoBehaviour, IUpdateable
{
    public static ConstrucctionManager instance;
    public List<GameObject> models;
    private GameObject target;
    private bool hasObject;
    private Ray _cameraRay = new Ray();
    private Node previus;

    public List<Tuple<Vector3, bool, int>> nodesTurrets;

    // Use this for initialization
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        nodesTurrets = new List<Tuple<Vector3, bool, int>>();

	}

    private void Start()
    {
        GameController.instance.AddUpdatebleConstruc(this);
        
    }

    public void Wall()
    {
        target = (GameObject)Instantiate(models[0]);
        hasObject = true;
        previus = null;
    }

    public void Minigun()
    {
        target = (GameObject)Instantiate(models[1]);
        hasObject = true;
        previus = null;
    }
    public void Frost()
    { }
    public void Heavy()
    { }

    public void UpdateMe()
    {
        if (hasObject)
        {
            _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            var temp = Physics.RaycastAll(_cameraRay)
                              .Where(impact => impact.transform.gameObject.CompareTag("Ground"))
                              .Select(donde => donde.point)
                              .First();

            if (target.CompareTag("Wall"))
            {
                var node = NodeConteiner.instance.allNodes.Where(x => x.avaliable && x != NodeConteiner.instance.TargetNode())
                                                          .OrderBy(x => Vector3.Distance(x.transform.position, temp))
                                                          .First();

                if (previus != null && previus != node && previus.avaliable)
                {
                    previus.RestoreConstruc();
                }

                node.PositionConstruc();
                previus = node;
                target.transform.position = node.transform.position + Vector3.up * 2;

                if (Input.GetMouseButtonDown(0) && target != null)
                {
                    target.transform.position = previus.transform.position;
                    GameCanvasController.instance.ReleaseConstrucction();
                    previus.avaliable = false;
                    target = null;
                    hasObject = false;
                }
                if (target.GetComponent<Wall>() != null && Input.GetMouseButtonDown(1) && target != null)
                {
                    target.GetComponent<Wall>().RotateWall();
                }
            }
            else
            {
                var node = nodesTurrets.Where(x => x.Item2)
                                       .OrderBy(x => Vector3.Distance(x.Item1, temp))
                                       .First();

                target.transform.position = node.Item1 + Vector3.up * 2;

                if (Input.GetMouseButtonDown(0) && target != null)
                {
                    target.transform.position = node.Item1;
                    GameCanvasController.instance.ReleaseConstrucction();
                    previus.avaliable = false;
                    target = null;
                    hasObject = false;
                    PlaceTurret(node.Item3);
                }
            }
        }
    }

    public void AddPointTurret(Vector3 v)
    {
        nodesTurrets.Add(Tuple.Create(v, true , v.GetHashCode()));
    }

    public void PlaceTurret(int hash)
    {
        int intData = -1;
        for (int i = 0; i < nodesTurrets.Count-1; i++)
        {
            if (nodesTurrets[i].Item3 == hash)
                intData = i;    
        }
        if(intData != -1)
            nodesTurrets.RemoveAt(intData);
    }

}
