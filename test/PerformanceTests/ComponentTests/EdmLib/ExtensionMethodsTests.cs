using BenchmarkDotNet.Attributes;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.Performance.ComponentTests.EdmLib
{
    [MemoryDiagnoser]
    public class ExtensionMethodsTests
    {
        private static readonly IEdmModel Model = TestUtils.GetTripPinModel(markAsImmutable: true);
        private static readonly IEdmEntitySet entitySet = Model.FindDeclaredEntitySet("People");
        private static readonly IEdmSingleton singleton = Model.FindDeclaredSingleton("Me");
        private static readonly IEdmModel edmModel = CreateModel();
        private static readonly IEdmEntitySet edmEntitySet = edmModel.FindDeclaredEntitySet("People");
        private static readonly IEdmSingleton edmSingleton = Model.FindDeclaredSingleton("Me");

        private static IEdmModel CreateModel()
        {
            var model = new EdmModel();
            var personType = model.AddEntityType("NS", "Person");
            personType.AddKeys(personType.AddStructuralProperty("Id", EdmPrimitiveTypeKind.String));
            var container = model.AddEntityContainer("NS", "Container");
            container.AddEntitySet("People", personType);
            container.AddSingleton("Me", personType);
            return model;
        }

        [Benchmark]
        public IEdmEntityType EntityType_WithEntitySet()
        {
            return entitySet.EntityType();
        }

        [Benchmark]
        public IEdmEntityType EntityType_WithSingleton()
        {
            return singleton.EntityType();
        }

        [Benchmark]
        public IEdmEntityType EntityTypeOptimizied_WithEntitySet()
        {
            return entitySet.EntityTypeOptimized();
        }

        [Benchmark]
        public IEdmEntityType EntityTypeOptimized_WithSingleton()
        {
            return singleton.EntityTypeOptimized();
        }

        [Benchmark]
        public IEdmEntityType EntityTypeOptimizied2_WithEntitySet()
        {
            return entitySet.EntityTypeOptimized2();
        }

        [Benchmark]
        public IEdmEntityType EntityTypeOptimized2_WithSingleton()
        {
            return singleton.EntityTypeOptimized2();
        }

        [Benchmark]
        public IEdmEntityType EntityType_WithEdmEntitySet()
        {
            return edmEntitySet.EntityType();
        }

        [Benchmark]
        public IEdmEntityType EntityType_WithEdmSingleton()
        {
            return edmSingleton.EntityType();
        }

        [Benchmark]
        public IEdmEntityType EntityTypeOptimizied_WithEdmEntitySet()
        {
            return edmEntitySet.EntityTypeOptimized();
        }

        [Benchmark]
        public IEdmEntityType EntityTypeOptimized_WithEdmSingleton()
        {
            return edmSingleton.EntityTypeOptimized();
        }

        [Benchmark]
        public IEdmEntityType EntityTypeOptimizied2_WithEdmEntitySet()
        {
            return edmEntitySet.EntityTypeOptimized2();
        }

        [Benchmark]
        public IEdmEntityType EntityTypeOptimized2_WithEdmSingleton()
        {
            return edmSingleton.EntityTypeOptimized2();
        }
    }
}
