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
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms.Design;
using System.Xml.Linq;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Settings.Constants;
using CompleX_Types.Extensions;

namespace CompleX_Types
{
    [Serializable]
    public class Tag: ICloneable, IDeepCloneable<Tag>
    {
          
        private string tagName;

        /// <summary>
        /// Tagname like (e.X input)
        /// </summary>
        public string TagName
        { 
            get
            {
                return tagName.Replace("##", ":");
            }set
            {
                tagName = value;
            }
        }

        /// <summary>
        /// Language like HTML, JSP etc
        /// </summary>
        public string TagLanguage { get; private set; }

        /// <summary>
        /// Value <a>Value</a> 
        /// </summary>
        public string TagValue { get; set; }

        /// <summary>
        /// Tagreference file (in html format)
        /// </summary>
        public string Helpfile
        {
            get
            {
                CultureInfo cultureInfo = CultureInfo.CurrentUICulture;
                string language = cultureInfo.Name;
                if (cultureInfo.Parent != null && !String.IsNullOrEmpty(cultureInfo.Parent.Name))
                    language = cultureInfo.Parent.Name;
                string file = Pathes.ApplicationPath + Pathes.REFERENCE.AddDirectorySeparatorChar() +
                              language.AddDirectorySeparatorChar() +
                              TagLanguage.AddDirectorySeparatorChar() + 
                              TagName.Replace(":","");
                if (File.Exists(file + ".html"))
                    return file + ".html";

                if (File.Exists(file + ".htm"))
                    return file + ".htm";

                return String.Empty;
            }
        }

        /// <summary>
        /// XML Descriptionfile
        /// </summary>
        public string VtmFile { get; private set; }

        /// <summary>
        /// Needs an tag
        /// </summary>
        public bool Endtag { get; private set; }

        /// <summary>
        /// List of all TagAttributes
        /// </summary>
        public List<TagAttribute> Attributes { get; set; }

        /// <summary>
        /// List of all events
        /// </summary>
        public List<string> Events { get; set; }

        /// <summary>
        /// Categories
        /// </summary>
        public List<string> AttribCategories { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="language"></param>
        /// <param name="tag"></param>
        public Tag(TagLanguage language, string tag)
        {
            TagLanguage = language.AsString();
            string tmpTag = tag;
            tag = tag.Replace("<", String.Empty).Replace(">", String.Empty).Replace("/", String.Empty);
            tag = language.AsString().AddDirectorySeparatorChar() + tag;
            tag = tag.Replace(":", "##");
            Attributes = new List<TagAttribute>();
            AttribCategories = new List<string>();
            Events = new List<string>();

            if (!File.Exists(tag))
            {
                tag = tag.Replace('/', Path.DirectorySeparatorChar);
                string dir = Pathes.ApplicationPath + Pathes.TAGLIBARY.AddDirectorySeparatorChar();
                VtmFile = dir + tag + ".vtm";
                if (!File.Exists(VtmFile))
                {
                    dir = Environment.CurrentDirectory.AddDirectorySeparatorChar() + Pathes.TAGLIBARY.AddDirectorySeparatorChar();
                    VtmFile = dir + tag + ".vtm";
                }
            }
            else
            {
                VtmFile = tag;
            }

            TagName = Path.GetFileNameWithoutExtension(tag);


            if (File.Exists(VtmFile))
                Parse();
            else
                throw new Exception("Tag \"" + tmpTag + "\" doesn't exists.");
        }


        /// <summary>
        /// Sets the value of a specified attribute
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <param name="addIfNotExists"></param>
        /// <returns></returns>
        public bool SetAttributeValue(string attributeName, string value, bool addIfNotExists)
        {
            TagAttribute orDefault =
               Attributes.FirstOrDefault(
                   tagAttribute => tagAttribute.AtrributeName.ToLower() == attributeName.ToLower());
            if (orDefault != null)
            {
                orDefault.AtrributeValue = value;
                return true;
            }
            if (addIfNotExists)
            {
                Attributes.Add(new TagAttribute(attributeName,"General") {AtrributeValue = value});
                return true;
            }
            return false;
        }



        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string s = "<" + TagName;

            IEnumerable<TagAttribute> attributes = Attributes.Where(attribute => !String.IsNullOrEmpty(attribute.AtrributeValue));
            s = attributes.Aggregate(s, (current, attribute) => current + (" " + attribute.AtrributeName + "=\"" + attribute.AtrributeValue + "\""));
            s += Endtag ? ">" + TagValue + "</" + TagName + ">" : " />";

            return s;

        }

        /// <summary>
        /// Creates a Clone of the object
        /// </summary>
        /// <param name="settings">controls the special behaviour when cloning</param>
        /// <returns>the cloned object</returns>
        public Tag DeepClone(CloneDeepSettings settings)
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


        /// <summary>
        /// Converts Tag to CustomClass
        /// </summary>
        /// <returns></returns>
        public CustomClass ToCustomClass()
        {
            var customClass = new CustomClass();

            customClass.AddProperty("Tag", TagName, String.Empty,String.Empty, TagName.GetType(),true,false);
            customClass.AddProperty("TagLanguage", TagLanguage, String.Empty, String.Empty, TagLanguage.GetType(), true, false);  
           
            if(Endtag)
               customClass.AddProperty("TagValue",TagValue,"The value for this Tag ",String.Empty,TagValue.GetType(),false,false);  
            

            foreach (TagAttribute attribute in Attributes)
            {

                string group = attribute.IsEventOrAction ? "Events" : attribute.AttribCategoryGroup;

                Type currentType = attribute.AttributeType != AttributeType.Enumerated ? attribute.AttributeType.ToType() : typeof(string);
                var possibleValues = Enumerable.Empty<string>();
                object value = attribute.AtrributeValue ?? String.Empty; // bei Flag den value zu bool machen, um im inspercor den wert zu setzen
                string description = attribute.AttributeType.ToString();

                if(currentType == typeof(bool))
                    value = !String.IsNullOrEmpty(value.ToString()) ? Convert.ToBoolean(value) : false;
               
                
                if (attribute.AttributeType == AttributeType.Enumerated && attribute.AttribOptions != null && attribute.AttribOptions.Values.Count > 0)
                    possibleValues =  attribute.AttribOptions.Keys.ToArray();

                if(attribute.AttributeType == AttributeType.Relativepath)
                {
                    // Wenn Attribute type relativ path ist, muss ein browse möglich sein daher UITypeEditor benutzen
                    customClass.AddProperty<FileNameEditor>(attribute.AtrributeName,value,description,group,currentType);
                    
                }else
                {
                    customClass.AddProperty(attribute.AtrributeName,value,description,group,currentType,false,false,possibleValues.ToArray());
                }

            }
            return customClass;
        }


        /// <summary>
        /// parse vtm file and fill TagInformations
        /// </summary>
        private void Parse()
        {
            XElement xml = XElement.Load(VtmFile);
            Endtag = xml.Attribute("endtag") != null && xml.Attribute("endtag").Value.ToLower() == "yes";

            var attributes = xml.Descendants("attributes").Descendants("attrib");
            var categories = xml.Descendants("attribcategories").Descendants("attribgroup");
            var events = xml.Descendants("attributes").Descendants("event");

            foreach (var e in events)
                Events.Add(e.GetAttribute("name"));

            foreach (var category in categories)
                AttribCategories.Add(category.GetAttribute("name"));

            if (AttribCategories.FirstOrDefault(s => s.Equals("General")) == default(string))
                AttribCategories.Add("General");  // Wenn keine gruppe für General existiert ein anlgen, falls ein Attribute in keiner Gruppe ist;
            
            Attributes = new List<TagAttribute>();

            foreach (var element in attributes)
            {
                var currentAttribute = new TagAttribute(element);

                #region Grouping 
                foreach (var category in categories)
                {
                    if (category.Attribute("elements") != null)
                    {
                        var list = category.Attribute("elements").Value.Split(',');
                        if(list.FirstOrDefault(s => s == currentAttribute.AtrributeName) != default(string))
                        {
                            currentAttribute.AttribCategoryGroup = category.GetAttribute("name");
                        }
                        if(String.IsNullOrEmpty( currentAttribute.AttribCategoryGroup ))
                        {
                            currentAttribute.AttribCategoryGroup = "General";
                        }
                    }

                }
                #endregion

                var eventName = Events.FirstOrDefault(s => s.ToLower().Equals(currentAttribute.AtrributeName.ToLower()));
                if (eventName != null)
                {
                    currentAttribute.AtrributeName = eventName;
                    currentAttribute.IsEventOrAction = true;
                }


                Attributes.Add(currentAttribute);
            }

            foreach (TagAttribute tagAttribute in Attributes)
            {
                if(String.IsNullOrEmpty(tagAttribute.AttribCategoryGroup))
                {
                    tagAttribute.AttribCategoryGroup = "General";
                }
            }

            if (Attributes.Where(attribute => attribute.AttribCategoryGroup.Equals("General") && !attribute.IsEventOrAction).ToList().Count <= 0)
                AttribCategories.Remove("General"); // Wenn sich kein Attribut in der Gruppe "General" befindet, diese wieder entfernen;

        }

        /// <summary>
        /// Creates tag from string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Tag FromString(string s)
        {
           return s.ToTag();
        }

        /// <summary>
        /// Creates a new instance from customClass 
        /// (Converts customclass to Tag)
        /// </summary>
        /// <param name="customClass"></param>
        /// <returns></returns>
        public static Tag CreateFromCustomClass(CustomClass customClass)
        {
            PropertyDescriptorCollection properties = customClass.GetProperties();
            PropertyDescriptor nameProp = properties["Tag"];
            PropertyDescriptor langProp = properties["TagLanguage"];
            if (nameProp != null && langProp != null)
            {
                string name = nameProp.GetValue(customClass).ToString();
                string lang = langProp.GetValue(customClass).ToString();
                if(!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(lang))
                {
                    try
                    {
                        var result = new Tag(lang.AsTagLanguage(),name);
                        if(result.Endtag)
                            result.TagValue = properties["TagValue"].GetValue(customClass).ToString();                            
                        
                        foreach (CustomClass.DynamicProperty property in properties)
                        {
                            CustomClass.DynamicProperty dynamicProperty = property;
                            object value = property.GetValue(customClass);
                            TagAttribute attribute =
                                result.Attributes.FirstOrDefault(
                                    tagAttribute => tagAttribute.AtrributeName.ToLower() == dynamicProperty.Name.ToLower());
                            if (attribute != null)
                            {
                                if (property.PropertyType == typeof(bool) && !(bool)value)  // bei bool, darf der Flag nicht gesetzt sein wenn value false
                                    attribute.AtrributeValue = String.Empty;
                                else if (property.PropertyType == typeof(Color) && value is Color)            // bei Color farbe zu HTML machen
                                    attribute.AtrributeValue = ColorTranslator.ToHtml((Color)value);
                                else
                                    attribute.AtrributeValue = value.ToString();
                            }else
                            {
                                if (dynamicProperty.Name != "TagValue" && dynamicProperty.Name != "TagLanguage" && dynamicProperty.Name != "Tag")
                                    // Falls Attribute nicht existiert, wird es jetzt angelegt
                                    result.SetAttributeValue(dynamicProperty.Name, value.ToString(),true);
                            }
                        }
                        return result;
                    }
                    catch 
                    {
                        return null;
                    }
                }
            }
            return null;  
        }

        /// <summary>
        /// Get All Tags
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAvailableTags(TagLanguage language)
        {
            return GetAvailableTags(language.AsString());
        }

        /// <summary>
        /// Get All Tags
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAvailableTags(string language)
        {
            var dir = Pathes.ApplicationPath + Pathes.TAGLIBARY + Path.DirectorySeparatorChar + language;
            if (Directory.Exists(dir))
            {
                var tags = Directory.GetFiles(dir, "*.vtm").OrderBy(s => s);
                foreach (var s in tags)
                {
                    string tag = Path.GetFileNameWithoutExtension(s);
                    tag = tag.Replace("##", ":");
                    yield return tag;
                }
            }
        }

        /// <summary>
        /// Typecast to CustomClass
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static explicit operator CustomClass(Tag tag)
        {
            return tag.ToCustomClass();
        } 

        /// <summary>
        /// Typecast to Tag
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static explicit operator Tag(CustomClass c)
        {
            return CreateFromCustomClass(c);
        }
       

    }
}