using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using NTQ.Sdk.Core.Attributes;

namespace NTQ.Sdk.Core.Utilities
{
    public static class LinQUtils
    {
        /// <summary>
        /// Filter list entities base on the custom attribute
        /// </summary>
        /// <param name="source"></param>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IQueryable<TEntity> DynamicFilter<TEntity>(this IQueryable<TEntity> source, TEntity entity)
        {
            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {
                if (entity.GetType().GetProperty(propertyInfo.Name) != null)
                {
                    var propertyValue = entity.GetType().GetProperty(propertyInfo.Name)?
                        .GetValue(entity, null);

                    if (propertyValue != null &&
                        !propertyInfo.CustomAttributes.Any(
                            (Func<CustomAttributeData, bool>)(x => x.AttributeType == typeof(SkipAttribute))))
                    {
                        if (propertyInfo.CustomAttributes.Any(
                                (Func<CustomAttributeData, bool>)(x =>
                                    x.AttributeType == typeof(StringAttribute))))
                        {
                            source = source.Where(propertyInfo.Name + ".ToLower().Contains(@0)",
                                propertyValue.ToString()?.ToLower());
                        }
                        else if (propertyInfo.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(x =>
                                         x.AttributeType == typeof(BooleanAttribute))))
                        {
                            source = source.Where(propertyInfo.Name + "== @0", propertyValue);
                        }
                        else if (propertyInfo.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(x =>
                                         x.AttributeType == typeof(IntAttribute))))
                        {
                            source = source.Where(propertyInfo.Name + "== @0", propertyValue);
                        }
                        else if (propertyInfo.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(x =>
                                         x.AttributeType == typeof(ChildAttribute))))
                        {
                            // Filter child's properties
                            foreach (PropertyInfo propertyChild in propertyInfo.GetType().GetProperties())
                            {
                                var propertyChildValue = propertyValue.GetType().GetProperty(propertyChild.Name)?
                                    .GetValue(propertyValue, null);

                                if (propertyChildValue != null &&
                                    !propertyInfo.CustomAttributes.Any(
                                        (Func<CustomAttributeData, bool>)(x =>
                                            x.AttributeType == typeof(SkipAttribute))))
                                {
                                    if (propertyChild.CustomAttributes.Any((Func<CustomAttributeData, bool>)(x =>
                                            x.AttributeType == typeof(StringAttribute))))
                                    {
                                        source = source.Where(
                                            propertyInfo.Name + "." + propertyChild.Name + ".ToLower().Contains(@0)",
                                            propertyChildValue.ToString()?.ToLower());
                                    }
                                    else if (propertyInfo.CustomAttributes.Any(
                                                 (Func<CustomAttributeData, bool>)(x =>
                                                     x.AttributeType == typeof(BooleanAttribute))))
                                    {
                                        source = source.Where(propertyInfo.Name + "." + propertyChild.Name + "== @0",
                                            propertyChildValue);
                                    }
                                    else if (propertyInfo.CustomAttributes.Any(
                                                 (Func<CustomAttributeData, bool>)(x =>
                                                     x.AttributeType == typeof(IntAttribute))))
                                    {
                                        source = source.Where(propertyInfo.Name + "." + propertyChild.Name + "== @0",
                                            propertyChildValue);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return source;
        }

        /// <summary>
        /// Sort list entities by SortDirection and SortBy custom attribute
        /// </summary>
        /// <param name="source"></param>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <example>
        /// services
        /// .AddMvc()
        /// .AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter());
        /// </example>
        /// <returns></returns>
        public static IQueryable<TEntity> DynamicSort<TEntity>(this IQueryable<TEntity> source, TEntity entity)
        {
            if (entity.GetType().GetProperties()
                    .Any(x => x.CustomAttributes.Any(a => a.AttributeType == typeof(SortDirectionAttribute))) &&
                entity.GetType().GetProperties()
                    .Any(x => x.CustomAttributes.Any(a => a.AttributeType == typeof(SortPropertyAttribute))))
            {
                var sortDirection = entity.GetType().GetProperties().SingleOrDefault(x =>
                        x.CustomAttributes.Any(a => a.AttributeType == typeof(SortDirectionAttribute)))?
                    .GetValue(entity, null);
                var sortBy = entity.GetType().GetProperties().SingleOrDefault(x =>
                        x.CustomAttributes.Any(a => a.AttributeType == typeof(SortPropertyAttribute)))?
                    .GetValue(entity, null);

                if (sortDirection != null && sortBy != null)
                {
                    if ((string)sortDirection == "asc")
                    {
                        source = source.OrderBy((string)sortBy);
                    }
                    else if ((string)sortDirection == "desc")
                    {
                        source = source.OrderBy((string)sortBy + " descending");
                    }
                }
            }

            return source;
        }

        /// <summary>
        /// Paging the list entities
        /// </summary>
        /// <param name="source"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="limitPaging"></param>
        /// <param name="defaultPaging"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static (int, IQueryable<TResult>) PagingQueryable<TResult>(this IQueryable<TResult> source, int page,
            int size, int limitPaging = 50, int defaultPaging = 1)
        {
            if (size > limitPaging)
            {
                size = limitPaging;
            }

            if (size < 1)
            {
                size = defaultPaging;
            }

            if (page < 1)
            {
                page = 1;
            }

            int total = source.Count();
            IQueryable<TResult> results = source.Skip((page - 1) * size).Take(size);
            return (total, results);
        }
    }
}