using RealSimulation7.Utilities.Enums;

namespace RealSimulation7.Utilities.Extentions
{
    public static class FileValidator
    {
        public static bool ValidateSize(this IFormFile file, int size, FileSize fileSize)
        {
            switch (fileSize)
            {
                case FileSize.KB:
                    return file.Length <= size * 1024;

                case FileSize.MB:
                    return file.Length <= size * 1024 * 1024;

                case FileSize.GB:
                    return file.Length <= size * 1024 * 1024 * 1024;
            }
            return false;
        }

        public static bool ValidateType(this IFormFile file, string fileType)
        {
            if(file.ContentType.Contains(fileType))
            {
                return true;
            }
            return false;
        }

        public static async Task<string> CreateFile(this IFormFile file, params string[] roots)
        {
            string fileName = String.Concat(Guid.NewGuid().ToString(), file.FileName);
            string path = string.Empty; 

            for(int i=0; i<roots.Length; i++)
            {
                path=Path.Combine(path, roots[i]);
            }
            path = Path.Combine(path, fileName);

            using(FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }

        public static async void DeleteFile(this string fileName, params string[] roots)
        {
            string path = string.Empty;

            for (int i = 0; i < roots.Length; i++)
            {
                path = Path.Combine(path, roots[i]);
            }
            path = Path.Combine(path, fileName);

            File.Delete(path);
        }
    }
}
