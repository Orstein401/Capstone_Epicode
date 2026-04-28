using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;
public class DialogueVariables
{
    private Dictionary<string, Ink.Runtime.Object> variables;

    public DialogueVariables()
    {
        variables = new Dictionary<string, Ink.Runtime.Object>();
    }
    public bool GetBool(string name) => GetValue(name, false);
    public int GetInt(string name) => GetValue(name, 0);

    private T GetValue<T>(string name, T defaultValue)
    {
        if (variables.ContainsKey(name))
        {
            Ink.Runtime.Value inkValue = variables[name] as Ink.Runtime.Value;
            if (inkValue.valueObject is T casted)
            {
                return casted;
            }
            else
            {
                Debug.LogError($"the type variable {name} is not of type {typeof(T)}");
            }
        }
        else
        {
            Debug.LogWarning($"there's not a variable with this name: {name}");
        }
        return defaultValue;
    }

    public void SetBool(string name, bool value)
    {
        Ink.Runtime.BoolValue boolean = GetOrCreateVariableValue<Ink.Runtime.BoolValue>(name);
        boolean.value = value;
    }
    public void SetInt(string name, int value)
    {
        Ink.Runtime.IntValue integer = GetOrCreateVariableValue<Ink.Runtime.IntValue>(name);
        integer.value = value;
    }
    private T GetOrCreateVariableValue<T>(string name) where T : Ink.Runtime.Value, new()
    {
        T variable;
        if (variables.ContainsKey(name))
        {
            variable = variables[name] as T;
        }
        else
        {
            variable = new T();
            variables[name] = variable;
        }
        return variable;
    }
    public void AddNewGlobalVariablesFromStory(Story story)
    {
        foreach (string name in story.variablesState)
        {
            if (!variables.ContainsKey(name))
            {
                Ink.Runtime.Object value = story.variablesState.GetVariableWithName(name);
                variables.Add(name, value);
               //Debug.Log(name + " " + value);
            }
        }
    }
    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariablesChange;
    }
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariablesChange;
    }
    private void VariablesChange(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables[name] = value;
        }
    }
    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
    public void FillListsWithVariables(List<string> intNames, List<int> intValues, List<string> boolNames, List<bool> boolValues)
    {
        foreach (string key in variables.Keys)
        {
            if (variables[key] is Ink.Runtime.BoolValue boolObject)
            {
                boolNames.Add(key);
                boolValues.Add(boolObject.value);
            }
            else if (variables[key] is Ink.Runtime.IntValue intObject)
            {
                intNames.Add(key);
                intValues.Add(intObject.value);
            }
        }
    }
    //public List<string> GetKeysName()
    //{
    //    return new List<string>(variables.Keys);
    //}
    //public List<object> GetValues()
    //{
    //    List<object> objs = new List<object>(variables.Values);
    //    foreach (var value in variables.Values)
    //    {
    //        object obj = (value as Ink.Runtime.Value).valueObject;
    //        objs.Add(obj);
    //    }
    //    return objs;
    //}
    //public void SetVariablesFromList(List<string> names, List<object> values)
    //{
    //    for (int i = 0; i < names.Count; i++)
    //    {
    //        variables[names[i]] = Ink.Runtime.Value.Create(values[i]);
    //    }
    //}

    public void SetBoolsFromList(List<string> names, List<bool> values)
    {
        for (int i = 0; i < names.Count; i++)
        {
            variables[names[i]] = new Ink.Runtime.BoolValue(values[i]);
        }
    }

    public void SetIntegersFromList(List<string> names, List<int> values)
    {
        for (int i = 0; i < names.Count; i++)
        {
            variables[names[i]] = new Ink.Runtime.IntValue(values[i]);
        }
    }
}
