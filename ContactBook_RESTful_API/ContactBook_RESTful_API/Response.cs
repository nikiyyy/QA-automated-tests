﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook_RESTful_API
{
    class Response
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string dateCreated { get; set; }
        public string comments { get; set; }
    }
}
