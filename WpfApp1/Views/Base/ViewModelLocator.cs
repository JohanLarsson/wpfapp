using System;
using Autofac;
using WpfApp1.Modules;

namespace WpfApp1.Views.Base
{
    public interface IViewModelLocator
    {
        T Get<T>();
    }

    public class ViewModelLocator : IViewModelLocator
    {
        private static IContainer _container;

        static ViewModelLocator()
        {
            if (_container != null)
                throw new InvalidOperationException($"Cannot initialize {nameof(ViewModelLocator)} multiple times");

            var builder = new ContainerBuilder();

            builder.RegisterModule(new AssemblyScanningModule());

            builder.RegisterType<ViewModelLocator>().As<IViewModelLocator>();

            _container = builder.Build();
        }

        public static T Get<T>()
        {
            return Resolve<T>();
        }

        T IViewModelLocator.Get<T>()
        {
            return Resolve<T>();
        }

        private static T Resolve<T>()
        {
            if (_container == null)
                throw new NullReferenceException($"{nameof(ViewModelLocator)} not initialized");

            return _container.Resolve<T>();
        }
    }
}
