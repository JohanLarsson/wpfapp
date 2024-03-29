﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using WpfApp1.Views.Base;

namespace WpfApp1.Modules
{
    public class AssemblyScanningModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ScanAssemblies(builder);

            base.Load(builder);
        }

        private static void ScanAssemblies(ContainerBuilder builder)
        {
            var assemblies = new List<Assembly>();

            assemblies.Add(Assembly.GetEntryAssembly());

            assemblies.AddRange(Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "Pcm.*.dll", SearchOption.AllDirectories)
                .Select(Assembly.LoadFrom));

            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.IsAbstract == false)
                    .Where(t => t.IsSubclassOf(typeof(ViewModelBase)))
                    .Where(t => t.Name.EndsWith("ViewModel"))
                    .AsSelf();

                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.IsAbstract == false)
                    .Where(t => t.IsSubclassOf(typeof(Window)))
                    .AsSelf();

                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.IsClass && t.GetInterfaces().Any(d => d.Name.EndsWith(t.Name)))
                    .As(t => t.GetInterfaces().First(i => i.Name.EndsWith(t.Name)));
            }
        }
    }
}