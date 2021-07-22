using System;
using System.Reflection;
using System.Windows.Forms;

namespace CompleX
{
    public class ClassViewer {
        public static void AddClassInfo(TreeView tv, Type type, object[] objects) {
            string s = type.Assembly.FullName;
            s = s.Substring(0, s.IndexOf(", "));
            TreeNode aNode = tv.Nodes[tv.Nodes.Add(new TreeNode(s, 0, 0))];
            foreach(object obj in objects) {
                s = obj.ToString();
                AddMetods(obj.GetType(), aNode.Nodes[aNode.Nodes.Add(new TreeNode(s, 1, 1))]);
            }
            tv.ExpandAll();
        }

        private static void AddMetods(Type type, TreeNode node) {
            MemberInfo[] mii = type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            foreach(MemberInfo mi in mii) {
                int ind = GetImageIndex(mi.MemberType);
                if(ind != -1) ind += GetAccessType(mi);
                node.Nodes.Add(new TreeNode(mi.ToString(), ind, ind));
            }
        }

        private static int GetImageIndex(MemberTypes type) {
            switch(type.ToString()) {
                case "Field": 
                case "Property": 
                    return 2;
                case "Event": 
                    return 3;
                case "Method": 
                case "Constructor":
                    return 4;
                default: 
                    return -1;
            }
        } 

        private static int GetAccessType(MemberInfo m) {
            int x = 3;
            MethodInfo mi = GetMethodInfo(m);
            int ret = x * 2;
            if(m is EventInfo) 
                mi = (m as EventInfo).GetAddMethod(true);
            if(mi != null) {
                if(mi.IsPublic) ret = 0;
                if(mi.IsPrivate) ret = x;
            }
            if(m is FieldInfo) { 
                if((m as FieldInfo).IsPublic) ret = 0;
                if((m as FieldInfo).IsPrivate) ret = x;
            }
            if(m is ConstructorInfo) {
                if((m as ConstructorInfo).IsPublic) ret = 0;
                if((m as ConstructorInfo).IsPrivate) ret = x;
            }
            return ret;
        }

        private static MethodInfo GetMethodInfo(MemberInfo m) {
            if(m is PropertyInfo) {
                PropertyInfo pi = m as PropertyInfo;
                m = pi.GetGetMethod();
                if(m == null) m = pi.GetGetMethod(true);
            }
            if(m is MethodInfo) 
                return m as MethodInfo;
            return null;
        }
    }
}