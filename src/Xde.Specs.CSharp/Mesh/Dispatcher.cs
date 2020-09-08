using System;

namespace Xde.Mesh
{
    public class Dispatcher
    {
        public static Dispatcher<TAspect> When<TAspect>(Predicate<TAspect> predicate = null)
        {
            var type = typeof(TAspect);

            return new Dispatcher<TAspect>();
        }
    }

    public class Dispatcher<TAspect>
    {
        public Dispatcher<TAspect> Then<TDestination>(Action<TAspect, TDestination> config)
            => this
        ;
    }
}
