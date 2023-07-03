// credit: https://briantria.com/ioc-service-locator-unity/#service_locator_singleton

using System;
using System.Collections.Generic;

public sealed class ServiceLocator : IServiceLocator
{
    private ServiceLocator() { }

    private static readonly Lazy<IServiceLocator> _instance = new Lazy<IServiceLocator>(() =>
    {
        return new ServiceLocator();
    });

    public static IServiceLocator Instance
    {
        get { return _instance.Value; }
    }


    private readonly Dictionary<Type, object> _serviceRegistry = new Dictionary<Type, object>();

    public void Register<TService>(TService serviceInstance)
    {
        Type serviceType = typeof(TService);

        if (_serviceRegistry.ContainsKey(serviceType))
        {
            _serviceRegistry[serviceType] = serviceInstance;
            return;
        }

        _serviceRegistry.Add(serviceType, serviceInstance);
    }

    public TService GetService<TService>()
    {
        Type serviceType = typeof(TService);
        if (_serviceRegistry.ContainsKey(serviceType))
        {
            return (TService)_serviceRegistry[serviceType];
        }

        throw new Exception($"{serviceType} is unregistered.");
    }
}
