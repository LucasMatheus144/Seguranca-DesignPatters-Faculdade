using System.ComponentModel.DataAnnotations;

namespace EspacoPotencial.Areas.Cadastro
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ValidateFileAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions = { ".png", ".jpg" };

        public override bool IsValid(object value)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                return _allowedExtensions.Any(e => e.Equals(extension, StringComparison.OrdinalIgnoreCase));
            }

            return true; 
        }
    }
}