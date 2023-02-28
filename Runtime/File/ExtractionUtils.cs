using System.IO;
using System.IO.Compression;

namespace WilliamQiufeng.UnityUtils.File
{
    public class ExtractionUtils
    {
        public static void ImprovedExtractToDirectory(string sourceArchiveFileName,
            string destinationDirectoryName,
            Overwrite overwriteMethod = Overwrite.IfNewer)
        {
            //Opens the zip file up to be read
            using var archive = ZipFile.OpenRead(sourceArchiveFileName);
            //Loops through each file in the zip file
            foreach (var file in archive.Entries)
                ImprovedExtractToFile(file, destinationDirectoryName, overwriteMethod);
        }

        public static void ImprovedExtractToFile(ZipArchiveEntry file,
            string destinationPath,
            Overwrite overwriteMethod = Overwrite.IfNewer)
        {
            //Gets the complete path for the destination file, including any
            //relative paths that were in the zip file
            var destinationFileName = Path.Combine(destinationPath, file.FullName);

            //Gets just the new path, minus the file name so we can create the
            //directory if it does not exist
            var destinationFilePath = Path.GetDirectoryName(destinationFileName);

            //Creates the directory (if it doesn't exist) for the new path
            Directory.CreateDirectory(destinationFilePath);

            //Determines what to do with the file based upon the
            //method of overwriting chosen
            switch (overwriteMethod)
            {
                case Overwrite.Always:
                    //Just put the file in and overwrite anything that is found
                    file.ExtractToFile(destinationFileName, true);
                    break;
                case Overwrite.IfNewer:
                    //Checks to see if the file exists, and if so, if it should
                    //be overwritten
                    if (!System.IO.File.Exists(destinationFileName) ||
                        System.IO.File.GetLastWriteTime(destinationFileName) < file.LastWriteTime)
                        //Either the file didn't exist or this file is newer, so
                        //we will extract it and overwrite any existing file
                        file.ExtractToFile(destinationFileName, true);
                    break;
                case Overwrite.Never:
                    //Put the file in if it is new but ignores the 
                    //file if it already exists
                    if (!System.IO.File.Exists(destinationFileName)) file.ExtractToFile(destinationFileName);
                    break;
            }
        }
    }

    public enum Overwrite
    {
        Always,
        IfNewer,
        Never
    }
}