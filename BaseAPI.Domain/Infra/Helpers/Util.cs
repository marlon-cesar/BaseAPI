using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace BaseAPI.Domain.Infra.Helpers
{
    public static class Util
    {
        #region Reflection

        public static List<Type> GetTypesOf(this Assembly assembly, Type baseType) => assembly
            .GetTypes()
            .Where(type =>
                type.BaseType != null
                && (
                    (type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == baseType)) // -> Generics, ex: CrudRepository<>
                    || (baseType.IsAssignableFrom(type) && !type.IsAbstract) // -> Non generics, ex: Repository
                )
            .ToList();

        #endregion

        #region Cryptography

        public static string ToMD5(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var result = new StringBuilder();
            var bytes = new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString("X2"));

            return result.ToString();
        }

        #endregion
    }
}
