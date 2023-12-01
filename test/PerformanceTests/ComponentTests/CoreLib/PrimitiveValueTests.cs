using BenchmarkDotNet.Attributes;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.Spatial;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.Performance.ComponentTests.CoreLib
{
    [MemoryDiagnoser]
    [ShortRunJob]
    public class PrimitiveValueTests
    {
        private static readonly byte[] byteArray = new byte[] { 1, 2, 3, 4 };
        private static readonly DateTimeOffset date = new DateTimeOffset(2010, 2, 12, 10, 10, 10, TimeSpan.Zero);
        private static readonly Guid guid = Guid.NewGuid();
        private static readonly int? nullableInt = 10;
        private static readonly ulong ulongValue = 20;
        private static readonly TimeOfDay timeOfDay = new TimeOfDay(10, 10, 10, 10);
        private static readonly GeographyPoint point = GeographyPoint.Create(10.0, 10.0);

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
        public ODataPrimitiveValue CreateGeographyPoint() => new ODataPrimitiveValue(point);


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
        public bool IsPrimitiveType_Point() => EdmLibraryExtensions.IsPrimitiveType(point.GetType());



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
        public bool IsPrimitiveTypeOptimized_Point() => EdmLibraryExtensions.IsPrimitiveTypeOptimized(point.GetType());


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

        [Benchmark]
        public bool IsPrimitiveTypeOptimized2_Point() => EdmLibraryExtensions.IsPrimitiveTypeOptimized2(point.GetType());



        [Benchmark]
        public bool IsPrimitiveTypeOptimized3_Int() => EdmLibraryExtensions.IsPrimitiveTypeOptimized3(10.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized3_String() => EdmLibraryExtensions.IsPrimitiveTypeOptimized3("what".GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized3_Bool() => EdmLibraryExtensions.IsPrimitiveTypeOptimized3(false.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized3_Date() => EdmLibraryExtensions.IsPrimitiveTypeOptimized3(date.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized3_ByteArray() => EdmLibraryExtensions.IsPrimitiveTypeOptimized3(byteArray.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized3_Guid() => EdmLibraryExtensions.IsPrimitiveTypeOptimized3(guid.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized3_NullableInt() => EdmLibraryExtensions.IsPrimitiveTypeOptimized3(nullableInt.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized3_Uint64() => EdmLibraryExtensions.IsPrimitiveTypeOptimized3(ulongValue.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized3_TimeOfDay() => EdmLibraryExtensions.IsPrimitiveTypeOptimized3(timeOfDay.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized3_Point() => EdmLibraryExtensions.IsPrimitiveTypeOptimized3(point.GetType());




        [Benchmark]
        public bool IsPrimitiveTypeOptimized4_Int() => EdmLibraryExtensions.IsPrimitiveTypeOptimized4(10.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized4_String() => EdmLibraryExtensions.IsPrimitiveTypeOptimized4("what".GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized4_Bool() => EdmLibraryExtensions.IsPrimitiveTypeOptimized4(false.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized4_Date() => EdmLibraryExtensions.IsPrimitiveTypeOptimized4(date.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized4_ByteArray() => EdmLibraryExtensions.IsPrimitiveTypeOptimized4(byteArray.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized4_Guid() => EdmLibraryExtensions.IsPrimitiveTypeOptimized4(guid.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized4_NullableInt() => EdmLibraryExtensions.IsPrimitiveTypeOptimized4(nullableInt.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized4_Uint64() => EdmLibraryExtensions.IsPrimitiveTypeOptimized4(ulongValue.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized4_TimeOfDay() => EdmLibraryExtensions.IsPrimitiveTypeOptimized4(timeOfDay.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized4_Point() => EdmLibraryExtensions.IsPrimitiveTypeOptimized4(point.GetType());




        [Benchmark]
        public bool IsPrimitiveTypeOptimized5_Int() => EdmLibraryExtensions.IsPrimitiveTypeOptimized5(10.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized5_String() => EdmLibraryExtensions.IsPrimitiveTypeOptimized5("what".GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized5_Bool() => EdmLibraryExtensions.IsPrimitiveTypeOptimized5(false.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized5_Date() => EdmLibraryExtensions.IsPrimitiveTypeOptimized5(date.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized5_ByteArray() => EdmLibraryExtensions.IsPrimitiveTypeOptimized5(byteArray.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized5_Guid() => EdmLibraryExtensions.IsPrimitiveTypeOptimized5(guid.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized5_NullableInt() => EdmLibraryExtensions.IsPrimitiveTypeOptimized5(nullableInt.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized5_Uint64() => EdmLibraryExtensions.IsPrimitiveTypeOptimized5(ulongValue.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized5_TimeOfDay() => EdmLibraryExtensions.IsPrimitiveTypeOptimized5(timeOfDay.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized5_Point() => EdmLibraryExtensions.IsPrimitiveTypeOptimized5(point.GetType());





        [Benchmark]
        public bool IsPrimitiveTypeOptimized6_Int() => EdmLibraryExtensions.IsPrimitiveTypeOptimized6(10.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized6_String() => EdmLibraryExtensions.IsPrimitiveTypeOptimized6("what".GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized6_Bool() => EdmLibraryExtensions.IsPrimitiveTypeOptimized6(false.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized6_Date() => EdmLibraryExtensions.IsPrimitiveTypeOptimized6(date.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized6_ByteArray() => EdmLibraryExtensions.IsPrimitiveTypeOptimized6(byteArray.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized6_Guid() => EdmLibraryExtensions.IsPrimitiveTypeOptimized6(guid.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized6_NullableInt() => EdmLibraryExtensions.IsPrimitiveTypeOptimized6(nullableInt.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized6_Uint64() => EdmLibraryExtensions.IsPrimitiveTypeOptimized6(ulongValue.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized6_TimeOfDay() => EdmLibraryExtensions.IsPrimitiveTypeOptimized6(timeOfDay.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized6_Point() => EdmLibraryExtensions.IsPrimitiveTypeOptimized6(point.GetType());




        [Benchmark]
        public bool IsPrimitiveTypeOptimized7_Int() => EdmLibraryExtensions.IsPrimitiveTypeOptimized7(10.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized7_String() => EdmLibraryExtensions.IsPrimitiveTypeOptimized7("what".GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized7_Bool() => EdmLibraryExtensions.IsPrimitiveTypeOptimized7(false.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized7_Date() => EdmLibraryExtensions.IsPrimitiveTypeOptimized7(date.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized7_ByteArray() => EdmLibraryExtensions.IsPrimitiveTypeOptimized7(byteArray.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized7_Guid() => EdmLibraryExtensions.IsPrimitiveTypeOptimized7(guid.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized7_NullableInt() => EdmLibraryExtensions.IsPrimitiveTypeOptimized7(nullableInt.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized7_Uint64() => EdmLibraryExtensions.IsPrimitiveTypeOptimized7(ulongValue.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized7_TimeOfDay() => EdmLibraryExtensions.IsPrimitiveTypeOptimized7(timeOfDay.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized7_Point() => EdmLibraryExtensions.IsPrimitiveTypeOptimized7(point.GetType());





        [Benchmark]
        public bool IsPrimitiveTypeOptimized8_Int() => EdmLibraryExtensions.IsPrimitiveTypeOptimized8(10.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized8_String() => EdmLibraryExtensions.IsPrimitiveTypeOptimized8("what".GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized8_Bool() => EdmLibraryExtensions.IsPrimitiveTypeOptimized8(false.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized8_Date() => EdmLibraryExtensions.IsPrimitiveTypeOptimized8(date.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized8_ByteArray() => EdmLibraryExtensions.IsPrimitiveTypeOptimized8(byteArray.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized8_Guid() => EdmLibraryExtensions.IsPrimitiveTypeOptimized8(guid.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized8_NullableInt() => EdmLibraryExtensions.IsPrimitiveTypeOptimized8(nullableInt.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized8_Uint64() => EdmLibraryExtensions.IsPrimitiveTypeOptimized8(ulongValue.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized8_TimeOfDay() => EdmLibraryExtensions.IsPrimitiveTypeOptimized8(timeOfDay.GetType());

        [Benchmark]
        public bool IsPrimitiveTypeOptimized8_Point() => EdmLibraryExtensions.IsPrimitiveTypeOptimized8(point.GetType());
    }
}
