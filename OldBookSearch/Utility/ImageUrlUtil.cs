using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldBookSearch.Utility
{
    public class ImageUrlUtil
    {
        public static string GetImageUrl(string url)
        {
            // if not existed, return embedded image url
            if (string.IsNullOrWhiteSpace(url))
            {
                return "images/no-image.png";
            }
            else
            {
                return url;
            }
        }
    }
}
