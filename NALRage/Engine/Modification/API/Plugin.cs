using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NALRage.Engine.Modification.API
{
    public abstract class Plugin : IProcessable
    {
        public abstract void Finally();
        public abstract void OnStart();
        public abstract void Process();
    }
}
