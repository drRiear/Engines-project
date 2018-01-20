using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public static class MessageDispatcher
{
    public static readonly List<Component> Listeners = new List<Component>();

    private static bool listIsBusy = false;

    public static void Send<T>(T message)
    {
        if (Listeners == null || Listeners.Count < 1) return;

        listIsBusy = true;
        foreach (var listener in Listeners)
            Send(message, listener);
    }

    public static void Send<T>(T message, Component listener)
    {
        if (listener == null)
            return;

        var type = listener.GetType();
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
                method.Invoke(listener, new object[] {message});
        }

        listIsBusy = false;
    }

    public static void AddListener(Component component)
    {
        if (Listeners.Contains(component) && listIsBusy) return;
        listIsBusy = true;
        Listeners.Add(component);
        listIsBusy = false;
    }

    public static void RemoveListener(Component component)
    {
        if (!Listeners.Contains(component)) return;
        Listeners.Remove(component);
    }

    private static void WaitAndRepeat()
    {
    }
}
