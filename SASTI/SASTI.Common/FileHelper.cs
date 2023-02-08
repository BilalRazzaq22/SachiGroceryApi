using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SASTI.Common
{
    public class FileHelper
    {
        public static Bitmap CropImage(string path, int width, int height, int X, int Y)
        {
            Rectangle cropRect = new Rectangle(X, Y, width, height);
            Bitmap src = Image.FromFile(path) as Bitmap;
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
                if (src != null)
                    ((IDisposable)src).Dispose();
            }
            return target;
        }
        public static ImageResponse GetImage(HttpFileCollection files, string fileKey, string folderPath, int thumbWidth, int thumbHeight, bool deleteOriginal)
        {
            var file = files.Count > 0 ? files[fileKey] : null;
            ImageResponse response = new ImageResponse();
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName).ToLower();
                string[] arr = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp",".doc",".txt"};

                if (arr.Contains(extension))
                {
                    string photoName = Guid.NewGuid() + extension;
                    string thumb = Guid.NewGuid() + extension;

                    string imagePath = ConfigurationManager.AppSettings["RootPath"] + folderPath + "\\" + photoName;
                    string newFilePath = ConfigurationManager.AppSettings["RootPath"] + folderPath + "\\";
                    if (!Directory.Exists(newFilePath))
                    {
                        Directory.CreateDirectory(newFilePath);
                    }
                    file.SaveAs(imagePath);

                    ResizeImage(imagePath, newFilePath, thumb, thumbWidth, thumbHeight);

                    if (deleteOriginal == true)
                    {
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                    }


                    response.ThumbnailURL = ConfigurationManager.AppSettings["WebPath"].ToString() + "content/" + folderPath + "/" + thumb;
                    response.ImageURL = "";
                    response.IsSuccess = true;
                    response.ResponseMessage = "";

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseMessage = "File extension not supported.";

                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseMessage = "File not found.";

                return response;
            }
        }
        public static ImageResponse GetImage(HttpFileCollectionBase files, string fileKey, string folderPath, int thumbWidth, int thumbHeight, int imgWidth, int imgHeight, bool deleteOriginal)
        {
            var file = files.Count > 0 ? files[fileKey] : null;
            ImageResponse response = new ImageResponse();
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName).ToLower();
                string[] arr = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };

                if (arr.Contains(extension))
                {
                    string photoName = Guid.NewGuid() + extension;
                    string thumb = Guid.NewGuid() + extension;
                    string img = Guid.NewGuid() + extension;

                    string imagePath = ConfigurationManager.AppSettings["RootPath"] + folderPath + "\\" + photoName;
                    string newFilePath = ConfigurationManager.AppSettings["RootPath"] + folderPath + "\\";
                    if (!Directory.Exists(newFilePath))
                    {
                        Directory.CreateDirectory(newFilePath);
                    }
                    file.SaveAs(imagePath);

                    ResizeImage(imagePath, newFilePath, thumb, thumbWidth, thumbHeight);
                    ResizeImage(imagePath, newFilePath, img, imgWidth, imgHeight);

                    if (deleteOriginal == true)
                    {
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                    }


                    response.ThumbnailURL = ConfigurationManager.AppSettings["WebPath"].ToString() + "content" + folderPath.Replace(@"\", "/") + "/" + thumb;
                    response.ImageURL = ConfigurationManager.AppSettings["WebPath"].ToString() + "content" + folderPath.Replace(@"\", "/") + "/" + img;
                    response.IsSuccess = true;
                    response.ResponseMessage = "";

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseMessage = "File extension not supported.";

                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseMessage = "File not found.";

                return response;
            }
        }
        public static string GetVideo(HttpFileCollection files, string fileKey)
        {
            var file = files.Count > 0 ? files[fileKey] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName).ToLower();
                string videoName = Guid.NewGuid() + extension;

                string currentYear = DateTime.Now.Year.ToString();
                string currentMonth = DateTime.Now.Month.ToString();

                string VideoPath = ConfigurationManager.AppSettings["RootPath"].ToString() + "Videos\\" + currentYear + "\\" + currentMonth + "\\";
                string VideoUrl = ConfigurationManager.AppSettings["WebPath"].ToString() + "Content/Uploads/Videos/" + currentYear + "/" + currentMonth + "/" + videoName;

                if (!Directory.Exists(VideoPath))
                {
                    Directory.CreateDirectory(VideoPath);
                }
                file.SaveAs(VideoPath + videoName);
                return VideoUrl;
            }
            else
            {
                return "";
            }
        }
        public static string ResizeImage(string filePath, string newFilePath, string newImgName, int newWidth, int newHeight)
        {
            string newImagePath;

            try
            {
                newImagePath = newFilePath + newImgName;

                Image imgPhoto = Image.FromFile(filePath);

                // Do not reszie image if its width is <= newWidth  ///
                if (imgPhoto.Width <= newWidth)
                {
                    imgPhoto.Save(newImagePath);
                    imgPhoto.Dispose();
                    return newImgName;
                }

                int sourceWidth, sourceHeight, sourceX, sourceY, destX, destY;
                double nPercent, nPercentW, nPercentH;
                sourceWidth = imgPhoto.Width;
                sourceHeight = imgPhoto.Height;
                sourceX = 0;
                sourceY = 0;
                destX = 0;
                destY = 0;

                nPercent = 0;
                nPercentW = 0;
                nPercentH = 0;

                // nPercentW = (Convert.ToDouble(newWidth) / Convert.ToDouble(sourceWidth));
                //nPercentH = (Convert.ToDouble(newHeight) / Convert.ToDouble(sourceHeight));

                nPercentW = ((float)newWidth / (float)sourceWidth);
                nPercentH = ((float)newHeight / (float)sourceHeight);

                if (nPercentH < nPercentW)
                {
                    nPercent = nPercentH;
                }
                else
                {
                    nPercent = nPercentW;
                }



                //if (nPercentH < nPercentW)
                //{
                //    nPercent = nPercentW;
                //    destY = Convert.ToInt32((newHeight - (sourceHeight * nPercent)) / 2);
                //}
                //else
                //{
                //    nPercent = nPercentH;
                //    destX = Convert.ToInt32((newWidth - (sourceWidth * nPercent)) / 2);
                //}

                if (nPercent > 1)
                    nPercent = 1;

                int destWidth = (int)Math.Round(sourceWidth * nPercent);
                int destHeight = (int)Math.Round(sourceHeight * nPercent);


                //  int destWidth, destHeight;
                // destWidth = Convert.ToInt32(sourceWidth * nPercent);
                //destHeight = Convert.ToInt32(sourceHeight * nPercent);

                // Bitmap bmPhoto;
                //bmPhoto = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                Bitmap bmPhoto = new Bitmap(destWidth <= newWidth ? destWidth : newWidth, destHeight < newHeight ? destHeight : newHeight, PixelFormat.Format32bppRgb);

                //bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                //Graphics grPhoto;
                //grPhoto = Graphics.FromImage(bmPhoto);
                //grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
                Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
                grPhoto.Clear(System.Drawing.Color.White);
                grPhoto.InterpolationMode = InterpolationMode.Default;
                grPhoto.CompositingQuality = CompositingQuality.HighQuality;
                grPhoto.SmoothingMode = SmoothingMode.HighQuality;

                grPhoto.DrawImage(imgPhoto,
                new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
                new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                System.Drawing.GraphicsUnit.Pixel);

                grPhoto.Dispose();

                bmPhoto.Save(newImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                bmPhoto.Dispose();

                imgPhoto.Dispose();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return newImgName;
        }


        public static ImageResponse SaveImageByUrl(string ImageUrl)
        {
            ImageResponse response = new ImageResponse();
            try
            {
                
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(ImageUrl);
                Bitmap bitmap;
                bitmap = new Bitmap(stream);
                string thumb = Guid.NewGuid() + "facebook.png";
                string filePath = ConfigurationManager.AppSettings["RootPath"] + "\\uploads\\users\\" + thumb;

                if (bitmap != null)
                {
                    bitmap.Save(filePath, ImageFormat.Png);

                    response.ThumbnailURL = ConfigurationManager.AppSettings["WebPath"] + "content\\uploads\\users\\" + thumb;
                    response.ImageURL = ConfigurationManager.AppSettings["WebPath"] + "content\\uploads\\users\\" + thumb;
                    response.IsSuccess = true;
                    response.ResponseMessage = "";
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseMessage = "File extension not supported.";
                }

                stream.Flush();
                stream.Close();
                client.Dispose();

                return response;
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ResponseMessage = "File extension not supported.";
                return response;
            }
           
        }
    
    }

    public class ImageResponse
    {
        public string ThumbnailURL { get; set; }
        public string ImageURL { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
    }
}
