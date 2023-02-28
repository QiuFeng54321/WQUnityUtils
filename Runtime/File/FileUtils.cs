using System.IO;

namespace WilliamQiufeng.UnityUtils.File
{
    public class FileUtils
    {
        public static string GetAvailableFileName(string path)
        {
            var dir = Path.GetDirectoryName(path) ?? "";
            var name = Path.GetFileNameWithoutExtension(path);
            var ext = Path.GetExtension(path);
            if (!System.IO.File.Exists(path)) return path;
            var i = 1;
            var res = path;
            while (System.IO.File.Exists(res = Path.Combine(dir, $"{name}{i}{ext}"))) i++;
            return res;
        }

        public static string CopyIfNotIn(string path, string dir)
        {
            if (!Path.GetRelativePath(dir, path).StartsWith("..")) return path;
            var name = Path.GetFileName(path);
            var resPath = Path.Combine(dir, name);
            System.IO.File.Copy(path, resPath, true);
            return resPath;
        }
    }
}