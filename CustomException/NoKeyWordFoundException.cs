﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.CustomException
{
    public class NoKeyWordFoundException : Exception
    {
        public NoKeyWordFoundException(string msg) : base(msg)
        {

        }
    }
}