﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project21WPF_ClassLibrary.AuthenticationModel
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string username { get; set; }
    }
}