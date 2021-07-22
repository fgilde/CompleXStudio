//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CompleX_Library.Helper
{
    public static class SerializationHelper
    {
        public static T CloneObject<T>(this T o)
        {
            MethodInfo method = o.GetType().GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
            if (method != null)
                return (T)method.Invoke(o, null);
            return o;
        }

        public static T XmlDeserialize<T>(string filename)
        {
            T result;
            if(TryXmlDeserialize(filename, out result))
                return result;
            return default(T);
        }
        public static bool TryXmlDeserialize<T>(string filename, out T result)
        {
            result = default(T);
            if (File.Exists(filename))
            {
                var fileStream = new FileStream(filename, FileMode.Open);
                try
                {
                    try
                    {
                        var serializer = new XmlSerializer(typeof(T));
                        result = (T)serializer.Deserialize(fileStream);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                finally
                {
                    fileStream.Close();
                }
            }
            return false;
        }

        public static bool XmlSerialize<T>(this T content, string filename)
        {
           return TryXmlSerialize( content, filename);
        }
        public static bool TryXmlSerialize<T>(this T content,string filename)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
            
            var fileStream = new FileStream(filename, FileMode.Create);
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(fileStream, content);
            }
            finally
            {
                fileStream.Close();
            }

            return File.Exists(filename);
        }

        public static T BinaryDeserialize<T>(string filename)
        {
            T result;
            if (TryBinaryDeserialize(filename, out result))
                return result;
            return default(T);
        }
        public static bool TryBinaryDeserialize<T>(string filename, out T result)
        {
            result = default(T);
            if (File.Exists(filename))
            {
                var fileStream = new FileStream(filename, FileMode.Open);
                try
                {
                    var serializer = new BinaryFormatter();
                    result = (T)serializer.Deserialize(fileStream);
                    return true;
                }
                finally
                {
                    fileStream.Close();
                }
            }
            return false;
        }

        public static bool BinarySerialize<T>(this T content, string filename)
        {
            return TryBinarySerialize(content, filename);
        }
        public static bool TryBinarySerialize<T>(this T content, string filename)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
                Directory.CreateDirectory(Path.GetDirectoryName(filename));

            var fileStream = new FileStream(filename, FileMode.Create);
            try
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(fileStream, content);
            }
            finally
            {
                fileStream.Close();
            }

            return File.Exists(filename);
        }



    }
}
