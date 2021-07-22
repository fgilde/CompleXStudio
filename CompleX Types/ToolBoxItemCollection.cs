//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary; 

namespace CompleX_Types
{
    [Serializable]
    public class ToolBoxItemCollection
    {
        public IEnumerable<ToolBoxItem> Items;

        public bool Serialize(string filename)
        {
            var fileStream = new FileStream(filename, FileMode.Create);
            var binFormatter = new BinaryFormatter();
            binFormatter.Serialize(fileStream, this);
            fileStream.Close();
            return File.Exists(filename);
        }

        public static ToolBoxItemCollection Deserialize(string filename)
        {
            var binFormatter = new BinaryFormatter();
            var fs = new FileStream(filename, FileMode.Open);
            var result = (ToolBoxItemCollection)binFormatter.Deserialize(fs);
            return result;
        }

    }
}
