//---------------------------------------------------------------------
// <copyright file="IEdmEntitySetBase.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.Edm
{
    /// <summary>
    /// Represents an EDM base entity set.
    /// </summary>
    public interface IEdmEntitySetBase : IEdmNavigationSource
    {
    }

    /// <summary>
    /// 
    /// </summary>
#pragma warning disable RS0016 // Add public types and members to the declared API
    public interface IHasEntitytype
#pragma warning restore RS0016 // Add public types and members to the declared API
    {
#pragma warning disable RS0016 // Add public types and members to the declared API
        IEdmEntityType EntityType { get; }
#pragma warning restore RS0016 // Add public types and members to the declared API
    }
}
