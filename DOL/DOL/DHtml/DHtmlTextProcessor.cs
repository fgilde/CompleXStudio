/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DHtmlTextProcessor Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DHtmlTextProcessor.cs: implementation of the DHtmlTextProcessor class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

namespace DOL.DHtml
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class DHtmlTextProcessor
	{

    /////////////////////////////////////////////////////////////////////////////////
    #region XHTML 有效字元之操作
        
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 檢查字元是否為 XHTML 有效字元
        /// </summary>
        /// <param name="unicode"></param>
        /// <returns></returns>
        public static bool IsValidXHtmlUnicode(char unicode)
        {
            bool result = false;
            if(!(('\u0000' <= unicode && unicode <= '\u0008') ||
                 ('\u000B' <= unicode && unicode <= '\u000C') ||
                 ('\u000E' <= unicode && unicode <= '\u001F') ||
                 ('\uD800' <= unicode && unicode <= '\uDFFF') ||
                 ('\uFFFE' <= unicode && unicode <= '\uFFFF')))
                result = true;

            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 檢查字串是否為 XHTML 有效字串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidXHtmlString(string str)
        {
            System.Diagnostics.Debug.Assert(str != null);

            bool result = true;
            StringReader reader = new StringReader(str);

            int read = reader.Read();
            while(read != -1)			
            {
                if(!IsValidXHtmlUnicode((char)read))
                {
                    result = false;
                    break;
                }

                read = reader.Read();
            }

            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 轉換字串至 XHTML 有效字串, 無效字元將轉換成 XHTML 參考
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TransformValidXHtmlString(string str)
        {
            System.Diagnostics.Debug.Assert(str != null);

            StringReader reader = new StringReader(str);
            StringBuilder builder = new StringBuilder(1024);

            int read = reader.Read();
            while(read != -1)			
            {
                if(IsValidXHtmlUnicode((char)read))
                    builder.Append((char)read); // 字元
                else builder.Append("&#" + ((int)read).ToString() + ";"); // 參考

                read = reader.Read();
            }

            string result = builder.ToString();
            return result;
        }
        
    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region HTML 與純文字之轉換

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
        public static string TranHtmlTextToStr(string str)
		{
            System.Diagnostics.Debug.Assert(str != null);
            StringBuilder builder = new StringBuilder(1024);

            int length = str.Length;
            int index = 0;
            while(index < length)
			{
                int newIndex = str.IndexOf('&', index);
                if(newIndex == -1)
                {
                    builder.Append(str.Substring(index));
                    break;
                }
                else // 參照: 參照的起始邊界條件 "&", 參照的結束邊界條件 ";"
                {
                    builder.Append(str.Substring(index, newIndex - index));
                    index = newIndex;

                    newIndex = str.IndexOf(';', index); // 找尋參照的結束邊界條件 ";"

                    if(newIndex == -1) // 找不到結束邊界條件則全部視為一般文字
                    {
                        builder.Append(str.Substring(index));
                        break;
                    }
                    else
                    {
                        string token = str.Substring(index, newIndex - index + 1);
                        if(token[1] == '#') // 字元參照
                        {
                            int intValue = int.Parse(token.Substring(2, token.Length - 3));
                            builder.Append((char)intValue);
                        }
                        else  // 實體參照
                        {
                            char referenceValue = TranRefEntityToChar(token);
                            if(referenceValue != 0) builder.Append(referenceValue);
                        }

                        index = newIndex;
                    }                     

                    ++index;
                }
			}

            string result = builder.ToString();
            return result;
		}

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
        public static string TranStrToHtmlText(string str)
		{
            System.Diagnostics.Debug.Assert(str != null);
            StringBuilder builder = new StringBuilder(1024);

            bool spaceTransform = false;
            for(int index = 0, count = str.Length; index < count; ++index)
            {
                char character = str[index];
                if('\x0009' == character || '\x000A' == character || '\x000D' == character || '\x0020' == character)
                {
                    if(spaceTransform == false)
                    {
                        spaceTransform = true;
                        builder.Append(' '); // 保留第一個空白, 並且轉換成 ASCII 32  
                    }
                    else builder.Append("&nbsp;"); // 第二個以後的空白轉換成 &nbsp;
                }
                else
                {
                    spaceTransform = false;
                    builder.Append(TranCharToRefEntity(character));
                }
            }

            string result = builder.ToString();
            return result;
		}

   		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
        public static char TranRefEntityToChar(string refEntity)
		{
            System.Diagnostics.Debug.Assert(refEntity != null);

			char result = ((char)0);
            switch(refEntity)
			{
                case "&amp;":
                    result = ('&');
                    break;
                case "&nbsp;":
                    result = (' ');
                    break;
				case "&lt;":
					result = ('<');
					break;
				case "&gt;":
					result = ('>');
					break;
				case "&quot;":
					result = ('\"');
					break;
				case "&Aacute;":
					result = ((char)193);
					break;
				case "&aacute;":
					result = ((char)225);
					break;
				case "&Acirc;":
					result = ((char)194);
					break;
				case "&acirc;":
					result = ((char)226);
					break;
				case "&acute;":
					result = ((char)180);
					break;
				case "&AElig;":
					result = ((char)198);
					break;
				case "&aelig;":
					result = ((char)230);
					break;
				case "&Agrave;":
					result = ((char)192);
					break;
				case "&agrave;":
					result = ((char)224);
					break;
				case "&alefsym;":
					result = ((char)8501);
					break;
				case "&Alpha;":
					result = ((char)913);
					break;
				case "&alpha;":
					result = ((char)945);
					break;
				case "&and;":
					result = ((char)8743);
					break;
				case "&ang;":
					result = ((char)8736);
					break;
				case "&Aring;":
					result = ((char)197);
					break;
				case "&aring;":
					result = ((char)229);
					break;
				case "&asymp;":
					result = ((char)8776);
					break;
				case "&Atilde;":
					result = ((char)195);
					break;
				case "&atilde;":
					result = ((char)227);
					break;
				case "&Auml;":
					result = ((char)196);
					break;
				case "&auml;":
					result = ((char)228);
					break;
				case "&bdquo;":
					result = ((char)8222);
					break;
				case "&Beta;":
					result = ((char)914);
					break;
				case "&beta;":
					result = ((char)946);
					break;
				case "&brvbar;":
					result = ((char)166);
					break;
				case "&bull;":
					result = ((char)8226);
					break;
				case "&cap;":
					result = ((char)8745);
					break;
				case "&Ccedil;":
					result = ((char)199);
					break;
				case "&ccedil;":
					result = ((char)231);
					break;
				case "&cedil;":
					result = ((char)184);
					break;
				case "&cent;":
					result = ((char)162);
					break;
				case "&Chi;":
					result = ((char)935);
					break;
				case "&chi;":
					result = ((char)967);
					break;
				case "&circ;":
					result = ((char)710);
					break;
				case "&clubs;":
					result = ((char)9827);
					break;
				case "&cong;":
					result = ((char)8773);
					break;
				case "&copy;":
					result = ((char)169);
					break;
				case "&crarr;":
					result = ((char)8629);
					break;
				case "&cup;":
					result = ((char)8746);
					break;
				case "&curren;":
					result = ((char)164);
					break;
				case "&dagger;":
					result = ((char)8224);
					break;
				case "&Dagger;":
					result = ((char)8225);
					break;
				case "&darr;":
					result = ((char)8595);
					break;
				case "&dArr;":
					result = ((char)8659);
					break;
				case "&deg;":
					result = ((char)176);
					break;
				case "&Delta;":
					result = ((char)916);
					break;
				case "&delta;":
					result = ((char)948);
					break;
				case "&diams;":
					result = ((char)9830);
					break;
				case "&divide;":
					result = ((char)247);
					break;
				case "&Eacute;":
					result = ((char)201);
					break;
				case "&eacute;":
					result = ((char)233);
					break;
				case "&Ecirc;":
					result = ((char)202);
					break;
				case "&ecirc;":
					result = ((char)234);
					break;
				case "&Egrave;":
					result = ((char)200);
					break;
				case "&egrave;":
					result = ((char)232);
					break;
				case "&empty;":
					result = ((char)8709);
					break;
				case "&emsp;":
					result = ((char)8195);
					break;
                case "&ensp;":
                    result = ((char)8194);
                    break;
				case "&Epsilon;":
					result = ((char)917);
					break;
				case "&epsilon;":
					result = ((char)949);
					break;
				case "&equiv;":
					result = ((char)8801);
					break;
				case "&Eta;":
					result = ((char)919);
					break;
				case "&eta;":
					result = ((char)951);
					break;
				case "&ETH;":
					result = ((char)208);
					break;
				case "&eth;":
					result = ((char)240);
					break;
				case "&Euml;":
					result = ((char)203);
					break;
				case "&euml;":
					result = ((char)235);
					break;
				case "&euro;":
					result = ((char)128);
					break;
				case "&exist;":
					result = ((char)8707);
					break;
				case "&fnof;":
					result = ((char)402);
					break;
				case "&forall;":
					result = ((char)8704);
					break;
				case "&frac12;":
					result = ((char)189);
					break;
				case "&frac14;":
					result = ((char)188);
					break;
				case "&frac34;":
					result = ((char)190);
					break;
				case "&fras1;":
					result = ((char)8260);
					break;
				case "&Gamma;":
					result = ((char)915);
					break;
				case "&gamma;":
					result = ((char)947);
					break;
				case "&ge;":
					result = ((char)8805);
					break;
				case "&harr;":
					result = ((char)8596);
					break;
				case "&hArr;":
					result = ((char)8660);
					break;
				case "&hearts;":
					result = ((char)9829);
					break;
				case "&hellip;":
					result = ((char)8230);
					break;
				case "&Iacute;":
					result = ((char)205);
					break;
				case "&iacute;":
					result = ((char)237);
					break;
				case "&Icirc;":
					result = ((char)206);
					break;
				case "&icirc;":
					result = ((char)238);
					break;
				case "&iexcl;":
					result = ((char)161);
					break;
				case "&Igrave;":
					result = ((char)204);
					break;
				case "&igrave;":
					result = ((char)236);
					break;
				case "&image;":
					result = ((char)8465);
					break;
				case "&infin;":
					result = ((char)8734);
					break;
				case "&int;":
					result = ((char)8747);
					break;
				case "&Iota;":
					result = ((char)921);
					break;
				case "&iota;":
					result = ((char)953);
					break;
				case "&iquest;":
					result = ((char)191);
					break;
				case "&isin;":
					result = ((char)8712);
					break;
				case "&Iuml;":
					result = ((char)207);
					break;
				case "&iuml;":
					result = ((char)239);
					break;
				case "&Kappa;":
					result = ((char)922);
					break;
				case "&kappa;":
					result = ((char)954);
					break;
				case "&Lambda;":
					result = ((char)923);
					break;
				case "&lambda;":
					result = ((char)955);
					break;
				case "&lang;":
					result = ((char)9001);
					break;
				case "&laquo;":
					result = ((char)171);
					break;
				case "&larr;":
					result = ((char)8592);
					break;
				case "&lArr;":
					result = ((char)8656);
					break;
				case "&lceil;":
					result = ((char)8968);
					break;
				case "&ldquo;":
					result = ((char)8220);
					break;
				case "&le;":
					result = ((char)8804);
					break;
				case "&lfloor;":
					result = ((char)8970);
					break;
				case "&lowast;":
					result = ((char)8727);
					break;
				case "&loz;":
					result = ((char)9674);
					break;
				case "&lrm;":
					result = ((char)8206);
					break;
				case "&lsaquo;":
					result = ((char)8249);
					break;
				case "&lsquo;":
					result = ((char)8216);
					break;
				case "&macr;":
					result = ((char)175);
					break;
				case "&mdash;":
					result = ((char)8212);
					break;
				case "&micro;":
					result = ((char)181);
					break;
				case "&middot;":
					result = ((char)183);
					break;
				case "&minus;":
					result = ((char)8722);
					break;
				case "&Mu;":
					result = ((char)924);
					break;
				case "&mu;":
					result = ((char)956);
					break;
				case "&nabla;":
					result = ((char)8711);
					break;
				case "&ndash;":
					result = ((char)8211);
					break;
				case "&ne;":
					result = ((char)8800);
					break;
				case "&ni;":
					result = ((char)8715);
					break;
				case "&not;":
					result = ((char)172);
					break;
				case "&notin;":
					result = ((char)8713);
					break;
				case "&nsub;":
					result = ((char)8836);
					break;
				case "&Ntilde;":
					result = ((char)209);
					break;
				case "&ntilde;":
					result = ((char)241);
					break;
				case "&Nu;":
					result = ((char)925);
					break;
				case "&nu;":
					result = ((char)957);
					break;
				case "&Oacute;":
					result = ((char)211);
					break;
				case "&oacute;":
					result = ((char)243);
					break;
				case "&Ocirc;":
					result = ((char)212);
					break;
				case "&ocirc;":
					result = ((char)244);
					break;
				case "&OElig;":
					result = ((char)338);
					break;
				case "&oelig;":
					result = ((char)339);
					break;
				case "&Ograve;":
					result = ((char)210);
					break;
				case "&ograve;":
					result = ((char)242);
					break;
				case "&oline;":
					result = ((char)8254);
					break;
				case "&Omega;":
					result = ((char)937);
					break;
				case "&omega;":
					result = ((char)969);
					break;
				case "&Omicron;":
					result = ((char)927);
					break;
				case "&omicron;":
					result = ((char)959);
					break;
				case "&oplus;":
					result = ((char)8853);
					break;
				case "&or;":
					result = ((char)8744);
					break;
				case "&ordf;":
					result = ((char)170);
					break;
				case "&ordm;":
					result = ((char)186);
					break;
				case "&Oslash;":
					result = ((char)216);
					break;
				case "&oslash;":
					result = ((char)248);
					break;
				case "&Otilde;":
					result = ((char)213);
					break;
				case "&otilde;":
					result = ((char)245);
					break;
				case "&otimes;":
					result = ((char)8855);
					break;
				case "&Ouml;":
					result = ((char)214);
					break;
				case "&ouml;":
					result = ((char)246);
					break;
				case "&para;":
					result = ((char)182);
					break;
				case "&part;":
					result = ((char)8706);
					break;
				case "&permil;":
					result = ((char)8240);
					break;
				case "&perp;":
					result = ((char)8869);
					break;
				case "&Phi;":
					result = ((char)934);
					break;
				case "&phi;":
					result = ((char)966);
					break;
				case "&Pi;":
					result = ((char)928);
					break;
				case "&pi;":
					result = ((char)960);
					break;
				case "&piv;":
					result = ((char)982);
					break;
				case "&plusmn;":
					result = ((char)177);
					break;
				case "&pound;":
					result = ((char)163);
					break;
				case "&prime;":
					result = ((char)8242);
					break;
				case "&Prime;":
					result = ((char)8243);
					break;
				case "&prod;":
					result = ((char)8719);
					break;
				case "&prop;":
					result = ((char)8733);
					break;
				case "&Psi;":
					result = ((char)936);
					break;
				case "&psi;":
					result = ((char)968);
					break;
				case "&radic;":
					result = ((char)8730);
					break;
				case "&rang;":
					result = ((char)9002);
					break;
				case "&raquo;":
					result = ((char)187);
					break;
				case "&rarr;":
					result = ((char)8594);
					break;
				case "&rArr;":
					result = ((char)8658);
					break;
				case "&rceil;":
					result = ((char)8969);
					break;
				case "&rdquo;":
					result = ((char)8221);
					break;
				case "&real;":
					result = ((char)8476);
					break;
				case "&reg;":
					result = ((char)174);
					break;
				case "&rfloor;":
					result = ((char)8971);
					break;
				case "&Rho;":
					result = ((char)929);
					break;
				case "&rho;":
					result = ((char)961);
					break;
				case "&rlm;":
					result = ((char)8207);
					break;
				case "&rsaquo;":
					result = ((char)8250);
					break;
				case "&rsquo;":
					result = ((char)8217);
					break;
				case "&sbquo;":
					result = ((char)8218);
					break;
				case "&Scaron;":
					result = ((char)352);
					break;
				case "&scaron;":
					result = ((char)353);
					break;
				case "&sdot;":
					result = ((char)8901);
					break;
				case "&sect;":
					result = ((char)167);
					break;
				case "&shy;":
					result = ((char)173);
					break;
				case "&Sigma;":
					result = ((char)931);
					break;
				case "&sigma;":
					result = ((char)963);
					break;
				case "&sigmaf;":
					result = ((char)962);
					break;
				case "&sim;":
					result = ((char)8764);
					break;
				case "&spades;":
					result = ((char)9824);
					break;
				case "&sub;":
					result = ((char)8834);
					break;
				case "&sube;":
					result = ((char)8838);
					break;
				case "&sum;":
					result = ((char)8721);
					break;
				case "&sup;":
					result = ((char)8835);
					break;
				case "&sup1;":
					result = ((char)185);
					break;
				case "&sup2;":
					result = ((char)178);
					break;
				case "&sup3;":
					result = ((char)179);
					break;
				case "&supe;":
					result = ((char)8839);
					break;
				case "&szlig;":
					result = ((char)223);
					break;
				case "&Tau;":
					result = ((char)932);
					break;
				case "&tau;":
					result = ((char)964);
					break;
				case "&there4;":
					result = ((char)8756);
					break;
				case "&Theta;":
					result = ((char)920);
					break;
				case "&theta;":
					result = ((char)952);
					break;
				case "&thetasym;":
					result = ((char)977);
					break;
				case "&thinsp;":
					result = ((char)8201);
					break;
				case "&THORN;":
					result = ((char)222);
					break;
				case "&thorn;":
					result = ((char)254);
					break;
				case "&tilde;":
					result = ((char)732);
					break;
				case "&times;":
					result = ((char)215);
					break;
				case "&trade;":
					result = ((char)8482);
					break;
				case "&Uacute;":
					result = ((char)218);
					break;
				case "&uacute;":
					result = ((char)250);
					break;
				case "&uarr;":
					result = ((char)8593);
					break;
				case "&uArr;":
					result = ((char)8657);
					break;
				case "&Ucirc;":
					result = ((char)219);
					break;
				case "&ucirc;":
					result = ((char)251);
					break;
				case "&Ugrave;":
					result = ((char)217);
					break;
				case "&ugrave;":
					result = ((char)249);
					break;
				case "&uml;":
					result = ((char)168);
					break;
				case "&upsih;":
					result = ((char)978);
					break;
				case "&Upsilon;":
					result = ((char)933);
					break;
				case "&upsilon;":
					result = ((char)965);
					break;
				case "&Uuml;":
					result = ((char)220);
					break;
				case "&uuml;":
					result = ((char)252);
					break;
				case "&weierp;":
					result = ((char)8472);
					break;
				case "&Xi;":
					result = ((char)926);
					break;
				case "&xi;":
					result = ((char)958);
					break;
				case "&Yacute;":
					result = ((char)221);
					break;
				case "&yacute;":
					result = ((char)253);
					break;
				case "&yen;":
					result = ((char)165);
					break;
				case "&Yuml;":
					result = ((char)376);
					break;
				case "&yuml;":
					result = ((char)255);
					break;
				case "&Zeta;":
					result = ((char)918);
					break;
				case "&zeta;":
					result = ((char)950);
					break;
				case "&zwj;":
					result = ((char)8205);
					break;
				case "&zwnj;":
					result = ((char)8204);
					break;

				default: break;
			}            

			return result;
		}

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
        public static string TranCharToRefEntity(char character)
		{
            string result = character.ToString();
            switch((int)character)
			{
				case '<':
					result = ("&lt;");
					break;
				case '>':
					result = ("&gt;");
					break;
				case '&':
					result = ("&amp;");
					break;
				case '\"':
					result = ("&quot;");
					break;                
				case 193:
					result = ("&Aacute;");
					break;
				case 225:
					result = ("&aacute;");
					break;
				case 194:
					result = ("&Acirc;");
					break;
				case 226:
					result = ("&acirc;");
					break;
				case 180:
					result = ("&acute;");
					break;
				case 198:
					result = ("&AElig;");
					break;
				case 230:
					result = ("&aelig;");
					break;
				case 192:
					result = ("&Agrave;");
					break;
				case 224:
					result = ("&agrave;");
					break;
				case 8501:
					result = ("&alefsym;");
					break;
				case 913:
					result = ("&Alpha;");
					break;
				case 945:
					result = ("&alpha;");
					break;
				case 8743:
					result = ("&and;");
					break;
				case 8736:
					result = ("&ang;");
					break;
				case 197:
					result = ("&Aring;");
					break;
				case 229:
					result = ("&aring;");
					break;
				case 8776:
					result = ("&asymp;");
					break;
				case 195:
					result = ("&Atilde;");
					break;
				case 227:
					result = ("&atilde;");
					break;
				case 196:
					result = ("&Auml;");
					break;
				case 228:
					result = ("&auml;");
					break;
				case 8222:
					result = ("&bdquo;");
					break;
				case 914:
					result = ("&Beta;");
					break;
				case 946:
					result = ("&beta;");
					break;
				case 166:
					result = ("&brvbar;");
					break;
				case 8226:
					result = ("&bull;");
					break;
				case 8745:
					result = ("&cap;");
					break;
				case 199:
					result = ("&Ccedil;");
					break;
				case 231:
					result = ("&ccedil;");
					break;
				case 184:
					result = ("&cedil;");
					break;
				case 162:
					result = ("&cent;");
					break;
				case 935:
					result = ("&Chi;");
					break;
				case 967:
					result = ("&chi;");
					break;
				case 710:
					result = ("&circ;");
					break;
				case 9827:
					result = ("&clubs;");
					break;
				case 8773:
					result = ("&cong;");
					break;
				case 169:
					result = ("&copy;");
					break;
				case 8629:
					result = ("&crarr;");
					break;
				case 8746:
					result = ("&cup;");
					break;
				case 164:
					result = ("&curren;");
					break;
				case 8224:
					result = ("&dagger;");
					break;
				case 8225:
					result = ("&Dagger;");
					break;
				case 8595:
					result = ("&darr;");
					break;
				case 8659:
					result = ("&dArr;");
					break;
				case 176:
					result = ("&deg;");
					break;
				case 916:
					result = ("&Delta;");
					break;
				case 948:
					result = ("&delta;");
					break;
				case 9830:
					result = ("&diams;");
					break;
				case 247:
					result = ("&divide;");
					break;
				case 201:
					result = ("&Eacute;");
					break;
				case 233:
					result = ("&eacute;");
					break;
				case 202:
					result = ("&Ecirc;");
					break;
				case 234:
					result = ("&ecirc;");
					break;
				case 200:
					result = ("&Egrave;");
					break;
				case 232:
					result = ("&egrave;");
					break;
				case 8709:
					result = ("&empty;");
					break;
				case 8195:
					result = ("&emsp;");
					break;
				case 8194:
					result = ("&ensp;");
					break;
				case 917:
					result = ("&Epsilon;");
					break;
				case 949:
					result = ("&epsilon;");
					break;
				case 8801:
					result = ("&equiv;");
					break;
				case 919:
					result = ("&Eta;");
					break;
				case 951:
					result = ("&eta;");
					break;
				case 208:
					result = ("&ETH;");
					break;
				case 240:
					result = ("&eth;");
					break;
				case 203:
					result = ("&Euml;");
					break;
				case 235:
					result = ("&euml;");
					break;
				case 128:
					result = ("&euro;");
					break;
				case 8707:
					result = ("&exist;");
					break;
				case 402:
					result = ("&fnof;");
					break;
				case 8704:
					result = ("&forall;");
					break;
				case 189:
					result = ("&frac12;");
					break;
				case 188:
					result = ("&frac14;");
					break;
				case 190:
					result = ("&frac34;");
					break;
				case 8260:
					result = ("&fras1;");
					break;
				case 915:
					result = ("&Gamma;");
					break;
				case 947:
					result = ("&gamma;");
					break;
				case 8805:
					result = ("&ge;");
					break;
				case 8596:
					result = ("&harr;");
					break;
				case 8660:
					result = ("&hArr;");
					break;
				case 9829:
					result = ("&hearts;");
					break;
				case 8230:
					result = ("&hellip;");
					break;
				case 205:
					result = ("&Iacute;");
					break;
				case 237:
					result = ("&iacute;");
					break;
				case 206:
					result = ("&Icirc;");
					break;
				case 238:
					result = ("&icirc;");
					break;
				case 161:
					result = ("&iexcl;");
					break;
				case 204:
					result = ("&Igrave;");
					break;
				case 236:
					result = ("&igrave;");
					break;
				case 8465:
					result = ("&image;");
					break;
				case 8734:
					result = ("&infin;");
					break;
				case 8747:
					result = ("&int;");
					break;
				case 921:
					result = ("&Iota;");
					break;
				case 953:
					result = ("&iota;");
					break;
				case 191:
					result = ("&iquest;");
					break;
				case 8712:
					result = ("&isin;");
					break;
				case 207:
					result = ("&Iuml;");
					break;
				case 239:
					result = ("&iuml;");
					break;
				case 922:
					result = ("&Kappa;");
					break;
				case 954:
					result = ("&kappa;");
					break;
				case 923:
					result = ("&Lambda;");
					break;
				case 955:
					result = ("&lambda;");
					break;
				case 9001:
					result = ("&lang;");
					break;
				case 171:
					result = ("&laquo;");
					break;
				case 8592:
					result = ("&larr;");
					break;
				case 8656:
					result = ("&lArr;");
					break;
				case 8968:
					result = ("&lceil;");
					break;
				case 8220:
					result = ("&ldquo;");
					break;
				case 8804:
					result = ("&le;");
					break;
				case 8970:
					result = ("&lfloor;");
					break;
				case 8727:
					result = ("&lowast;");
					break;
				case 9674:
					result = ("&loz;");
					break;
				case 8206:
					result = ("&lrm;");
					break;
				case 8249:
					result = ("&lsaquo;");
					break;
				case 8216:
					result = ("&lsquo;");
					break;
				case 175:
					result = ("&macr;");
					break;
				case 8212:
					result = ("&mdash;");
					break;
				case 181:
					result = ("&micro;");
					break;
				case 183:
					result = ("&middot;");
					break;
				case 8722:
					result = ("&minus;");
					break;
				case 924:
					result = ("&Mu;");
					break;
				case 956:
					result = ("&mu;");
					break;
				case 8711:
					result = ("&nabla;");
					break;
				case 160:
					result = ("&nbsp;");
                    break;
				case 8211:
					result = ("&ndash;");
					break;
				case 8800:
					result = ("&ne;");
					break;
				case 8715:
					result = ("&ni;");
					break;
				case 172:
					result = ("&not;");
					break;
				case 8713:
					result = ("&notin;");
					break;
				case 8836:
					result = ("&nsub;");
					break;
				case 209:
					result = ("&Ntilde;");
					break;
				case 241:
					result = ("&ntilde;");
					break;
				case 925:
					result = ("&Nu;");
					break;
				case 957:
					result = ("&nu;");
					break;
				case 211:
					result = ("&Oacute;");
					break;
				case 243:
					result = ("&oacute;");
					break;
				case 212:
					result = ("&Ocirc;");
					break;
				case 244:
					result = ("&ocirc;");
					break;
				case 338:
					result = ("&OElig;");
					break;
				case 339:
					result = ("&oelig;");
					break;
				case 210:
					result = ("&Ograve;");
					break;
				case 242:
					result = ("&ograve;");
					break;
				case 8254:
					result = ("&oline;");
					break;
				case 937:
					result = ("&Omega;");
					break;
				case 969:
					result = ("&omega;");
					break;
				case 927:
					result = ("&Omicron;");
					break;
				case 959:
					result = ("&omicron;");
					break;
				case 8853:
					result = ("&oplus;");
					break;
				case 8744:
					result = ("&or;");
					break;
				case 170:
					result = ("&ordf;");
					break;
				case 186:
					result = ("&ordm;");
					break;
				case 216:
					result = ("&Oslash;");
					break;
				case 248:
					result = ("&oslash;");
					break;
				case 213:
					result = ("&Otilde;");
					break;
				case 245:
					result = ("&otilde;");
					break;
				case 8855:
					result = ("&otimes;");
					break;
				case 214:
					result = ("&Ouml;");
					break;
				case 246:
					result = ("&ouml;");
					break;
				case 182:
					result = ("&para;");
					break;
				case 8706:
					result = ("&part;");
					break;
				case 8240:
					result = ("&permil;");
					break;
				case 8869:
					result = ("&perp;");
					break;
				case 934:
					result = ("&Phi;");
					break;
				case 966:
					result = ("&phi;");
					break;
				case 928:
					result = ("&Pi;");
					break;
				case 960:
					result = ("&pi;");
					break;
				case 982:
					result = ("&piv;");
					break;
				case 177:
					result = ("&plusmn;");
					break;
				case 163:
					result = ("&pound;");
					break;
				case 8242:
					result = ("&prime;");
					break;
				case 8243:
					result = ("&Prime;");
					break;
				case 8719:
					result = ("&prod;");
					break;
				case 8733:
					result = ("&prop;");
					break;
				case 936:
					result = ("&Psi;");
					break;
				case 968:
					result = ("&psi;");
					break;
				case 8730:
					result = ("&radic;");
					break;
				case 9002:
					result = ("&rang;");
					break;
				case 187:
					result = ("&raquo;");
					break;
				case 8594:
					result = ("&rarr;");
					break;
				case 8658:
					result = ("&rArr;");
					break;
				case 8969:
					result = ("&rceil;");
					break;
				case 8221:
					result = ("&rdquo;");
					break;
				case 8476:
					result = ("&real;");
					break;
				case 174:
					result = ("&reg;");
					break;
				case 8971:
					result = ("&rfloor;");
					break;
				case 929:
					result = ("&Rho;");
					break;
				case 961:
					result = ("&rho;");
					break;
				case 8207:
					result = ("&rlm;");
					break;
				case 8250:
					result = ("&rsaquo;");
					break;
				case 8217:
					result = ("&rsquo;");
					break;
				case 8218:
					result = ("&sbquo;");
					break;
				case 352:
					result = ("&Scaron;");
					break;
				case 353:
					result = ("&scaron;");
					break;
				case 8901:
					result = ("&sdot;");
					break;
				case 167:
					result = ("&sect;");
					break;
				case 173:
					result = ("&shy;");
					break;
				case 931:
					result = ("&Sigma;");
					break;
				case 963:
					result = ("&sigma;");
					break;
				case 962:
					result = ("&sigmaf;");
					break;
				case 8764:
					result = ("&sim;");
					break;
				case 9824:
					result = ("&spades;");
					break;
				case 8834:
					result = ("&sub;");
					break;
				case 8838:
					result = ("&sube;");
					break;
				case 8721:
					result = ("&sum;");
					break;
				case 8835:
					result = ("&sup;");
					break;
				case 185:
					result = ("&sup1;");
					break;
				case 178:
					result = ("&sup2;");
					break;
				case 179:
					result = ("&sup3;");
					break;
				case 8839:
					result = ("&supe;");
					break;
				case 223:
					result = ("&szlig;");
					break;
				case 932:
					result = ("&Tau;");
					break;
				case 964:
					result = ("&tau;");
					break;
				case 8756:
					result = ("&there4;");
					break;
				case 920:
					result = ("&Theta;");
					break;
				case 952:
					result = ("&theta;");
					break;
				case 977:
					result = ("&thetasym;");
					break;
				case 8201:
					result = ("&thinsp;");
					break;
				case 222:
					result = ("&THORN;");
					break;
				case 254:
					result = ("&thorn;");
					break;
				case 732:
					result = ("&tilde;");
					break;
				case 215:
					result = ("&times;");
					break;
				case 8482:
					result = ("&trade;");
					break;
				case 218:
					result = ("&Uacute;");
					break;
				case 250:
					result = ("&uacute;");
					break;
				case 8593:
					result = ("&uarr;");
					break;
				case 8657:
					result = ("&uArr;");
					break;
				case 219:
					result = ("&Ucirc;");
					break;
				case 251:
					result = ("&ucirc;");
					break;
				case 217:
					result = ("&Ugrave;");
					break;
				case 249:
					result = ("&ugrave;");
					break;
				case 168:
					result = ("&uml;");
					break;
				case 978:
					result = ("&upsih;");
					break;
				case 933:
					result = ("&Upsilon;");
					break;
				case 965:
					result = ("&upsilon;");
					break;
				case 220:
					result = ("&Uuml;");
					break;
				case 252:
					result = ("&uuml;");
					break;
				case 8472:
					result = ("&weierp;");
					break;
				case 926:
					result = ("&Xi;");
					break;
				case 958:
					result = ("&xi;");
					break;
				case 221:
					result = ("&Yacute;");
					break;
				case 253:
					result = ("&yacute;");
					break;
				case 165:
					result = ("&yen;");
					break;
				case 376:
					result = ("&Yuml;");
					break;
				case 255:
					result = ("&yuml;");
					break;
				case 918:
					result = ("&Zeta;");
					break;
				case 950:
					result = ("&zeta;");
					break;
				case 8205:
					result = ("&zwj;");
					break;
				case 8204:
					result = ("&zwnj;");
					break;
                
				default: break;
			}

			return result;
        }
        
    #endregion
        
    /////////////////////////////////////////////////////////////////////////////////
    #region HTML 字元之處理

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool EqualesOfAnyChar(char a, string b)
        {
            System.Diagnostics.Debug.Assert(b != null);            
            return b.IndexOf(a) != -1;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Comapct WSC in input text
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string ComapctWSCString(string str)
        {
            System.Diagnostics.Debug.Assert(str != null);
            StringBuilder builder = new StringBuilder(1024);

            int index = 0;
            int length = str.Length;
            while(index < length)
            {
                int newIndex = str.IndexOfAny(WhiteSpaceCharsArray, index);
                if(newIndex != -1)
                {
                    builder.Append(str.Substring(index, newIndex - index));
                    builder.Append(' ');
                    index = newIndex + 1;
                    while(index < length)
                    {
                        char temp = str[index];
                        if('\x0009' == temp || '\x000A' == temp || '\x000D' == temp || '\x0020' == temp)
                            ++index;
                        else break;
                    }
                }
                else 
                {
                    builder.Append(str.Substring(index));
                    break;
                }
            }

            string result = builder.ToString();
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string RemoveWSCString(string str)
        {
            System.Diagnostics.Debug.Assert(str != null);
            StringBuilder builder = new StringBuilder(1024);

            int index = 0;
            int length = str.Length;
            while(index < length)
            {
                int newIndex = str.IndexOfAny(WhiteSpaceCharsArray, index);
                if(newIndex != -1)
                {
                    builder.Append(str.Substring(index, newIndex - index));
                    index = newIndex + 1;
                    while(index < length)
                    {
                        char temp = str[index];
                        if('\x0009' == temp || '\x000A' == temp || '\x000D' == temp || '\x0020' == temp)
                            ++index;
                        else break;
                    }
                }
                else
                {
                    builder.Append(str.Substring(index));
                    break;
                }
            }

            string result = builder.ToString();
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ExistWhiteSpaceChar(string str)
        {
            System.Diagnostics.Debug.Assert(str != null);
            return str.IndexOfAny(WhiteSpaceCharsArray) != -1;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static bool IsWhiteSpaceChar(char temp)
        {
            bool result = false;
            if('\x0009' == temp || '\x000A' == temp || '\x000D' == temp || '\x0020' == temp)
                result = true;

            return result;
        }


        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static bool IsVaildAttribueNameChar(char temp)
        {
            bool result = false;
            if('a' <= temp && temp <= 'z' || 'A' <= temp && temp <= 'Z' || '0' <= temp && temp <= '9')
                result = true;

            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static bool IsVaildAttribueValueChar(char temp)
        {
            bool result = false;
            // The attribute value may only contain letters (a-z and A-Z), digits (0-9), 
            // hyphens (ASCII decimal 45), periods (ASCII decimal 46), underscores 
            // (ASCII decimal 95), and colons (ASCII decimal 58). 
            if('a' <= temp && temp <= 'z' || 'A' <= temp && temp <= 'Z' || '0' <= temp && temp <= '9' ||
                temp == '-' || temp == '.' || temp == ':' || temp == '_')                
                result = true;

            // Unofficial
            if(temp == '/' ||  temp == '#')
                result = true;

            return result;
        }

    #endregion 

    /////////////////////////////////////////////////////////////////////////////////
    #region 變數

        /// <summary>
        /// HTML 的空白字元
        /// </summary>
        public static readonly string WhiteSpaceChars = "\x0009\x000A\x000D\x0020";  // '\t', '\n', '\r', ' '
        /// <summary>
        /// HTML 的空白字元
        /// </summary>
        public static readonly char[] WhiteSpaceCharsArray = { '\x0009', '\x000A', '\x000D', '\x0020' };  // '\t', '\n', '\r', ' '

    #endregion

    }
}
