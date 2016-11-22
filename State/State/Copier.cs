using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace State
{
    public class Copier
    {
        public const int priceCopy = 10;

        public int Cash {get; set;}

        public string Document { get; set; }

        private readonly Dictionary<string, ICopierState> _states = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(ICopierState).IsAssignableFrom(p) && !p.IsInterface)
                .Select(p => (ICopierState)Activator.CreateInstance(p))
                .ToDictionary(p => p.GetType().Name.Replace("State", "").ToLower());

        public ICopierState this[string stateName]
        {
            get{
                return _states[stateName.ToLower()];
            }
        }
        
        public void RunMachine()
        {
            var state = _states["hello"];
            while (true)
            {
                state = state.Handle(this);
            }
        }
    }
}
