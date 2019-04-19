//bibaoke.com

using Less.Text;

namespace Less.Network
{
    /// <summary>
    /// MimeType
    /// </summary>
    public class MimeType : StringEnum
    {
        /// <summary>
        /// image/jpeg
        /// </summary>
        public static MimeType Image_Jpeg
        {
            get;
            private set;
        }

        /// <summary>
        /// image/png
        /// </summary>
        public static MimeType Image_Png
        {
            get;
            private set;
        }

        /// <summary>
        /// image/gif
        /// </summary>
        public static MimeType Image_Gif
        {
            get;
            private set;
        }

        /// <summary>
        /// image/icon
        /// </summary>
        public static MimeType Image_Icon
        {
            get;
            private set;
        }

        /// <summary>
        /// image/bmp
        /// </summary>
        public static MimeType Image_Bmp
        {
            get;
            private set;
        }

        /// <summary>
        /// image/tiff
        /// </summary>
        public static MimeType Image_Tiff
        {
            get;
            private set;
        }

        /// <summary>
        /// image/exif
        /// </summary>
        public static MimeType Image_Exif
        {
            get;
            private set;
        }

        /// <summary>
        /// image/emf
        /// </summary>
        public static MimeType Image_Emf
        {
            get;
            private set;
        }

        /// <summary>
        /// image/wmf
        /// </summary>
        public static MimeType Image_Wmf
        {
            get;
            private set;
        }

        /// <summary>
        /// image/memorybmp
        /// </summary>
        public static MimeType Image_MemoryBmp
        {
            get;
            private set;
        }

        /// <summary>
        /// image/vnd.microsoft.icon
        /// </summary>
        public static MimeType Image_VndMicrosoftIcon
        {
            get;
            private set;
        }

        /// <summary>
        /// text/html
        /// </summary>
        public static MimeType Text_Html
        {
            get;
            private set;
        }

        /// <summary>
        /// text/xml
        /// </summary>
        public static MimeType Text_Xml
        {
            get;
            private set;
        }

        /// <summary>
        /// text/css
        /// </summary>
        public static MimeType Text_Css
        {
            get;
            private set;
        }

        /// <summary>
        /// application/javascript
        /// </summary>
        public static MimeType Application_Javascript
        {
            get;
            private set;
        }

        /// <summary>
        /// application/x-javascript
        /// </summary>
        public static MimeType Application_XJavascript
        {
            get;
            private set;
        }

        /// <summary>
        /// application/json
        /// </summary>
        public static MimeType Application_Json
        {
            get;
            private set;
        }

        /// <summary>
        /// */*
        /// </summary>
        public static MimeType All
        {
            get;
            private set;
        }

        static MimeType()
        {
            MimeType.Image_Jpeg = "image/jpeg";
            MimeType.Image_Png = "image/png";
            MimeType.Image_Gif = "image/gif";
            MimeType.Image_Icon = "image/icon";
            MimeType.Image_Bmp = "image/bmp";
            MimeType.Image_Tiff = "image/tiff";
            MimeType.Image_Exif = "image/exif";
            MimeType.Image_Emf = "image/emf";
            MimeType.Image_Wmf = "image/wmf";
            MimeType.Image_MemoryBmp = "image/memorybmp";
            MimeType.Image_VndMicrosoftIcon = "image/vnd.microsoft.icon";
            MimeType.Text_Html = "text/html";
            MimeType.Text_Xml = "text/xml";
            MimeType.Text_Css = "text/css";
            MimeType.Application_Javascript = "application/javascript";
            MimeType.Application_XJavascript = "application/x-javascript";
            MimeType.Application_Json = "application/json";
            MimeType.All = "*/*";
        }

        private MimeType(string value) : base(value)
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(MimeType l, MimeType r)
        {
            if (l.IsNotNull() && r.IsNotNull())
            {
                return l.Value.CompareTrimAndIgnoreCase(r.Value);
            }

            return l.IsNull() && r.IsNull();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(MimeType l, MimeType r)
        {
            return !(l == r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is MimeType)
            { 
                return this == (MimeType)obj;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 从 MimeType 到 string 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(MimeType value)
        {
            return value.IsNotNull() ? value.ToString() : null;
        }

        /// <summary>
        /// 从 string 到 MimeType 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator MimeType(string value)
        {
            return value.IsNotNull() ? new MimeType(value) : null;
        }
    }
}
