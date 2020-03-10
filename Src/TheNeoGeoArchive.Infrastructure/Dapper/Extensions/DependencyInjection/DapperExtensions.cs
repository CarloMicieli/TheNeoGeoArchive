using System;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using static Dapper.SqlMapper;

namespace TheNeoGeoArchive.Infrastructure.Dapper.Extensions.DependencyInjection
{
public static class DapperExtensions
    {
        public static IServiceCollection ReplaceDapper(this IServiceCollection services, Action<DapperOptions> action)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDatabaseContext));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var options = new DapperOptions(services);
            action?.Invoke(options);

            return services;
        }

        public static IServiceCollection AddDapper(this IServiceCollection services, Action<DapperOptions> action)
        {
            var options = new DapperOptions(services);
            action?.Invoke(options);

            Type baseType = typeof(ITypeHandler);

            var typeHandlers = options.Assemblies
                .SelectMany(a =>
                    a.GetTypes()
                     .Where(t => baseType.IsAssignableFrom(t)));

            foreach (var typeHandler in typeHandlers)
            {
                var iTypeHandler = Activator.CreateInstance(typeHandler) as ITypeHandler;
                var type = typeHandler?.BaseType?.GetGenericArguments()[0];
                if (type != null && iTypeHandler != null)
                {
                    SqlMapper.AddTypeHandler(type, iTypeHandler);
                }
            }

            return services;
        }
    }

    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid?>
    {
        public override Guid? Parse(object value)
        {
            if (value is null)
                return null;

            return new Guid(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, Guid? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}