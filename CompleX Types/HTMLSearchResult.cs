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

namespace CompleX_Types
{
    class HTMLSearchResult
    {
        public static bool EnableTracing;
        private Stack<string> tagStack;

        public HTMLSearchResult()
        {
            tagStack = null;
            sSearchResult = "";
            sTagAttribute = "";
        }
        public HTMLSearchResult(string htmlData)
        {
            tagStack = null;
            sSearchResult = htmlData;
            sTagAttribute = "";
        }
        private string sSearchResult;
        /// <summary>
        /// Represents the TAG data for the last searched TAG.
        /// </summary>
        public string TAGData
        {
            get { return sSearchResult; }
        }
        private string sTagAttribute;
        /// <summary>
        /// Represents the TAG attribute for the last searched TAG.
        /// </summary>
        public string TagAttribute
        {
            get { return sTagAttribute; }
        }

        private static HTMLSearchResult Result(string str)
        {
            var ret = new HTMLSearchResult {sSearchResult = str};
            return ret;
        }
        public HTMLSearchResult GetTagDataBetweenStrings(string sFileData, string sSearchStartText, string sSearchEndText)
        {
            string sResult = "";
            int nPos1 = sFileData.IndexOf(sSearchStartText);
            if (nPos1 >= 0)
            {
                int nPos2 = sFileData.IndexOf(sSearchEndText);
                if (nPos2 > nPos1 + sSearchStartText.Length)
                {
                    sResult = sFileData.Substring(nPos1 + sSearchStartText.Length, nPos2 - 1);
                }
            }
            return Result(sResult);
        }
        /// <summary>
        ///Extracts the first occurance of specified tag from the current HTML data.
        /// <param name="sSearchTag">TAG to search.</param>
        /// <returns>Returns an HTMLSearchResult object containing the tag data.</returns>
        /// </summary>
        public HTMLSearchResult GetTagData(string sSearchTag)
        {
            return GetTagData(sSearchResult, sSearchTag, 1);
        }
        /// <summary>
        ///Extracts the Nth occurance of specified tag from the current HTML data.
        /// <param name="sSearchTag">TAG to search</param>
        /// <param name="nOccurance">The Nth occurance to search for</param>
        /// <returns>Returns an HTMLSearchResult object containing the tag data.</returns>
        /// </summary>

        public HTMLSearchResult GetTagData(string sSearchTag, int nOccurance)
        {
            return GetTagData(sSearchResult, sSearchTag, nOccurance);
        }

        //This function returns the html between given two search strings
        /// <summary>
        ///Extracts the Nth occurance of specified tag from the current HTML data.
        /// <param name="sFileData">The HTML page data, that contains the entire html page</param>
        /// <param name="sSearchTag">TAG to search</param>
        /// <param name="nOccurance">The Nth occurance to search for</param>
        /// <returns>Returns an HTMLSearchResult object containing the tag data.</returns>
        /// </summary>

        public HTMLSearchResult GetTagData(string sFileData, string sSearchTag, int nOccurance)
        {
            #region General_Variables_&_Validation
            string sTagData = "";
            int nStartPos = -1;

            tagStack = new Stack<string>();
            int nFoundOccurance = 0; // keep track of the no of instances we found, of the search tag.
            int nLen = sFileData.Length;
            int nLevel = 0;
            bool bFound = false;

            tagStack.Clear();

            if (nLen < 1)
            {
                throw (new ArgumentNullException("File Data cannot be null or blank string"));
            }
            if (sSearchTag.Length < 1)
            {
                throw (new ArgumentNullException("Search Tag cannot be null or blank string"));
            }
            if (nOccurance < 1)
            {
                throw (new ArgumentOutOfRangeException("The occurance number cannot be less than zero."));
            }
            //--------------------START THE SEARCH-----------------
            #endregion
            try
            {
                nLen = nLen - sSearchTag.Length + 1; //the part where we can compare
                for (int i = 0; i < nLen; i++)
                {
                    if (bFound == false)
                    {
                        if (sFileData[i] == '<' && sFileData[i + 1] != '/' && sFileData[i + 1] != '!')  //found some tag...
                        {
                            i++;
                            sTagAttribute = ""; //its class member.
                            string sTagName = ReadTillEndOfTag(sFileData, ref i, out sTagAttribute);
                            if (string.Compare(sSearchTag, sTagName, true) == 0)
                            {
                                bFound = true;
                                nStartPos = i+1;//Need to fix this...
                            }
                        }
                        else if (sFileData[i] == '<' && sFileData[i + 1] == '/') //end tag found
                        {
                            string sEndTag = "";
                            i += 2;
                            while (sFileData[i] != '>')
                                sEndTag += sFileData[i++];

                            if (String.Compare(sEndTag, sSearchTag, true) == 0 && nLevel == 0)
                            {
                                throw (new Exception("The Start tag was not found, however its end tag was found"));
                            }

                        }
                        else
                        {
                            continue;
                        }
                    } //bFound==false
                    else
                    {
                        if (sFileData[i] == '<' && sFileData[i + 1] != '/' && sFileData[i + 1] != '!')  //found some tag...
                        {
                            i++;
                            string attribute;
                            string sTagName = ReadTillEndOfTag(sFileData, ref i, out attribute);
                            if (String.Compare(sTagName, "script", true) == 0)
                            {
                                int k =i+1;
                                while (i < sFileData.Length)
                                {
                                    string substr = sFileData.Substring(k, 9);
                                    if (String.Compare(substr, "</script>", true) == 0)
                                    {
                                        i = k+9;
                                        break;
                                    }
                                    k++;
                                }
                            }
                            else if (String.Compare(sTagName, "input", true) != 0 && String.Compare(sTagName, "link", true) != 0 && String.Compare(sTagName, "br", true) != 0 && String.Compare(sTagName, "meta", true) != 0 && String.Compare(sTagName, "img", true) != 0)
                            {
                                tagStack.Push(sTagName);
                                nLevel++;
                            }

                        }
                        else if (sFileData[i] == '<' && sFileData[i + 1] == '/')  //end tag found
                        {
                            int nLastCharPos = i - 1;
                            string sEndTag = "";
                            i += 2;
                            while (sFileData[i] != '>')
                                sEndTag += sFileData[i++];
                            if (String.Compare(sEndTag, sSearchTag, true) == 0 && nLevel == 0)
                            {
                                nFoundOccurance++;
                                int nEndPos = nLastCharPos;
                                bFound = false;

                                if (nFoundOccurance == nOccurance)
                                {
                                    sTagData = sFileData.Substring(nStartPos, nEndPos - nStartPos + 1);
                                    //Console.Write("\nSearch String\n======================================" + sTAGData + "\n==============================");
                                    break;
                                }
                                //break;
                            }
                            else
                            {
                                string sPopedTag = tagStack.Pop();
                                if (string.Compare(sPopedTag, sEndTag, true) != 0)
                                {
                                    //throw (new Exception("Unknown tag end"));
                                    tagStack.Push(sPopedTag);
                                    nLevel++;
                                }
                                nLevel--;
                            }
                        }
                        else
                        {
                            continue;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            var result = new HTMLSearchResult();
            result.sSearchResult = sTagData;
            return result;
        }

        private string ReadTillEndOfTag(string sFileData, ref int i, out string sTagAttribute)
        {
            string sTagName = "";
            sTagAttribute = "";

            //first skip all white spaces...

            while (sFileData[i] == ' ' || sFileData[i] == '\n' || sFileData[i] == '\r' || sFileData[i] == '\t' || sFileData[i] == '\v')
            {
                if (sFileData[i] == '>')
                    throw (new ArgumentException("The file data doesn't start a valid tag"));

                i++;
            }
            if (i >= sFileData.Length)
            {
                return sTagName;
            }
            //now read the TAG name
            while (sFileData[i] != '>' && sFileData[i] != ' ' && sFileData[i] != '\n' && sFileData[i] != '\r' && sFileData[i] != '\t' && sFileData[i] != '\v')
            {
                sTagName += sFileData[i++];
            }
            //Tag Name extracted...

            //Now read the Tag attribute...
            while (sFileData[i] != '>')
            {
                sTagAttribute += sFileData[i++];
            }
            return sTagName;
        }
    }
}