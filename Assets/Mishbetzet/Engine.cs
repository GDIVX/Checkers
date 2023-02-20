using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishbetzet
{
    public abstract class Engine
    {
        public abstract void Run();
        public abstract bool IsRunning { get; }
        public abstract void Stop();
        
    }
}
