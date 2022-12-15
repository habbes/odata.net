using Microsoft.OData;
using Microsoft.OData.Core.ExperimentalWriter;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentsLib
{
    internal class ExperimentalNewWriterPayloadWriter : IPayloadWriter<IEnumerable<Customer>>
    {
        IStreamBasedJsonWriterFactory jsonWriterFactory;
        IEdmModel model;
        ITypeResolver resolver;

        public ExperimentalNewWriterPayloadWriter(IEdmModel model, IStreamBasedJsonWriterFactory jsonWriterFactory, ITypeResolver resolver)
        {
            this.jsonWriterFactory = jsonWriterFactory;
            this.model = model;
            this.resolver = resolver;
        }

        public Task WritePayloadAsync(IEnumerable<Customer> payload, Stream stream, bool includeRawValues = false)
        {
            var settings = new ODataMessageWriterSettings();

            settings.ODataUri = new ODataUri
            {
                ServiceRoot = new Uri("https://services.odata.org/V4/OData/OData.svc/"),

            };

            var writer = new TypedODataWriter(this.jsonWriterFactory, stream, model, this.resolver, settings);
            writer.WriteStartResourceSet("Customers");

            foreach (var customer in payload)
            {
                writer.WriterResource(customer);
            }

            writer.WriteEndResourceSet();

            writer.Flush();

            return Task.CompletedTask;
        }
    }
}
