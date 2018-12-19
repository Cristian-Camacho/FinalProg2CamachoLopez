using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BomberEnemy : Entidad
{
    private Animator animator;
    private Node target;
    private Queue<Node> path;
    public float range;
    public float damage;
    public GameObject explodeFeed;
    public GameObject dieFeed;


    /*    private EventFSM<StatesTake> _eventFSM;

        State<StatesTake> movement = new State<StatesTake>("Walk");
        State<StatesTake> attackCore = new State<StatesTake>("CoreAttack");
        State<StatesTake> attackWall = new State<StatesTake>("WallAttack");
        State<StatesTake> attackPlayer = new State<StatesTake>("PlayerAttack");
        */
    public override void Start()
    {
        base.Start();
        speed += UnityEngine.Random.Range(0, speed);
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", true);
        var nodes = NodeConteiner.instance.allNodes;
        target = nodes.Where(x => x.avaliable)
                      .OrderBy(node => Vector3.Distance(transform.position, node.transform.position))
                      .First();


    }

    private void RunGOAP()
    {
        var actions = new List<GOAPAction<BomberEnemy>>();

        actions.Add(new GOAPAction<BomberEnemy>
            (
                "GoCore",
                1f,
                bomber => bomber.currentLife > bomber.maxLife / 2,
                bomber =>
                {
                    BomberEnemy bomberEnemy = this;

                    var tempPath = LazyAStar.Run(target, IsTarget, NeightAndDanger, DistanceToCoreNode).ToList();
                    for (int i = 0; i < tempPath.Count - 1; i++)
                    {
                        bomberEnemy.path.Enqueue(tempPath[i].Item2);
                    }
                    return bomberEnemy;
                }

            ));

        actions.Add(new GOAPAction<BomberEnemy>
           (
               "AttackCore",
               0.5f,
               bomber => bomber.target.nextNodes.First() == NodeConteiner.instance.TargetNode(),
               bomber =>
               {
                   BomberEnemy bomberEnemy = this;
                   bomberEnemy.target = NodeConteiner.instance.TargetNode();
                   return bomberEnemy;
               }

           ));

        GOAP.Run(this, x => x.target = NodeConteiner.instance.TargetNode(),
                    actions, Heuristic);
    }

    private bool IsTarget(Node n)
    {
        return n.target;
    }

    private float Heuristic(BomberEnemy bomber)
    {
        float dist = Vector3.Distance(bomber.transform.position, NodeConteiner.instance.TargetNode().transform.position);
        return dist;

    }

    private float CostOfPathTo(List<Node> nodes, Vector3 targetPos)
    {
        float finalCost = 0;
        var targetNode = nodes.OrderBy(x => Vector3.Distance(x.transform.position, targetPos)).First();
        var pathTo = new List<Node>();
        pathTo = AStar.Run(nodes.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First(),
                               n => n == targetNode, NeightAndDanger, DistanceToCoreNode);

        foreach (var item in pathTo)
        {
            finalCost += item.securityLvl;
        }

        return finalCost;
    }

    private float DistanceToCoreNode(Node n)
    {
        var dist = Vector3.Distance(NodeConteiner.instance.TargetNode().transform.position, n.transform.position);
        return dist;
    }


    private List<Tuple<Node, float>> NeightAndDanger(Node node)
    {
        var vecinos = new List<Tuple<Node, float>>();


        foreach (var vecino in node.nextNodes.Where(n => n.avaliable))
        {
            vecinos.Add(Tuple.Create(vecino, vecino.securityLvl));
        }

        return vecinos;
    }
    
    
    public override void UpdateMe()
    {
        var direction = target.transform.position - transform.position;
        transform.position += direction.normalized * Time.deltaTime * speed;

        if (Vector3.Distance(transform.position, target.transform.position) < range && !target.target)
        {
            if (path == null)
                StartCoroutine(GOAPThinking());
            else if (path.Count > 0)
                target = path.Dequeue();
        }

        if (target == NodeConteiner.instance.TargetNode() && Vector3.Distance(transform.position, target.transform.position) < range)
        {
            Explote();
        }

    }

    IEnumerator GOAPThinking()
    {
        path = new Queue<Node>();
        RunGOAP();
        yield return new WaitForSeconds(60);

    }

    void Explote()
    {
        StopCoroutine(GOAPThinking());
        GameController.instance.GetGameCore().TakeHit(damage);
        Feedback(explodeFeed, transform.position, Quaternion.identity, 0);
        EnemyController.instance.MultiplyEnemies();
        RemoveMe();
        Destroy(this.gameObject);
    }

    public override void Die()
    {
        GameController.instance.AddResources(50f);
        EnemyController.instance.MultiplyEnemies();
        Feedback(dieFeed, transform.position, Quaternion.identity, 0);
        Destroy(this.gameObject);
        RemoveMe();
    }
}
