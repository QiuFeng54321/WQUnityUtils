namespace WilliamQiufeng.UnityUtils.File
{
    public static class BackupUtility
    {
        public delegate string GenerateFilePath(string path, int backupIndex);

        public static void BackupFor(string path, int maxBackupCount, GenerateFilePath filePathGenerator = default)
        {
            BackupFor(path, 0, maxBackupCount, filePathGenerator);
        }

        private static string BackupFilePath(string path, int backupIndex)
        {
            return $"{path}.{backupIndex}.bak";
        }

        private static void BackupFor(string path, int backupIndex, int backupCount,
            GenerateFilePath filePathGenerator = default)
        {
            var srcPath = backupIndex == 0 ? path : (filePathGenerator ?? BackupFilePath)(path, backupIndex);
            var backupPath = BackupFilePath(path, backupIndex + 1);
            if (System.IO.File.Exists(backupPath) && backupIndex + 1 < backupCount)
                BackupFor(path, backupIndex + 1, backupCount);
            System.IO.File.Copy(srcPath, backupPath, true);
        }
    }
}