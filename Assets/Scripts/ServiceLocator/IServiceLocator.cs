// credit: https://briantria.com/ioc-service-locator-unity/#service_locator_singleton

public interface IServiceLocator
{
    static IServiceLocator Instance { get; }
    void Register<TService>(TService serviceInstance);
    TService GetService<TService>();
}