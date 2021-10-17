using System;

namespace Xde.Forms.Mesh
{
    public class When
    {
        public static When<TAspect> Is<TAspect>(Predicate<TAspect> predicate = null)
        {
            var type = typeof(TAspect);

            return new When<TAspect>();
        }

        public static When<TAspect> Has<TAspect>(Predicate<TAspect> predicate = null)
        {
            var type = typeof(TAspect);

            return new When<TAspect>();
        }
    }

    public class When<TAspect>
    {
        public When<TAspect> Then<TDestination>(Action<TAspect, TDestination> config)
            => this
        ;
    }
}
