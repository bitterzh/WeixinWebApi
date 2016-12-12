﻿using System;

namespace Totyu.WeixinSDK.Annotations
{

    public class IgnoreValueAttribute : Attribute
    {
        public IgnoreValueAttribute(object value)
        {
            this.Value = value;
        }
        public object Value { get; set; }
    }
}
