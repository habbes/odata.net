using Microsoft.OData.Core.ExperimentalWriter;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentsLib
{
    internal class ExperimentalWriterTypeResolver : DefaultTypeResolver
    {
        public ExperimentalWriterTypeResolver(IEdmModel model)
        {
            MapEdmToClrType(model.FindDeclaredType("NS.Customer"), typeof(Customer));
            MapEdmToClrType(model.FindDeclaredType("NS.Address"), typeof(Address));
        }
    }
}
