﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace System
{
    public static class XmlExtension
    {
        public static string ParseApiData(this XmlReader reader, string Section,
            string Atribute)
        {
            reader.ReadToFollowing(Section);
            reader.MoveToAttribute(Atribute);
            return reader.Value;
        }
    }

    public static class JsonExtension
    {
        public static string ParseAPIData(this string source, string sectionName, string parametrName, string parametrSighs, string acceptedSigns)
        {
            string result = "";
            var data = source;
            result = data.Substring(
                data.IndexOf(sectionName)
                );
            if (source.Contains(parametrName))
            {
                result = result.Substring(result.IndexOf(parametrName + parametrSighs) + parametrName.Length + parametrSighs.Length);
                string endResult = "";
                foreach (char chars in result)
                {
                    if (acceptedSigns.Contains(chars))
                    {
                        endResult += chars;
                    }
                    else
                    {
                        break;
                    }
                }
                return endResult;
            }
            return "NaN";
        }
    }
}
