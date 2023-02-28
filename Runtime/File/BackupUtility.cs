namespace WilliamQiufeng.UnityUtils.File
{
    public static class BackupUtility
    {
        public static void BackupFor(string path, int maxBackupCount)
        {
            BackupFor(path, 0, maxBackupCount);
        }

        private static string BackupFilePath(string path, int backupIndex)
        {
            return $"{path}.{backupIndex}.bak";
        }

        private static void BackupFor(string path, int backupIndex, int backupCount)
        {
            var srcPath = backupIndex == 0 ? path : BackupFilePath(path, backupIndex);
            var backupPath = BackupFilePath(path, backupIndex + 1);
            if (System.IO.File.Exists(backupPath) && backupIndex + 1 < backupCount)
                BackupFor(path, backupIndex + 1, backupCount);
            System.IO.File.Copy(srcPath, backupPath, true);
        }
    }
}