using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GOAP
{

    public static IEnumerable<Tuple<State, GOAPAction<State>>> Run<State>
        (
            State initialState,
            Func<State, bool> goal,
            IEnumerable<GOAPAction<State>> actions,
            Func<State, float> heuristic
        )
    {
        var initialNode = Tuple.Create(initialState, default(GOAPAction<State>));

        Func<Tuple<State, GOAPAction<State>>, bool> satisfies = node =>
         {
             return goal(node.Item1);
         };

        Func<Tuple<State, GOAPAction<State>>, float> goapHeuristic = node =>
        {
            return heuristic(node.Item1);
        };

        Func<Tuple<State, GOAPAction<State>>, IEnumerable<Tuple<Tuple<State, GOAPAction<State>>, float>>> expand = node =>
        {
            var states = new List<Tuple<Tuple<State, GOAPAction<State>>, float>>();

            foreach (var action in actions)
            {
                if (action.condition(node.Item1))
                {
                    states.Add(Tuple.Create(Tuple.Create(action.effect(node.Item1), action), action.cost));
                }
            }
            return states;
        };

        return AStar.Run(initialNode, satisfies, expand, goapHeuristic);
    }

}
