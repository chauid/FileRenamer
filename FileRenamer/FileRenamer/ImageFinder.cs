using System.ComponentModel.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FindImage
{
    /// <summary>
    /// 이미지 파일을 RootPath에서 모든 하위 디렉토리를 조회하여 찾아줍니다.
    /// </summary>
    internal class ImageFinder
    {
        private static readonly string ProjectPath = Path.GetFullPath(Path.Combine(Application.StartupPath));
        private struct ImageInfo
        {
            public bool IsFind;
            public Image ImageFile;
        }
        #region GetImageFile
        public static Image GetIamgeFile(string InputFileName) // 파일 이름만 넣을 시 
        {
            ImageInfo SearchedFile = SearchProcess(InputFileName, ProjectPath);
            if (SearchedFile.IsFind) return SearchedFile.ImageFile;
            else return new Bitmap(1,1); // 파일없음 : (1,1)비트맵 
        }
        public static Image GetIamgeFile(string InputFileName, string InputRootDirectory) // 파일 이름 + 경로 
        {
            ImageInfo SearchedFile = SearchProcess(InputFileName, InputRootDirectory);
            if (SearchedFile.IsFind) return SearchedFile.ImageFile;
            else return new Bitmap(1, 1); // 파일없음 : (1,1)비트맵 
        }
        public static Image GetIamgeFile(string InputFileName, int width, int height) // 파일 이름 + 크기 
        {
            return GetIamgeFile(InputFileName, ProjectPath, width, height);
        }
        public static Image GetIamgeFile(string InputFileName, string InputRootDirectory, int width, int height) // 파일 이름 + 경로 + 크기 
        {
            Image image;
            ImageInfo SearchedFile = SearchProcess(InputFileName, InputRootDirectory);
            if (SearchedFile.IsFind) image = SearchedFile.ImageFile;
            else return new Bitmap(1, 1);
            Rectangle destRect = new(0, 0, width, height);
            Bitmap destImage = new(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
            return destImage;
        }
        #endregion
        private static ImageInfo SearchProcess(string InputFileName, string InputRootDirectory)
        {
            ImageInfo SearchedFile = new() { ImageFile = new Bitmap(1, 1), IsFind = false };
            int overlap = 0, overlaptemp = 0;
            if (InputRootDirectory == null || InputFileName == null) return SearchedFile;
            DirectoryInfo SearchDirectory = new(InputRootDirectory);
            FileInfo[] RootSearch_File = SearchDirectory.GetFiles(InputFileName);
            foreach (FileInfo files in RootSearch_File) Console.WriteLine("search:{0}", files);
            foreach (FileInfo files in RootSearch_File) overlap++;
            foreach (FileInfo files in RootSearch_File) // RootDirectory
            {
                overlaptemp++;
                if (overlaptemp > 1) break;
                SearchedFile.ImageFile = Image.FromFile(files.FullName);
                SearchedFile.IsFind = true;
            }
            foreach (DirectoryInfo SubDirectory in SearchDirectory.GetDirectories()) // SubDirectory
            {
                if ((SubDirectory.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) // 숨김 파일 조회X
                {
                    FileInfo[] Search_File = SubDirectory.GetFiles(InputFileName, SearchOption.AllDirectories);
                    foreach (FileInfo files in Search_File) Console.WriteLine("search:{0}", files); // 찾은 파일 경로 Debug
                    foreach (FileInfo files in Search_File) overlap++; // 중복 파일 수 계산
                    foreach (FileInfo files in Search_File)
                    {
                        overlaptemp++;
                        if (overlaptemp > 1) break;
                        SearchedFile.ImageFile = Image.FromFile(files.FullName);
                        SearchedFile.IsFind = true;
                    }
                    if (overlaptemp > 1) break;
                }
            }
            if (overlaptemp > 1) { Console.WriteLine("이미지파일 이름 중복 발생! 중복파일 수:{0}", overlap); return SearchedFile; } // 중복파일 (먼저 발견한 파일 우선)
            else if (overlaptemp == 1) return SearchedFile; // 파일 찾음
            else { Console.WriteLine("이미지 없음"); return SearchedFile; } // 파일 없음
        }
    }
}
