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
                .Where(p => typeof(ICopierState).IsAssignableFrom(p) && !p.IsAbstract)
                .Select(p => (ICopierState)Activator.CreateInstance(p))
                .ToDictionary(p => p.GetType().Name);

        public ICopierState this[string stateName]
        {
            get{
                return _states[stateName];
            }
        }
        
        public void RunMachine()
        {
            var state = _states["HelloState"];
            while (true)
            {
                state = state.Handle(this);
            }
        }
    }
}
