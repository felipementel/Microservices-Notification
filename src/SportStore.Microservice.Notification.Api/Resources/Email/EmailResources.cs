using System;
using System.IO;

namespace SportStore.Microservice.Product.Domain.Resources.Email
{
    public class EmailResources
    {
        public static string GetEmailTemplateNewProduct()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var separator = Path.DirectorySeparatorChar;

            var filePath = $"{baseDir}{separator}Resources{separator}Email{separator}Templates{separator}NewProduct.html";

            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }

            return null;
        }
    }
}
