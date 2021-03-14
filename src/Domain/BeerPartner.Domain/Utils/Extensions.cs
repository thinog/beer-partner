using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace BeerPartner.Domain.Utils
{
    public static class Extensions
    {
        public static string GetJsonPropertyName<TObject, TProperty>(this TObject obj, Expression<Func<TObject, TProperty>> propertyExpression)
        {
            var memberExpression = propertyExpression.Body as MemberExpression;

            var jsonPropertyNameAttributes = obj
                .GetType()
                .GetProperties()
                .First(p => p.Name.Equals(memberExpression.Member.Name))
                .GetCustomAttributes(typeof(JsonPropertyNameAttribute), true);

            if(jsonPropertyNameAttributes != null && jsonPropertyNameAttributes.Any())
                return ((JsonPropertyNameAttribute)jsonPropertyNameAttributes[0]).Name;

            return memberExpression.Member.Name;
        }
    }
}