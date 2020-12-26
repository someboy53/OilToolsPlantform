using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace com.Origin.CommonLibrary.Framework
{
    /// <summary>
    /// 图片压缩
    /// </summary>
    public class ImageCompress
    {
        /// <summary>
        /// 指定缩放类型
        /// </summary>
        public enum ImageCompressType
        {
            //***指定高宽缩放（可能变形）
            WH = 0,
            //***指定宽，高按比例
            W = 1,
            //***指定高，宽按比例
            H = 2,
            //***指定高宽裁减（不变形）
            Cut = 3,
            //***不指定，使用原始
            N = 4
        }

        #region Compress
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="height">高度</param>
        /// <param name="width"></param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <param name="type">压缩缩放类型</param>
        /// <returns></returns>
        public bool Compress(string sFile, string dFile, int height, int width, int flag, ImageCompressType type)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;

            //****缩放后的宽度和高度
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = iSource.Width;
            int oh = iSource.Height;

            switch (type)
            {
                case ImageCompressType.N://***原始高宽
                    {
                        towidth = ow;
                        toheight = oh;
                        break;
                    }
                case ImageCompressType.WH://指定高宽缩放（可能变形）           
                    {
                        break;
                    }
                case ImageCompressType.W://指定宽，高按比例     
                    {
                        toheight = iSource.Height * width / iSource.Width;
                        break;
                    }
                case ImageCompressType.H://指定高，宽按比例
                    {
                        towidth = iSource.Width * height / iSource.Height;
                        break;
                    }
                case ImageCompressType.Cut://指定高宽裁减（不变形）     
                    {
                        if ((double)iSource.Width / (double)iSource.Height > (double)towidth / (double)toheight)
                        {
                            oh = iSource.Height;
                            ow = iSource.Height * towidth / toheight;
                            y = 0;
                            x = (iSource.Width - ow) / 2;
                        }
                        else
                        {
                            ow = iSource.Width;
                            oh = iSource.Width * height / towidth;
                            x = 0;
                            y = (iSource.Height - oh) / 2;
                        }
                        break;
                    }
                default:
                    break;
            }

            Bitmap ob = new Bitmap(towidth, toheight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(System.Drawing.Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource
              , new Rectangle(x, y, towidth, toheight)
              , new Rectangle(0, 0, iSource.Width, iSource.Height)
              , GraphicsUnit.Pixel);
            g.Dispose();
            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int i = 0; i < arrayICI.Length; i++)
                {
                    if (arrayICI[i].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[i];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();

                ob.Dispose();

            }
        }
        #endregion
    }
}