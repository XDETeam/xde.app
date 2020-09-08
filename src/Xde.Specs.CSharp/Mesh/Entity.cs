using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Xde.Mesh
{
    public class Entity
    {
        private static long _LastId = 0;

        private ConcurrentDictionary<Type, object> _aspects = new ConcurrentDictionary<Type, object>();

        public long Id { get; init; }

        private Entity()
        {

        }

        public Entity Set<TAspect>(TAspect aspect)
        {
            if (!_aspects.TryAdd(typeof(TAspect), aspect))
            {
                throw new Exception($"Failed to add <{typeof(TAspect).Name}> to {this}");
            }

            return this;
        }

        public Entity Set<TAspect>(Action<TAspect> config = null)
            where TAspect : new()
        {
            var aspect = new TAspect();

            config?.Invoke(aspect);

            Set(aspect);

            return this;
        }

        public TAspect Get<TAspect>()
            where TAspect : class
        {
            if (_aspects.TryGetValue(typeof(TAspect), out var result))
            {
                return (TAspect)result;
            }

            return null;
        }

        /// <inheritdoc />
        public override string ToString()
            => $"Object #{Id} <{string.Join(';', _aspects.Select(aspect => aspect.Key.Name))}>"
        ;

        public static Entity Create() => new Entity
        {
            Id = Interlocked.Increment(ref _LastId)
        };
    }
}
