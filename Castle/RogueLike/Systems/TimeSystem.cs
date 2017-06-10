using System.Collections.Generic;
using System.Linq;

namespace RogueLike.Systems
{
    /// <summary>
    /// Represents timing system
    /// </summary>
    public class TimeSystem
    {
        private int time;
        private readonly SortedDictionary<int, List<Interfaces.ITimer>> objects;

        public TimeSystem()
        {
            time = 0;
            objects = new SortedDictionary<int, List<Interfaces.ITimer>>();
        }

        // Starts to follow for active objects
        public void Add(Interfaces.ITimer @object)
        {
            int key = time + @object.Time;
            if (!objects.ContainsKey(key))
            {
                objects.Add(key, new List<Interfaces.ITimer>());
            }
            objects[key].Add(@object);
        }
        
        public void Remove(Interfaces.ITimer @object)
        {
            KeyValuePair<int, List<Interfaces.ITimer>> objListFound
              = new KeyValuePair<int, List<Interfaces.ITimer>>(-1, null);

            foreach (var obj in objects)
            {
                if (obj.Value.Contains(@object))
                {
                    objListFound = obj;
                    break;
                }
            }
            if (objListFound.Value != null)
            {
                objListFound.Value.Remove(@object);
                if (objListFound.Value.Count <= 0)
                {
                    objects.Remove(objListFound.Key);
                }
            }
        }
        
        public Interfaces.ITimer Get()
        {
            var firstGroup = objects.First();
            var first = firstGroup.Value.First();
            Remove(first);
            time = firstGroup.Key;
            return first;
        }

        // Get the current time
        public int GetTime()
        {
            return time;
        }

        // Reset the time and clear out the objects
        public void Clear()
        {
            time = 0;
            objects.Clear();
        }
    }
}
