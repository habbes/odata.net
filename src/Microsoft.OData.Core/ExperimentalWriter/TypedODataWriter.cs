using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.OData.Core.ExperimentalWriter
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0016:Add public types and members to the declared API", Justification = "<Pending>")]
    public class TypedODataWriter
    {
        IStreamBasedJsonWriterFactory jsonWriterFactory = null;
        Stream outputStream;
        IJsonWriter jsonWriter;
        IEdmModel model;
        ITypeResolver resolver;
        ODataSerializationContext context;

        public TypedODataWriter(IStreamBasedJsonWriterFactory jsonWriterFactory, Stream outputStream, IEdmModel model, ITypeResolver resolver, ODataMessageWriterSettings settings)
        {
            this.jsonWriterFactory = jsonWriterFactory;
            this.outputStream = outputStream;
            this.jsonWriter = jsonWriterFactory.CreateJsonWriter(outputStream, true, Encoding.UTF8);
            this.model = model;
            this.resolver = resolver;
            this.context = new ODataSerializationContext
            {
                Model = model,
                JsonWriter = jsonWriter,
                Resolver = resolver,
                Settings = settings
            };
        }

        public void WriterResource<T>(T resource)
        {
            var resourceWriter = context.Resolver.GetResourceWriter<T>();
            resourceWriter.Write(resource, context);
            //jsonWriter.StartObjectScope();
            //var edmType = converter.GetEdmType(model);
            //foreach (var property in edmType.Properties())
            //{
            //    var propertyWriter = converter.GetPropertyWriter(property);
            //    jsonWriter.WriteName(property.Name);
            //    propertyWriter.WriteValue(resource, this.jsonWriter);
            //}

            //jsonWriter.EndObjectScope();
        }

        public void WriteStartResourceSet(string navigationSourceName)
        {
            context.JsonWriter.StartObjectScope();
            context.JsonWriter.WriteName("@odata.context");
            context.JsonWriter.WriteValue($"{context.Settings.ODataUri.ServiceRoot}$metadata#{navigationSourceName}");
            context.JsonWriter.WriteName("value");
            context.JsonWriter.StartArrayScope();
        }
        public void WriteEndResourceSet()
        {
            context.JsonWriter.EndArrayScope();
            context.JsonWriter.EndObjectScope();
        }

        public void Flush()
        {
            context.JsonWriter.Flush();
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0016:Add public types and members to the declared API", Justification = "<Pending>")]
    public interface IResourceConverter<T>
    {
        IPropertyWriter<T> GetPropertyWriter(IEdmProperty edmProperty, IODataSerializationContext context);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0016:Add public types and members to the declared API", Justification = "<Pending>")]
    public interface IPropertyWriter<TEntity>
    {
        void WriteValue(TEntity entity, IODataSerializationContext context);
    }

    interface IValueWriter<TValue>
    {
        void Write(TValue value, IJsonWriter jsonWriter);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0016:Add public types and members to the declared API", Justification = "<Pending>")]
    public interface IResourceWriter<TResource>
    {
        void Write(TResource resource, IODataSerializationContext context);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0016:Add public types and members to the declared API", Justification = "<Pending>")]
    public class PocoResourceConverter<T> : IResourceConverter<T>
    {
        Dictionary<IEdmProperty, IPropertyWriter<T>> _cachedProperties = new Dictionary<IEdmProperty, IPropertyWriter<T>>();
        private static Type type = typeof(T);

        public virtual IPropertyWriter<T> GetPropertyWriter(IEdmProperty edmProperty, IODataSerializationContext context)
        {
            if (!_cachedProperties.TryGetValue(edmProperty, out var propertyWriter))
            {
                propertyWriter = CreatePropertyWriter(edmProperty, context);
                _cachedProperties.Add(edmProperty, propertyWriter);
            }

            return propertyWriter;
        }

        private static IPropertyWriter<T> CreatePropertyWriter(IEdmProperty edmProperty, IODataSerializationContext context)
        {
            // Rudimentary implementation of Get property. Assumes 1:1 name matching
            // Doesn't take inheritance into account
            PropertyInfo clrProperty = type.GetProperty(edmProperty.Name);
            if (edmProperty.Type.IsPrimitive() && edmProperty.Type.PrimitiveKind() == EdmPrimitiveTypeKind.Int32)
            {
                return new IntPropertyWriter<T>(clrProperty);
            }

            if (edmProperty.Type.IsPrimitive() && edmProperty.Type.PrimitiveKind() == EdmPrimitiveTypeKind.String)
            {
                return new StringPropertyWriter<T>(clrProperty);
            }

            if (edmProperty.Type.IsComplex())
            {
                Type complexPropertyWriterType = typeof(ComplexPropertyWriter<,>).MakeGenericType(type, clrProperty.PropertyType);
                return (IPropertyWriter<T>)Activator.CreateInstance(complexPropertyWriterType, clrProperty);
            }

            if (edmProperty.Type.IsCollection())
            {
                var edmElementType = edmProperty.Type.AsCollection().ElementType();
                if (edmElementType.IsPrimitive() && edmElementType.PrimitiveKind() == EdmPrimitiveTypeKind.Int32)
                {
                    return new PrimitiveCollectionPropertyWriter<T, int>(clrProperty);
                }

                if (edmElementType.IsPrimitive() && edmElementType.PrimitiveKind() == EdmPrimitiveTypeKind.String)
                {
                    return new PrimitiveCollectionPropertyWriter<T, string>(clrProperty);
                }

                if (edmElementType.IsComplex())
                {
                    // TODO: for simplicity, assume the CLR property type is IEnumerable<ElementType> and
                    // extract the ElementType
                    Type complexCollectionElementType = clrProperty.PropertyType.GetGenericArguments()[0];
                    return (IPropertyWriter<T>)Activator.CreateInstance(
                        typeof(ComplexCollectionPropertyWriter<,>)
                        .MakeGenericType(type, complexCollectionElementType),
                        clrProperty);
                        
                }
            }

            if (edmProperty.Type.IsUntyped())
            {
                return new UntypedPropertyWriter<T>(clrProperty);
            }

            throw new Exception($"Property {edmProperty.Name} has unsupported type {edmProperty.Type.FullName()}");
        }
    }

    class ResourceWriter<T> : IResourceWriter<T>
    {
        public void Write(T resource, IODataSerializationContext context)
        {
            var converter = context.Resolver.GetResourceConverter<T>();
            var jsonWriter = context.JsonWriter;
            jsonWriter.StartObjectScope();
            var edmType = context.Resolver.GetEdmType(typeof(T)) as IEdmStructuredType;
            foreach (var property in edmType.Properties())
            {
                var propertyWriter = converter.GetPropertyWriter(property, context);
                jsonWriter.WriteName(property.Name);
                propertyWriter.WriteValue(resource, context);
            }

            jsonWriter.EndObjectScope();
        }
    }

    // Does this class need to be generic?
    class IntPropertyWriter<T> : IPropertyWriter<T>
    {
        private PropertyInfo property;
#if NETCOREAPP || NET45_OR_GREATER
        private Func<T, int> getter;
#endif
        public IntPropertyWriter(PropertyInfo property)
        {
            this.property = property;
        }

        public void WriteValue(T entity, IODataSerializationContext context)
        {
            int value = this.GetValue(entity);
            IntWriter.Instance.Write(value, context.JsonWriter);
        }

        private int GetValue(T entity)
        {
#if NETCOREAPP || NET45_OR_GREATER
            return this.GetGetter()(entity);
#else
            // Apparently DynamicMethods is not defined in .netstandard
            return (int)property.GetValue(entity);
#endif
        }

#if NETCOREAPP || NET45_OR_GREATER
        private Func<T, int> GetGetter()
        {
            if (this.getter == null)
            {
                this.getter = CreateGetter();
            }

            return this.getter;
        }

        private Func<T, int> CreateGetter()
        {
            // Generate and cache a getter method dynamically for retrieving the property values
            // more efficiently
            // If we call PropertyInfo.GetValue(), this will box the values if they're structs
            MethodInfo realMethod = property.GetMethod;
            Debug.Assert(realMethod != null);

            Type[] argTypes = new Type[] { typeof(T) };
            DynamicMethod dynamicMethod = new DynamicMethod("", typeof(int), argTypes);

            // TODO: For simplicity, assuming T is a class/reference type and no boxing/unboxing is necessary
            ILGenerator generator = dynamicMethod.GetILGenerator();

            generator.Emit(OpCodes.Ldarg_0); // load the resource
            generator.Emit(OpCodes.Callvirt, realMethod);
            generator.Emit(OpCodes.Ret);

            return (Func<T, int>)dynamicMethod.CreateDelegate(typeof(Func<T, int>));
        }
#endif
    }

    class StringPropertyWriter<T> : IPropertyWriter<T>
    {
        private PropertyInfo property;

#if NETCOREAPP || NET45_OR_GREATER
        Func<T, string> getter;
#endif

        public StringPropertyWriter(PropertyInfo property)
        {
            this.property = property;
        }

        public void WriteValue(T entity, IODataSerializationContext context)
        {
            string value = GetValue(entity);
            StringWriter.Instance.Write(value, context.JsonWriter);
        }

        private string GetValue(T entity)
        {
#if NETCOREAPP || NET45_OR_GREATER
            return this.GetGetter()(entity);
#else
            // Apparently DynamicMethods is not defined in .netstandard
            return (string)property.GetValue(entity);
#endif
        }

#if NETCOREAPP || NET45_OR_GREATER
        private Func<T, string> GetGetter()
        {
            if (this.getter == null)
            {
                this.getter = CreateGetter();
            }

            return this.getter;
        }

        private Func<T, string> CreateGetter()
        {
            // Generate and cache a getter method dynamically for retrieving the property values
            // more efficiently
            // If we call PropertyInfo.GetValue(), this will box the values if they're structs
            MethodInfo realMethod = property.GetMethod;
            Debug.Assert(realMethod != null);

            Type[] argTypes = new Type[] { typeof(T) };
            DynamicMethod dynamicMethod = new DynamicMethod("", typeof(string), argTypes);

            // TODO: For simplicity, assuming T is a class/reference type and no boxing/unboxing is necessary
            ILGenerator generator = dynamicMethod.GetILGenerator();

            generator.Emit(OpCodes.Ldarg_0); // load the resource
            generator.Emit(OpCodes.Callvirt, realMethod);
            generator.Emit(OpCodes.Ret);

            return (Func<T, string>)dynamicMethod.CreateDelegate(typeof(Func<T, string>));
        }
#endif
    }

    class PrimitiveCollectionPropertyWriter<TResource, TElement> : PropertyWriter<TResource>
    {
        public PrimitiveCollectionPropertyWriter(PropertyInfo property) : base(property)
        {
        }

        public override void WriteValue(TResource entity, IODataSerializationContext context)
        {
            IEnumerable<TElement> enumerable = (IEnumerable<TElement>)Property.GetValue(entity);
            var jsonWriter = context.JsonWriter;
            jsonWriter.StartArrayScope();
            foreach (TElement element in enumerable)
            {
                IValueWriter<TElement> valueWriter = Helpers.GetValueWriter<TElement>();
                valueWriter.Write(element, jsonWriter);
            }

            jsonWriter.EndArrayScope();
        }
    }

    class ComplexPropertyWriter<TResource, TProperty> : PropertyWriter<TResource>
    {
        public ComplexPropertyWriter(PropertyInfo property) : base(property)
        {
        }

        public override void WriteValue(TResource resource, IODataSerializationContext context)
        {
            var value = (TProperty)Property.GetValue(resource); // use reflection emit to avoid boxing
            var resourceWriter = context.Resolver.GetResourceWriter<TProperty>();
            resourceWriter.Write(value, context);
        }
    }

    class ComplexCollectionPropertyWriter<TResource, TElement> : PropertyWriter<TResource>
    {
        public ComplexCollectionPropertyWriter(PropertyInfo property) : base(property)
        {
        }

        public override void WriteValue(TResource resource, IODataSerializationContext context)
        {
            IEnumerable<TElement> enumerable = (IEnumerable<TElement>)Property.GetValue(resource);
            context.JsonWriter.StartArrayScope();
            var itemWriter = context.Resolver.GetResourceWriter<TElement>();

            foreach (TElement item in enumerable)
            {
                itemWriter.Write(item, context);
            }

            context.JsonWriter.EndArrayScope();
        }
    }

    class UntypedPropertyWriter<TResource> : PropertyWriter<TResource>
    {
        public UntypedPropertyWriter(PropertyInfo property) : base(property)
        {
        }

        public override void WriteValue(TResource resource, IODataSerializationContext context)
        {
            object value = Property.GetValue(resource);
            // TODO: need to figure out how to let the user control untyped value serialization
            // I add quotes cause I know the sample data does not enclose the value in quotes
            // this causes string concatenations/allocations, so definitely not ideal
            //context.JsonWriter.WriteRawValue($"\"{value}\"");
        }
    }

    abstract class PropertyWriter<T> : IPropertyWriter<T>
    {
        protected PropertyInfo Property { get; private set; }

        public PropertyWriter(PropertyInfo property)
        {
            this.Property = property;
        }

        public abstract void WriteValue(T resource, IODataSerializationContext context);
    }

    class IntWriter : IValueWriter<int>
    {
        public static IntWriter Instance = new IntWriter();
        public void Write(int value, IJsonWriter jsonWriter)
        {
            jsonWriter.WriteValue(value);
        }
    }

    class StringWriter : IValueWriter<string>
    {
        public static StringWriter Instance = new StringWriter();

        public void Write(string value, IJsonWriter jsonWriter)
        {
            jsonWriter.WriteValue(value);
        }
    }

    internal static class Helpers
    {
        public static IValueWriter<T> GetValueWriter<T>()
        {
            Type targetType = typeof(T);

            if (targetType == typeof(int))
            {
                return IntWriter.Instance as IValueWriter<T>;
            }
            else if (targetType == typeof(string))
            {
                return StringWriter.Instance as IValueWriter<T>;
            }

            throw new Exception($"Could not resolve value writer for type {targetType.FullName}");
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0016:Add public types and members to the declared API", Justification = "<Pending>")]
    public interface ITypeResolver
    {
        IResourceConverter<T> GetResourceConverter<T>();
        IResourceWriter<T> GetResourceWriter<T>();
        Type GetClrType(IEdmType edmType);
        IEdmType GetEdmType(Type clrType);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0016:Add public types and members to the declared API", Justification = "<Pending>")]
    public interface IODataSerializationContext
    {
        ITypeResolver Resolver { get; }
        IEdmModel Model { get; }
        IJsonWriter JsonWriter { get; }
        ODataMessageWriterSettings Settings { get; }
        
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0016:Add public types and members to the declared API", Justification = "<Pending>")]
    public class ODataSerializationContext : IODataSerializationContext
    {
        public ITypeResolver Resolver { get; internal set; }
        public IEdmModel Model { get; internal set; }
        public IJsonWriter JsonWriter { get; internal set; }
        public ODataMessageWriterSettings Settings { get; internal set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0016:Add public types and members to the declared API", Justification = "<Pending>")]
    public class DefaultTypeResolver : ITypeResolver
    {
        private ConcurrentDictionary<IEdmType, Type> edmToClrTypeCache = new ConcurrentDictionary<IEdmType, Type>();
        private ConcurrentDictionary<Type, IEdmType> clrToEdmTypeCache = new ConcurrentDictionary<Type, IEdmType>();
        // TODO: instead of using object as the value type, create a non-generic IResourceConverter interface instead
        private ConcurrentDictionary<Type, object> resourceConverterCache = new ConcurrentDictionary<Type, object>();
        // TODO: instead of using object as the value type, create a non-generic IResourceWriter interface instead
        private ConcurrentDictionary<Type, object> resourceWriterCache = new ConcurrentDictionary<Type, object>();
        public virtual Type GetClrType(IEdmType edmType)
        {
            if (!edmToClrTypeCache.TryGetValue(edmType, out Type clrType))
            {
                throw new Exception($"Could not resolve CLR type corresponding to {edmType.FullTypeName()}");
            }

            return clrType;
        }

        public void MapEdmToClrType(IEdmType edmType, Type clrType)
        {
            // assume you can't change mappings
            edmToClrTypeCache.TryAdd(edmType, clrType);
            clrToEdmTypeCache.TryAdd(clrType, edmType);
        }

        public virtual IResourceConverter<T> GetResourceConverter<T>()
        {
            return (IResourceConverter<T>)resourceConverterCache.GetOrAdd(typeof(T), _ => new PocoResourceConverter<T>());
        }

        public virtual IResourceWriter<T> GetResourceWriter<T>()
        {
            return (IResourceWriter<T>)resourceWriterCache.GetOrAdd(typeof(T), _ => new ResourceWriter<T>());
        }

        public IEdmType GetEdmType(Type clrType)
        {
            if (!clrToEdmTypeCache.TryGetValue(clrType, out IEdmType edmType))
            {
                throw new Exception($"Could not resolve EDM type corresponding to {clrType.FullName}");
            }

            return edmType;
        }
    }
}
