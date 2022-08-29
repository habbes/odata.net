//---------------------------------------------------------------------
// <copyright file="ODataUri.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.OData.UriParser.Aggregation;
    using Microsoft.OData.UriParser;
    #endregion Namespaces

    /// <summary>
    /// The root node of a query. Holds the query itself plus additional metadata about the query.
    /// </summary>
    internal interface IODataUri
    {
        /// <summary>
        /// Gets or sets the request Uri.
        /// </summary>
        Uri RequestUri { get; }

        /// <summary>
        /// Gets or sets the service root Uri.
        /// </summary>
        Uri ServiceRoot { get; }

        /// <summary>
        /// Get the parameter alias nodes info.
        /// </summary>
        IDictionary<string, SingleValueNode> ParameterAliasNodes { get;  }

        /// <summary>
        /// Gets or sets the top level path for this uri.
        /// </summary>
        ODataPath Path { get; set; }

        /// <summary>
        /// Gets or sets any custom query options for this uri.
        /// </summary>
        IEnumerable<QueryNode> CustomQueryOptions { get; }

        /// <summary>
        /// Gets or sets any $select or $expand option for this uri.
        /// </summary>
        SelectExpandClause SelectAndExpand { get; set; }

        /// <summary>
        /// Gets or sets any $filter option for this uri.
        /// </summary>
        FilterClause Filter { get; }

        /// <summary>
        /// Gets or sets any $orderby option for this uri.
        /// </summary>
        OrderByClause OrderBy { get; }

        /// <summary>
        /// Gets or sets any $search option for this uri.
        /// </summary>
        SearchClause Search { get; }

        /// <summary>
        /// Gets or sets any $apply option for this uri.
        /// </summary>
        ApplyClause Apply { get; }

        /// <summary>
        /// Gets or sets any $compute option for this uri.
        /// </summary>
        ComputeClause Compute { get; }

        /// <summary>
        /// Gets or sets any $skip option for this uri.
        /// </summary>
        long? Skip { get; }

        /// <summary>
        /// Gets or sets any $top option for this uri.
        /// </summary>
        long? Top { get; }

        /// <summary>
        /// Gets or sets any $index option for this uri.
        /// </summary>
        long? Index { get; }

        /// <summary>
        /// Get or sets any query $count option for this uri.
        /// </summary>
        bool? QueryCount { get; }

        /// <summary>
        /// Gets or sets any $skiptoken option for this uri.
        /// </summary>
        string SkipToken { get; }

        /// <summary>
        /// Gets or sets any $deltatoken option for this uri.
        /// </summary>
        string DeltaToken { get; }

        ///// <summary>
        ///// Gets or sets the ParameterAliasValueAccessor.
        ///// </summary>
        //internal ParameterAliasValueAccessor ParameterAliasValueAccessor
        //{
        //    get
        //    {
        //        if (parameterAliasValueAccessor == null)
        //        {
        //            IDictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
        //            parameterAliasValueAccessor = new ParameterAliasValueAccessor(dictionary);
        //        }

        //        return parameterAliasValueAccessor;
        //    }
        //    set
        //    {
        //        parameterAliasValueAccessor = value;
        //    }
        //}
    }
}
