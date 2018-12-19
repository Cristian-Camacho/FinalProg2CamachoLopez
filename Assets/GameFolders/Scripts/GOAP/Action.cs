using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPAction<State> {

    public string Name;

    public float cost;
    //public Func<State, float> cost;

    public Func<State, bool> condition;
    public Func<State, State> effect;

    public GOAPAction(string name,
        float cost,
        Func<State, bool> condition,
        Func<State, State> effect)
    {
        Name = name;
        this.cost = cost;
        this.condition = condition;
        this.effect = effect;
    }
}
