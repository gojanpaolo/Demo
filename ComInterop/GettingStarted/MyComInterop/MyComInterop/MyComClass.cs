using System;
using System.Runtime.InteropServices;

namespace MyComInterop
{
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("11C580CC-A213-4AA0-AF0C-EFA7A59F38EF")]
    [ComVisible(true)]
    public interface IMyComClass
    {
        string MyString { get; }
    }

    [ClassInterface(ClassInterfaceType.None)]
    [Guid("F3F2A69E-422C-4EA4-BDB3-11C57AB82DC3")]
    [ComVisible(true)]
    public class MyComClass : IMyComClass
    {
        public string MyString => "My string from my class.";
    }
}
