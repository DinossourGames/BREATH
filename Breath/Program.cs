#pragma warning disable 618

using System;
using Breath.Modules;
using Breath.Systems;
using Ninject;

namespace Breath
{
    internal static class Program
    {
        private static void Main(string[] args) => new GameManager(new StandardKernel(
            new Core(),
            new ScenesModule()
            )).Run();
    }
}