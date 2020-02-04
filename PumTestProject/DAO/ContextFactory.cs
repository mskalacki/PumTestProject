using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.DAO
{
    public class ContextFactory : IContextFactory
    {
        public PumContext CreateContext()
        {
            return new PumContext();
        }
    }
}
