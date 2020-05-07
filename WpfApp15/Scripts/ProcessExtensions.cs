using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp15.Scripts
{
    public static class ProcessExtensions
    {
        [DllImport("Kernel32.dll")]
        private static extern uint QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        public static string GetMainModuleFileName(this Process process, int buffer = 1024)
        {
            var fileNameBuilder = new StringBuilder(buffer);
            uint bufferLength = (uint)fileNameBuilder.Capacity + 1;
            return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) != 0 ?
                fileNameBuilder.ToString() :
                null;
        }

        public static Icon GetIcon(this Process process)
        {
            try
            {
                string mainModuleFileName = process.GetMainModuleFileName();
                return Icon.ExtractAssociatedIcon(mainModuleFileName);
            }
            catch
            {
                // Probably no access
                return null;
            }
        }
        public static ImageSource ToImageSource(this Icon icon)
        {
            try
            {
                ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
                return imageSource;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public  static ImageSource ToImageSource(this Image image)
        {
                 var ms = new MemoryStream();
                image.Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnDemand;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
                return bitmapImage;         
        }
    }
}
