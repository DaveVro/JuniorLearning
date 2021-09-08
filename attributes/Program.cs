using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace attributes
{
    class Program
    {
        /// <summary>
        /// Entry point of the application, returning information of attributes
        /// </summary>
        static void Main(string[] args)
        {
            TypeInfo typeInfo = typeof(MyClass).GetTypeInfo();
            Console.WriteLine("The assembly qualified name of MyClass is " + typeInfo.AssemblyQualifiedName);

            var attrs = typeInfo.GetCustomAttributes();
            foreach (var attr in attrs)
            {
                Console.WriteLine("Attribute on MyClass: " + attr.GetType().Name);
            }
        }
    }

    /// <summary>
    /// Example of using the Obsolete attribute
    /// </summary>
    [Obsolete]
    public class MyClass
    {

    }

    /// <summary>
    /// Example of using Obsolete attribute along with a string parameter providing information of what to do instead
    /// </summary>
    [Obsolete("ThisClass is Obsolete. Use ThisClass2 instead.")]
    public class ThisClass
    {

    }

    /// <summary>
    /// An example of defining a new attribute which inherits from the Attribute base class
    /// </summary>
    public class MySpecialAttribute : Attribute
    {

    }

    /// <summary>
    /// An example of using MySpecialAttribute
    /// </summary>
    [MySpecial]
    public class SomeOtherClass
    {

    }

    /// <summary>
    /// An example of using RaisePropertyChanged
    /// </summary>
    public class MyUIClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
