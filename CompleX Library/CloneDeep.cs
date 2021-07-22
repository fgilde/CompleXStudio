using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;

namespace CompleX_Library
{
    /// <summary>
    /// Implement this interface to clone internal stuff which is not covered by CloneDeepFactory.CloneDeep()
    /// You should call CloneDeepFactory.DeepCloneImpl to clone the public visible stuff, after that clone the private stuff
    /// </summary>
    public interface IDeepCloneable
    {
        /// <summary>
        /// Creates a Clone of the object
        /// </summary>
        /// <param name="settings">controls the special behaviour when cloning</param>
        /// <returns>the cloned object</returns>
        object DeepClone(CloneDeepSettings settings);
    }
    /// <summary>
    /// Implement this interface to clone internal stuff which is not covered by CloneDeepFactory.CloneDeep()
    /// You should call CloneDeepFactory.DeepCloneImpl to clone the public visible stuff, after that clone the private stuff
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDeepCloneable<T>
    {
        /// <summary>
        /// Creates a Clone of the object
        /// </summary>
        /// <param name="settings">controls the special behaviour when cloning</param>
        /// <returns>the cloned object</returns>
        T DeepClone(CloneDeepSettings settings);
    }

    /// <summary>
    /// Internal Factory:
    /// This Interface is designed to be implemented in the objetc which has to be cloned.
    /// Implement it to create instances of objects that need special constructors
    /// </summary>
    public interface ICreateInstance
    {
        object CreateInstance();
    }

    /// <summary>
    /// External Factory:
    /// Implement this interface to create instances of objects that need special constructors
    /// Add an implementor of this interface to CloneDeepSettings.InstanceFactories
    /// This one is the right choice if cannot implement ICreateInstance in the object to be cloned, so
    /// you can use this one to provide an external factory.
    /// (if both are provided, this one has higher priority, because it is provided excplicitly in the settings)
    /// </summary>
    public interface IInstanceFactory
    {
        /// <summary>
        /// creates a new instance with the same type than srcObj 
        /// </summary>
        /// <param name="srcObj">use this to get the needed parameters for special ctors
        /// This on would not be null</param>
        /// <returns>the new instance with the same type than srcObj</returns>
        object CreateInstance(object srcObj);
    }

    public class CloneDeepSettings
    {
        public CloneDeepSettings()
        {
            CloneFields = true;
            CloneProperties = true;
            CloneDeepObjects = true;
            TryToUseIDeepClonable = true;
            TryToUseIClonable = false;
            TryToUseIClonableIfCtorFails = true;
            CloneDeepInterfaces = false;
            CloneDeepIList = true;
            CloneDeepIDictionary = true;
        }
        public bool CloneFields { get; set; }
        public bool CloneProperties { get; set; }
        public bool CloneDeepObjects { get; set; }
        public bool TryToUseIDeepClonable { get; set; }
        public bool TryToUseIClonable { get; set; }
        public bool TryToUseIClonableIfCtorFails { get; set; }
        public bool CloneDeepInterfaces { get; set; }
        public bool CloneDeepIList { get; set; }
        public bool CloneDeepIDictionary { get; set; }
        public List<MemberInfo> SkipItems = new List<MemberInfo>();
        public Dictionary<Type, IInstanceFactory> InstanceFactories = new Dictionary<Type, IInstanceFactory>();
    }
    /// <summary>
    /// static class to provide cloning functions
    /// </summary>
    public static class CloneDeepFactory
    {
        /// <summary>
        /// Creates a Clone of the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcObj"></param>
        /// <returns>the cloned object</returns>
        public static T CloneDeep<T>(this T srcObj)
        {
            return CloneDeep(srcObj, null);
        }
        /// <summary>
        /// Creates a Clone of the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcObj"></param>
        /// <param name="settings">controls the special behaviour when cloning</param>
        /// <returns>the cloned object</returns>
        public static T CloneDeep<T>(this T srcObj, CloneDeepSettings settings)
        {
            return CloneDeepInternal(srcObj, settings, true);
        }
        /// <summary>
        /// this static method can be used to implement an Interface like ICloneable or IDeepCloneable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Source">the object to copy</param>
        /// <param name="settings">controls the special behaviour when cloning</param>
        /// <returns>the cloned object</returns>
        public static T DeepCloneImpl<T>(T Source, CloneDeepSettings settings)
        {
            if (Source == null)
                return default(T);
            if (settings == null)
                settings = new CloneDeepSettings();
            Type SourceType = Source.GetType();
            object Target = CreateInstance(SourceType, Source, settings);
            CloneItems<T>(SourceType, Source, Target, settings);
            return (T)Target;
        }
        static T CloneDeepInternal<T>(this T srcObj, CloneDeepSettings settings, bool TopLevel)
        {
            if (srcObj == null)
                return default(T);
            if (settings == null)
                settings = new CloneDeepSettings();

            return (T)CloneValue(srcObj.GetType(), srcObj, settings, TopLevel);
        }
        static object CreateInstance(Type srcType, object srcObj, CloneDeepSettings settings)
        {
            //1. try to find an external Factory provided in the settings
            if (settings.InstanceFactories.ContainsKey(srcType))
                return settings.InstanceFactories[srcType].CreateInstance(srcObj);

            //2. perhaps the source object has implemented ICreateInstance
            if (typeof(ICreateInstance).IsAssignableFrom(srcType))
                return ((ICreateInstance)(srcObj)).CreateInstance(); //use ICreateInstance to create an instance

            //3. try to use the Activator and a contructor without params
            ConstructorInfo ci = srcType.GetConstructor(Type.EmptyTypes);
            if (ci != null)
                return ci.Invoke(null); //target = Activator.CreateInstance(srcType);

            //4. special handling for Arrays (try to create them empty without cloning them - and copy the items later)
            if (srcType.IsArray)
            {
                object arr = srcObj;
                return Activator.CreateInstance(srcType, ((Array)arr).Length);
            }
            //5. at least try to use ICloneable to create an Instance (we can override the items later)
            if (settings.TryToUseIClonableIfCtorFails && (typeof(ICloneable).IsAssignableFrom(srcType)))
                return ((ICloneable)srcObj).Clone();

            //End. bad luck
            throw new Exception(string.Format("CloneDeep: Not able to create an instance of {0}.\nProvide a parameterless constructor, an IInstanceFactory in settings or implement ICreateInstance", srcType));
        }

        static void CloneItems<T>(Type srcType, object srcObj, object target, CloneDeepSettings settings)
        {
            //copy the fields
            if (settings.CloneFields)
            {
                foreach (FieldInfo fi in srcType.GetFields()) //better watch only the public fields, not: BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                {
                    if (!settings.SkipItems.Contains(fi))
                        fi.SetValue(target, CloneValue(fi.FieldType, fi.GetValue(srcObj), settings, false));
                }
            }

            //copy the properties
            if (settings.CloneProperties)
            {
                foreach (PropertyInfo pi in srcType.GetProperties()) //better watch only the public fields, not: BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                {
                    if (!settings.SkipItems.Contains(pi))
                    {
                        if (pi.CanRead && pi.CanWrite && (pi.GetIndexParameters().Length == 0)) //property must be readable and writeable, indexed Properties are not supported
                            pi.SetValue(target, CloneValue(pi.PropertyType, pi.GetValue(srcObj, null), settings, false), null);
                    }
                }
            }
            if (typeof(IList).IsAssignableFrom(srcType) && settings.CloneDeepIList)
            {
                IList srcList = (IList)srcObj;
                IList targetList = (IList)target;
                if (srcList.Count == targetList.Count)
                {   //same count (this should work for arrays too)
                    for (int i = 0; i < srcList.Count; i++)
                    {
                        targetList[i] = CloneDeepInternal(srcList[i], settings, false);
                    }
                }
                else
                {   //different count
                    if (targetList.IsFixedSize)
                        throw new Exception(string.Format("CloneDeep: Not able to clone IList {0}: fixed Size and different count.\nPerhaps skip this Item in settings and clone it manually", targetList));
                    //clear target and add all items
                    targetList.Clear();
                    foreach (object item in srcList)
                    {
                        targetList.Add(CloneDeepInternal(item, settings, false));
                    }
                }
            }
            if (typeof(IDictionary).IsAssignableFrom(srcType) && settings.CloneDeepIDictionary)
            {
                IDictionary srcDic = (IDictionary)srcObj;
                IDictionary targetDic = (IDictionary)target;
                targetDic.Clear();
                foreach (object key in srcDic.Keys)
                {
                    targetDic.Add(CloneDeepInternal(key, settings, false), CloneDeepInternal(srcDic[key], settings, false));
                }
            }
        }

        private static object CloneValue(Type ValueType, object Value, CloneDeepSettings settings, bool TopLevel)
        {
            if (Value == null)
                return null;
            if (ValueType.IsValueType)
                return Value;
            //if (ValueType.IsArray)...  -> is handled via it's IList
            if (ValueType.IsClass || (ValueType.IsInterface && settings.CloneDeepInterfaces))
            {
                if (ValueType == typeof(string))
                    return Value;

                //if Toplevel then create a new instance (even if CloneDeepObjects=false) -> if not we would return the same reference as given
                if (settings.CloneDeepObjects || (ValueType.IsInterface && settings.CloneDeepInterfaces) || TopLevel)
                {
                    //1. try to use our own Interface
                    Type genType = typeof(IDeepCloneable<>).MakeGenericType(ValueType);
                    if (ValueType.GetInterfaces().Any(intftype => intftype == genType))
                    {
                        InterfaceMapping imap = ValueType.GetInterfaceMap(genType);
                        MethodInfo mi = imap.InterfaceMethods.FirstOrDefault(e => e.Name == "DeepClone");
                        return mi.Invoke(Value, new object[1] { settings });
                    }
                    //this way looks better, but here we don't have the Type T, because this one changes when calling recursive
                    //if (settings.TryToUseIDeepClonable && (typeof(IDeepCloneable<T>).IsAssignableFrom(ValueType)))
                    //    return ((IDeepCloneable<T>)Value).DeepClone(settings);

                    //now check the non generic part
                    if (settings.TryToUseIDeepClonable && (typeof(IDeepCloneable).IsAssignableFrom(ValueType)))
                        return ((IDeepCloneable)Value).DeepClone(settings);
                    //2. try to use ICloneable
                    if (settings.TryToUseIClonable && (typeof(ICloneable).IsAssignableFrom(ValueType)))
                        return ((ICloneable)Value).Clone();
                    //3. now we do "DeepClone" on our own way
                    return DeepCloneImpl(Value, settings);
                }
                else
                    return Value; //don't CloneDeepObjects -> return Reference
            }
            if (ValueType.IsInterface)
                if (settings.CloneDeepInterfaces)
                    throw new Exception("invalid logic"); //this case has been handled above
                else
                    return Value; //don't CloneDeepInterfaces -> return Reference

            return null;
        }
    }
}
