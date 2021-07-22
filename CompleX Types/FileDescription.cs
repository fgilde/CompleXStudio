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
using CompleX_Library.Helper;

namespace CompleX_Types
{
    public class FileDescription
    {
        public FileDescription(string fileExtension)
        {
            string extensionName;
            Name = FileHelper.GetFileDescriptionByExtension(fileExtension, out extensionName);
            MimeType = FileHelper.GetMimeType(fileExtension);
            Extensions = new List<string> {fileExtension};
            ExtensionName = extensionName;
        }

        /// <summary>
        /// Name
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// MimeType
        /// </summary>
        /// <value>The type of the MIME.</value>
        public string MimeType { get; private set; }


        /// <summary>
        /// Gets or sets the name of the extension.
        /// </summary>
        /// <value>The name of the extension.</value>
        public string ExtensionName { get; private set; }

        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        public List<string> Extensions { get; set; }

        /// <summary>
        /// Returns this description as filtermask string for openfiledialogs
        /// </summary>
        /// <returns></returns>
        public string GetDialogFilter()
        {
            var filter = new StringBuilder();
            var extensions = new StringBuilder();
            foreach (string extension in Extensions)
                extensions.Append("*").Append(extension).Append(";");
            string mask = extensions.ToString().Remove(extensions.ToString().Length - 1, 1);

            filter.Append(Name).Append(" ").Append(MimeType);
            filter.Append(" (").Append(mask).Append(")|").Append(mask).Append("|");

            return filter.ToString().Remove(filter.ToString().Length - 1, 1);
        } 

    }

    public static class FileDescriptionHelper
    {
        /// <summary>
        /// Returns a List of FileDescription as filtermask string for openfiledialogs
        /// </summary>
        /// <param name="fileDescriptions">The file descriptions.</param>
        /// <returns></returns>
        public static string GetDialogFilter(this IEnumerable<FileDescription> fileDescriptions)
        {
            var filter = new StringBuilder();
            string filterstring = String.Empty;
            foreach (FileDescription description in fileDescriptions)
            {
                var extensions = new StringBuilder();
                foreach (string extension in description.Extensions)
                    extensions.Append("*").Append(extension).Append(";");
                string mask = extensions.ToString().Remove(extensions.ToString().Length - 1, 1);

                filter.Append(description.Name).Append(" ").Append(description.MimeType);
                filter.Append(" (").Append(mask).Append(")|").Append(mask).Append("|");

                filterstring = filter.ToString().Remove(filter.ToString().Length - 1, 1);
            }
            return filterstring;
        }
       
    }

}
