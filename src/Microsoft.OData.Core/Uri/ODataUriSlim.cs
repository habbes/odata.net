using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.OData
{
    internal sealed class ODataUriSlim : IODataUri
    {
        private ODataUri odataUri;

        public ODataUriSlim(ODataUri odataUri)
        {
            this.odataUri = odataUri;
            this.SelectAndExpand = odataUri.SelectAndExpand;
            this.Path = odataUri.Path;
        }

        public ODataUriSlim(ODataUriSlim odataUriSlim)
        {
            this.odataUri = odataUriSlim.odataUri;
            this.SelectAndExpand = odataUriSlim.SelectAndExpand;
            this.Path = odataUriSlim.Path;
        }

        public System.Uri RequestUri => this.odataUri.RequestUri;

        public System.Uri ServiceRoot => this.odataUri.ServiceRoot;

        public IDictionary<string, SingleValueNode> ParameterAliasNodes => this.odataUri.ParameterAliasNodes;

        public ODataPath Path { get; set; }

        public IEnumerable<QueryNode> CustomQueryOptions => this.odataUri.CustomQueryOptions;

        public SelectExpandClause SelectAndExpand { get; set; }

        public FilterClause Filter => this.odataUri.Filter;

        public OrderByClause OrderBy => this.odataUri.OrderBy;

        public SearchClause Search => this.odataUri.Search;

        public ApplyClause Apply => this.odataUri.Apply;

        public ComputeClause Compute => this.odataUri.Compute;

        public long? Skip => this.odataUri.Skip;

        public long? Top => this.odataUri.Top;

        public long? Index => this.odataUri.Index;

        public bool? QueryCount => this.odataUri.QueryCount;

        public string SkipToken => this.odataUri.SkipToken;

        public string DeltaToken => this.odataUri.DeltaToken;
    }
}
