//#define DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _007_FactoryInjection
{
    public class DataAccessFactory
    {
        private static IDataAccessObject dataAcessObject;

        internal static IDataAccessObject Create()
        {
            if (dataAcessObject != null)
            {
                return dataAcessObject;
            }

            return new FileDataObject();
        }

        [Conditional("DEBUG")]
        public static void SetDataAccessObject(IDataAccessObject customDataObject)
        {
            dataAcessObject = customDataObject;
        }
    }
}
