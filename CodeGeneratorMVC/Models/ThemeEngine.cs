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
using System.Drawing;

public class ThemeEngine {
    public static void SetRowRegularColors(ref Color back, ref Color fore) {
        back = Color.White;
        fore = Color.Black;
    }
    public static void SetRowGoodColors(ref Color back, ref Color fore) {
        back = Color.FromArgb(255, 230, 245, 230);
        fore = Color.Black;
    }
    public static void SetRowErrorColors(ref Color back, ref Color fore) {
        back = Color.FromArgb(255, 255, 230, 230);
        fore = Color.Black;
    }
    public static void SetRowWarningColors(ref Color back, ref Color fore) {
        back = Color.FromArgb(255, 255, 245, 230);
        fore = Color.Black;
    }
}
