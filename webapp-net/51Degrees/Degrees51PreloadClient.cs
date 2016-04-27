﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using FiftyOne.Foundation.Mobile.Detection.Entities.Stream;
using FiftyOne.Foundation.Mobile.Detection.Factories;
using Sdl.Web.Mvc.Configuration;

namespace Sdl.Web.Modules.Degrees51
{    
    public class Degrees51PreloadClient : IProcessHostPreloadClient
    {
        public static readonly string LITE_URI = "https://github.com/51Degrees/dotNET-Device-Detection/blob/master/data/51Degrees-LiteV3.2.dat?raw=true";

        public void Preload(string[] parameters)
        {          
            try
            {
                string liteUri = WebConfigurationManager.AppSettings["fiftyOneDegrees.lite.dataset"] ?? LITE_URI;
                // we need to read the BinaryFilePath to find out where 51degree's is looking for its dataset but unfortunatly this is an
                // internal property so we use reflection to get it.
                var configSection = WebConfigurationManager.GetWebApplicationSection("fiftyOne/detection");
                Type configSectionType = configSection.GetType();
                System.Reflection.PropertyInfo info = configSectionType.GetProperty("BinaryFilePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                string path = (string)info.GetValue(configSection);
                if (path.Contains("~"))
                {
                    // remap to physical path if required
                    path = HttpContext.Current.Server.MapPath(path);
                }
                FileInfo fileInfo = new FileInfo(path);

                // check if dataset file exists              
                if (!fileInfo.Exists)
                {
                    if (!Directory.Exists(fileInfo.DirectoryName))
                    {
                        Directory.CreateDirectory(fileInfo.DirectoryName);
                    }
                    // we need to download the lite dataset at this point
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(liteUri);
                    request.Method = "GET";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using (FileStream fileStream = fileInfo.Create())
                    {
                        response.GetResponseStream().CopyTo(fileStream);
                    }
                }
            }
            catch
            { }           
        }
    }
}
