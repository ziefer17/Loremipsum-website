using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectLoremipsu.Models
{
    public class PropertyViewModel
    {
        public property Property { get; set; }
        public HttpPostedFileBase Img1 { get; set; }
        public HttpPostedFileBase Img2 { get; set; }
        public HttpPostedFileBase Img3 { get; set; }
        public HttpPostedFileBase Img4 { get; set; }
    }
}