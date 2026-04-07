using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Asteroids.Scripts.OwnPhysics
{
    public class PhysicsRouter : ITickable
    {
        private Collisions _collisions = new Collisions();

        private readonly Func<IEnumerable<Record>> _recordsProvider;

        public PhysicsRouter(CollisionsRecords collisionsRecords)
        {
            _recordsProvider = collisionsRecords.Values;
        }

        public void TryAddCollision(object modelA, object modelB)
        {
            _collisions.TryBind(modelA, modelB);
        }

        public void Tick()
        {
            foreach (var pair in _collisions.Pairs)
                TryRoute(pair);

            _collisions = new Collisions();
        }
        

        public void TryRoute((object, object) pair)
        {
            IEnumerable<Record> records = _recordsProvider?.Invoke().Where(record => record.IsTarget(pair));

            foreach (var record in records)
            {
                ((dynamic)record).Do((dynamic)pair.Item1, (dynamic)pair.Item2);
            }
        }

        private class Collisions
        {
            private List<(object, object)> _pairs = new List<(object, object)>();

            public IEnumerable<(object, object)> Pairs => _pairs;

            public void TryBind(object a, object b)
            {
                foreach (var (left, right) in _pairs)
                {
                    if (left == a && right == b)
                        return;

                    if (left == b && right == a)
                        return;
                }

                _pairs.Add((a, b));
            }
        }
    }
}