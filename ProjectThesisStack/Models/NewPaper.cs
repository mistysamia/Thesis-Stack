using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectThesisStack.Models
{
    public class NewPaper
    {
        public string teamName { get; set; }
        public string paperTitle { get; set; }
        public string paperBody { get; set; }
        public string tags { get; set; }
        public string publicationsLink { get; set; }
        public string status { get; set; }
        public string PDFLink { get; set; }

    }
}