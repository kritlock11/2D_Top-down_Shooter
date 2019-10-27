using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    static Dictionary<object, object> servicecontainer = null;
    public static T GetService<T>(bool createObjectIfNotFound = true) where T : Object
    {
        if (servicecontainer == null)
            servicecontainer = new Dictionary<object, object>();

        try
        {
            if (servicecontainer.ContainsKey(typeof(T)))
            {
                T service = (T)servicecontainer[typeof(T)];
                if (service != null)
                {
                    return service;
                }
                else                
                {
                    servicecontainer.Remove(typeof(T));           
                    return FindService<T>(createObjectIfNotFound);        
                }
            }
            else
            {
                return FindService<T>(createObjectIfNotFound);
            }
        }
        catch (System.Exception)
        {
            throw new System.NotImplementedException("Can't find requested service, and create new one is set to " + createObjectIfNotFound);
        }
    }

    static T FindService<T>(bool createObjectIfNotFound = true) where T : Object
    {
        T type = GameObject.FindObjectOfType<T>();
        if (type != null)
        {
            servicecontainer.Add(typeof(T), type);
        }
        else if (createObjectIfNotFound)
        {
            GameObject go = new GameObject(typeof(T).Name, typeof(T));
            servicecontainer.Add(typeof(T), go.GetComponent<T>());
        }
        return (T)servicecontainer[typeof(T)];
    }
}
