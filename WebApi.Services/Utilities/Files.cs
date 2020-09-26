using System.IO;
using System.Threading.Tasks;

namespace WebApi.Services.Utilities
{
    public static class Files
    {
        #region Create

        public static bool Create(string content, string filename, string physicalPath)
        {
            if (Exists(filename, physicalPath)) return false;

            if(!DirectoryExists(physicalPath))
                DirectoryCreate(physicalPath);

            using (var file = File.CreateText(physicalPath + filename))
                file.Write(content);

            return true;
        }

        public static async Task<bool> CreateAsync(string content, string filename, string directory)
        {
            if (Exists(filename, directory)) return false;

            if(!DirectoryExists(directory))
                DirectoryCreate(directory);

            using (var file = File.CreateText(directory + filename))
                await file.WriteAsync(content);

            return true;
        }

        public static void DirectoryCreate(string directory)
        {
            Directory.CreateDirectory(directory);
        }

        #endregion

        #region Delete

        public static void DeleteDirectory(string directory)
        {
            if (DirectoryExists(directory))
                Directory.Delete(directory, true);
        }

        public static async Task DeleteDirectoryAsync(string directory)
        {
            if (DirectoryExists(directory))
                await Task.Run(() => Directory.Delete(directory, true));
        }

        public static void Delete(string filename, string directory)
        {
            if (Exists(filename, directory))
                File.Delete(directory + filename);
        }

        public static async Task DeleteAsync(string filename, string directory)
        {
            if (Exists(filename, directory))
                await Task.Run(() => File.Delete(directory + filename));
        }

        #endregion

        #region Exists

        public static bool Exists(string filename, string directory)
        {
            return File.Exists(directory + filename);
        }

        public static bool DirectoryExists(string directory)
        {
            return Directory.Exists(directory);
        }

        #endregion
    }
}
