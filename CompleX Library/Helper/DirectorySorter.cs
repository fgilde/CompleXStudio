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
using System.Text;
using System.Collections;
using System.IO;

namespace CompleX_Library.Helper
{
    public class DirectorySorter : IComparer
    {
        public int Compare(object x, object y)
        {
            DirectoryInfo dir1 = (DirectoryInfo)x;
            DirectoryInfo dir2 = (DirectoryInfo)y;
            return dir1.Name.CompareTo(dir2.Name);
        }
    }

    public class FileSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            FileInfo file1 = (FileInfo)x;
            FileInfo file2 = (FileInfo)y;
            return file1.Name.CompareTo(file2.Name);
        }
    }
}