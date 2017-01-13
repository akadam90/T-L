using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAndLearn.Web.Extensions
{
    public static class UrlExtensions
    {
        public static string ContentNoCaching(this UrlHelper helper, string fileName)
        {
            //Random rand = new Random();
            //int num = rand.Next();

            //var absPath = helper.Content(fileName);
            //System.IO.FileInfo info = new System.IO.FileInfo(absPath);
            var ticks = DateTime.Now.Ticks;// info.LastWriteTime.Ticks;

            return helper.Content(string.Format("{0}?{1}", fileName, ticks));
        }
    }
}