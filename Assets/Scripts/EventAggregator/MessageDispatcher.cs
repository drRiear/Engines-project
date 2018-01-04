using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class MessageDispatcher
{
    public static readonly List<Component> _Listeners = new List<Component>();

    public static void Send<T>(T message) where T : class
    {
        foreach (var component in _Listeners)
            Send(message, component);
    }

    public static void Send<T>(T message, Component component)
    {
        if (component == null)
            return;

        var type = component.GetType();
        var methods = type.GetMethods(
            BindingFlags.Instance | BindingFlags.NonPublic |
            BindingFlags.Public | BindingFlags.Static);

        foreach (var method in methods)
        {
            var parameters = method.GetParameters();
            if (parameters.Length != 1)
                continue;

            var param = parameters[0];
            if (param.ParameterType == message.GetType())
                method.Invoke(component, new object[] { message });
        }
    }

    public static void AddListener(Component component)
    {
        if (_Listeners.Contains(component)) return;
        _Listeners.Add(component);
    }
    public static void RemoveListener(Component component)
    {
        if (!_Listeners.Contains(component)) return;
        _Listeners.Remove(component);
    }

}
