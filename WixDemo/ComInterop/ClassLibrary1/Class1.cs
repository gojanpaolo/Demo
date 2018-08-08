using System;
using System.Runtime.InteropServices;

namespace ClassLibrary1
{
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("BA51C14A-52E5-46D5-B4BF-97A65CC52CB5")]
    [ComVisible(true)]
    public interface IClass1
    {
        [DispId(1)]
        string MyString { get; }
    }

    [ClassInterface(ClassInterfaceType.None)]
    [Guid("CF44CAFD-E9FF-41EA-A0A1-65F1EDA4E2B5")]
    [ComVisible(true)]
    public class Class1 : IClass1
    {
        public string MyString => "My string from my class.";
    }
}
