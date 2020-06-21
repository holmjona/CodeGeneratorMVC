using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public static class ObjectExtensions {
    //public static void Flag(this DataGridViewCell x) {
    //    x.Tag = true;
    //}
    //public static void UnFlag(DataGridViewCell x) {
    //    x.Tag = false;
    //}
    //public static bool isFlagged(this DataGridViewCell x) {
    //    return System.Convert.ToBoolean(x.Tag);
    //}
    public static int Count(this string st, char ch) {
        int cnt = 0;
        foreach (char c in st) {
            if (c == ch)
                cnt += 1;
        }
        return cnt;
    }
}
