using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _005_DI_Container
{
    public interface IDataAccessObject
    {
        List<string> GetFiles();
    }
}
