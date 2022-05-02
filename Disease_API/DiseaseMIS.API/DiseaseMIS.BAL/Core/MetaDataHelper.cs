using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace DiseaseMIS.BAL.Core
{
    public static class MetaDataHelper
    {
        public static List<object> GetPropertyInfo<T>() where T : class
        {
            Type temp = typeof(T);
            PropertyInfo[] propertyInfos = temp.GetProperties();

            List<object> _fields = new List<object>();
            foreach (var prop in propertyInfos)
            {
                _fields.Add(new
                {
                    prop.Name,
                    Type = prop.PropertyType.Name
                });
            }

            return _fields;
        }

        public static void SetBaseData<T>(List<T> entity) where T : class
        {
            foreach (var item in entity)
            {
                item.GetType().GetProperty("CreatedOn").SetValue(item, DateTime.Now, null);
                item.GetType().GetProperty("LastUpdatedOn").SetValue(item, DateTime.Now, null);
            }
        }

        public static void UpdateBaseData<T>(T entity) where T : class
        {
            entity.GetType().GetProperty("LastUpdatedOn").SetValue(entity, DateTime.Now, null);
        }

        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }

        public static void DetachLocal<T>(this DbContext context, T t, string entryId) where T : class
        {
            var local = context.Set<T>().Local
                .FirstOrDefault(entry => entry.Equals(entryId));
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
