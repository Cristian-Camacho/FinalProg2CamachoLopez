using System.Collections.Generic;
using System;

public static class LazyAStar
{
    public static IEnumerable<Tuple<bool, Node, IEnumerable<Node>>> Run<Node>
        (
            Node start,
            Func<Node, bool> satisfies,
            Func<Node, List<Tuple<Node, float>>> expand,
            Func<Node, float> heuristic
        )
    {
        var open = new PriorityQueue<Node>();
        open.Enqueue(start, 0);
        
        var closed = new HashSet<Node>();
        
        var parents = new Dictionary<Node, Node>();
        
        var costs = new Dictionary<Node, float>();
        costs[start] = 0;

        while (!open.IsEmpty)
        {
            var current = open.Dequeue();  
            
            yield return Tuple.Create(
                                        satisfies(current),
                                        current,
                                        EnumerableUtils.GeneratePath(current, parents)
                                     );

            var currentCost = costs[current];  
            closed.Add(current);
            
            foreach (var childPair in expand(current))
            {
                var child = childPair.Item1;
                var childCost = childPair.Item2;
                
                if (closed.Contains(child)) continue;

                var tentativeCost = currentCost + childCost;
                if (costs.ContainsKey(child) && tentativeCost > costs[child]) continue;

                parents[child] = current;

                costs[child] = tentativeCost;
                open.Enqueue(child, tentativeCost + heuristic(child));
            }

        }
        
    }

}