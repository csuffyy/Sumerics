﻿namespace Sumerics
{
    using LightInject;
    using System;
    using System.Collections.Generic;

    public sealed class Container : IContainer
    {
        readonly ServiceContainer _block;

        public Container()
        {
            _block = new ServiceContainer();
        }

        public void Register<T>(T instance)
        {
            _block.RegisterInstance(instance);
        }

        public Object Get(Type type)
        {
            return _block.GetInstance(type);
        }

        public IEnumerable<Object> All(Type type)
        {
            return _block.GetAllInstances(type);
        }
    }
}