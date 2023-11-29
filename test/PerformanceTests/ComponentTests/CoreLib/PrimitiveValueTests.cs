using BenchmarkDotNet.Attributes;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.Performance.ComponentTests.CoreLib
{
    [MemoryDiagnoser]
    public class PrimitiveValueTests
    {
        private static readonly byte[] byteArray = new byte[] { 1, 2, 3, 4 };
        private static readonly DateTimeOffset date = new DateTimeOffset(2010, 2, 12, 10, 10, 10, TimeSpan.Zero);
        private static readonly Guid guid = Guid.NewGuid();
        private static readonly int? nullableInt = 10;
        private static readonly ulong ulongValue = 20;
        private static readonly TimeOfDay timeOfDay = new TimeOfDay(10, 10, 10, 10);

        [Benchmark]
        public ODataPrimitiveValue CreateIntValue()
        {
            return new ODataPrimitiveValue(10);
        }

        [Benchmark]
        public ODataPrimitiveValue CreateBoolValue()
        {
            return new ODataPrimitiveValue(true);
        }

        [Benchmark]
        public ODataPrimitiveValue CreateStringValue()
        {
            return new ODataPrimitiveValue("what");
        }

        [Benchmark]
        public ODataPrimitiveValue CreateDateValue()
        {
            return new ODataPrimitiveValue(date);
        }

        [Benchmark]
        public ODataPrimitiveValue CreateByteArrayValue()
        {
            return new ODataPrimitiveValue(byteArray);
        }

        [Benchmark]
        public ODataPrimitiveValue CreateGuidValue()
        {
            return new ODataPrimitiveValue(guid);
        }

        [Benchmark]
        public ODataPrimitiveValue CreateNullableIntValue() => new ODataPrimitiveValue(nullableInt);

        [Benchmark]
        public ODataPrimitiveValue CreateUint64Value() => new ODataPrimitiveValue(ulongValue);

        [Benchmark]
        public ODataPrimitiveValue CreateTimeOfDayValue() => new ODataPrimitiveValue(timeOfDay);


        [Benchmark]
        public bool IsPrimitiveType_Int() => EdmLibraryExtensions.IsPrimitiveType(10.GetType());

        [Benchmark]
        public bool IsPrimitiveType_String() => EdmLibraryExtensions.IsPrimitiveType("what".GetType());

        [Benchmark]
        public bool IsPrimitiveType_Bool() => EdmLibraryExtensions.IsPrimitiveType(false.GetType());

        [Benchmark]
        public bool IsPrimitiveType_Date() => EdmLibraryExtensions.IsPrimitiveType(date.GetType());

        [Benchmark]
        public bool IsPrimitiveType_ByteArray() => EdmLibraryExtensions.IsPrimitiveType(byteArray.GetType());

        [Benchmark]
        public bool IsPrimitiveType_Guid() => EdmLibraryExtensions.IsPrimitiveType(guid.GetType());

        [Benchmark]
        public bool IsPrimitiveType_NullableInt() => EdmLibraryExtensions.IsPrimitiveType(nullableInt.GetType());

        [Benchmark]
        public bool IsPrimitiveType_Uint64() => EdmLibraryExtensions.IsPrimitiveType(ulongValue.GetType());

        [Benchmark]
        public bool IsPrimitiveType_TimeOfDay() => EdmLibraryExtensions.IsPrimitiveType(timeOfDay.GetType());



        [Benchmark]
        public bool IsPrimitiveTypeOptimized_Int() => EdmLibraryExtensions.IsPrimitiveTypeOptimized(10.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized_String() => EdmLibraryExtensions.IsPrimitiveTypeOptimized("what".GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized_Bool() => EdmLibraryExtensions.IsPrimitiveTypeOptimized(false.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized_Date() => EdmLibraryExtensions.IsPrimitiveTypeOptimized(date.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized_ByteArray() => EdmLibraryExtensions.IsPrimitiveTypeOptimized(byteArray.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized_Guid() => EdmLibraryExtensions.IsPrimitiveTypeOptimized(guid.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized_NullableInt() => EdmLibraryExtensions.IsPrimitiveTypeOptimized(nullableInt.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized_Uint64() => EdmLibraryExtensions.IsPrimitiveTypeOptimized(ulongValue.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized_TimeOfDay() => EdmLibraryExtensions.IsPrimitiveTypeOptimized(timeOfDay.GetType());


        [Benchmark]
        public bool IsPrimitiveTypeOptimized2_Int() => EdmLibraryExtensions.IsPrimitiveTypeOptimized2(10.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized2_String() => EdmLibraryExtensions.IsPrimitiveTypeOptimized2("what".GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized2_Bool() => EdmLibraryExtensions.IsPrimitiveTypeOptimized2(false.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized2_Date() => EdmLibraryExtensions.IsPrimitiveTypeOptimized2(date.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized2_ByteArray() => EdmLibraryExtensions.IsPrimitiveTypeOptimized2(byteArray.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized2_Guid() => EdmLibraryExtensions.IsPrimitiveTypeOptimized2(guid.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized2_NullableInt() => EdmLibraryExtensions.IsPrimitiveTypeOptimized2(nullableInt.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized2_Uint64() => EdmLibraryExtensions.IsPrimitiveTypeOptimized2(ulongValue.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized2_TimeOfDay() => EdmLibraryExtensions.IsPrimitiveTypeOptimized2(timeOfDay.GetType());
    }
}
