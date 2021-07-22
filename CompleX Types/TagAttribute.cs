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
using System.Xml.Linq;
using CompleX_Library;
using CompleX_Library.Helper;


namespace CompleX_Types
{
    /// <summary>
    /// AttributeType Enum
    /// </summary>
    [Serializable]
    public enum AttributeType
    {
        Text,
        Enumerated,
        Color,
        Relativepath,
        CssStyle,
        CssID,
        Style,
        Flag,
        Size, // width:src, height:src
    }


    /// <summary>
    /// TagAttribute Class
    /// </summary>
    [Serializable]
    public class TagAttribute : IDeepCloneable<TagAttribute>, ICloneable
    {
        /// <summary>
        /// Name of Attribute
        /// </summary>
        public string AtrributeName {get; set;}

        /// <summary>
        /// Value for attribute
        /// </summary>
        public string AtrributeValue { get; set; }

        /// <summary>
        /// onClick e.x ?
        /// </summary>
        public bool IsEventOrAction { get; set; }

        /// <summary>
        /// Type of Atrributevalue
        /// </summary>
        public AttributeType AttributeType { get; set;}

        /// <summary>
        /// Group 
        /// </summary>
        public string AttribCategoryGroup  { get; set; }

        /// <summary>
        /// Possible Values if type Enumerated
        /// </summary>
        public Dictionary<string, string> AttribOptions { get; set;}

        public TagAttribute(string name,string group)
        {
            AtrributeName = name;
            AttributeType = AttributeType.Text;
            AttribCategoryGroup = group;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xml"></param>
        public TagAttribute(XElement xml){
            AtrributeName = xml.Attribute("name") != null ? xml.Attribute("name").Value.ToLower() : String.Empty;
            AttributeType = xml.Attribute("type") != null ? GetAttributeTypeByName(xml.Attribute("type").Value): AttributeType.Text;

            if (AttributeType == AttributeType.Enumerated)
            {
                AttribOptions = new Dictionary<string, string>();
                var attriboption = xml.Descendants("attriboption");
                foreach (var item in attriboption)
                {
                    string key = item.GetAttribute("value");
                    string caption = item.GetAttribute("caption");
                    if(String.IsNullOrEmpty(caption) && !String.IsNullOrEmpty(key))
                        caption = key;

                    if(!String.IsNullOrEmpty(key) && !String.IsNullOrEmpty(caption))
                        AttribOptions.Add(key, caption);
                }
            }
 
        }


        /// <summary>
        /// Gets the type of the attribute.
        /// </summary>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns></returns>
        public static AttributeType GetAttributeTypeByName(string attributeType)
        {
            attributeType = attributeType.ToLower();
            switch (attributeType)
            {
                case "text": return  AttributeType.Text;
                case "color": return AttributeType.Color;
                case "enumerated": return AttributeType.Enumerated;
                case "relativepath": return AttributeType.Relativepath;
                case "cssstyle": return AttributeType.CssStyle;
                case "cssid": return AttributeType.CssID;
                case "style": return  AttributeType.Style;
                case "flag": return  AttributeType.Flag;
                case "size": return  AttributeType.Size;
                case "width:src": return AttributeType.Size;
                case "height:src": return AttributeType.Size;                
                case "width": return AttributeType.Size;
                case "height": return AttributeType.Size; 
            }
            return AttributeType.Text; 
        }

        /// <summary>
        /// Creates a Clone of the object
        /// </summary>
        /// <param name="settings">controls the special behaviour when cloning</param>
        /// <returns>the cloned object</returns>
        public TagAttribute DeepClone(CloneDeepSettings settings)
        {
            return CloneDeepFactory.DeepCloneImpl(this, settings);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public object Clone()
        {
            return CloneDeepFactory.DeepCloneImpl(this, null);
        }

    }


    /// <summary>
    /// TagAttributeHelper Class 
    /// </summary>
    public static class TagAttributeHelper
    {
        public static Type ToType(this AttributeType attributeType)
        {
            switch (attributeType)
            {
                case AttributeType.Color:
                    return typeof(System.Drawing.Color);
                case AttributeType.CssID:
                    return typeof(string);
                case AttributeType.CssStyle:
                    return typeof(string);
                case AttributeType.Enumerated:
                    return typeof(IEnumerable<string>);
                case AttributeType.Flag:
                    return typeof(bool);
                case AttributeType.Relativepath:
                    return typeof(string);
                case AttributeType.Size:
                    return typeof(string);
                case AttributeType.Style:
                    return typeof(string);
                case AttributeType.Text:
                    return typeof(string);
                default:
                    return typeof(string);
            }
        }
    }

}