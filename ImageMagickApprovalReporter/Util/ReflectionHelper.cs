using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ImageMagickApprovalReporter.Util
{
    public static class ReflectionHelper
    {
        #region Find by base class

        /// <summary>
        /// Creates objects from classes that inherit from <typeparamref name="BaseClass"/> that have default public instance constructor in the current executing assembly
        /// </summary>
        /// <typeparam name="BaseClass">The type of the base class</typeparam>
        /// <returns>An IEnumerable of object that are based on that base class</returns>
        public static IEnumerable<BaseClass> CreateAllClassesInAssemblyInhertidedBy<BaseClass>() where BaseClass : class
        {
            return CreateAllClassesInAssemblyInhertidedBy<BaseClass>(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Creates objects from classes that inherit from <typeparamref name="BaseClass"/> that have default public instance constructor
        /// </summary>
        /// <typeparam name="BaseClass">The type of the base class</typeparam>
        /// <param name="assembly">The assambly to search in</param>
        /// <returns>An IEnumerable of object that are based on that base class</returns>
        public static IEnumerable<BaseClass> CreateAllClassesInAssemblyInhertidedBy<BaseClass>(Assembly assembly) where BaseClass : class
        {
            var typesBasodOn = FindAllClassesInAssemblyInhertidedBy<BaseClass>(assembly);
            return typesBasodOn.Select(t => (BaseClass)Activator.CreateInstance(t));
        }

        /// <summary>
        /// Returns the Type of the classes that inherit from <typeparamref name="BaseClass"/> that have default public instance constructor  in the current executing assembly
        /// </summary>
        /// <typeparam name="BaseClass">The type of the base class</typeparam>
        /// <returns>An IEnumerable of Types that are based on that base class</returns>
        public static IEnumerable<Type> FindAllClassesInAssemblyInhertidedBy<BaseClass>() where BaseClass : class
        {
            return FindAllClassesInAssemblyInhertidedBy<BaseClass>(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Returns the Type of the classes that inherit from <typeparamref name="BaseClass"/> that have default public instance constructor
        /// </summary>
        /// <typeparam name="BaseClass">The type of the base class</typeparam>
        /// <param name="assembly">The assambly to search in</param>
        /// <returns>An IEnumerable of Types that are based on that base class</returns>
        public static IEnumerable<Type> FindAllClassesInAssemblyInhertidedBy<BaseClass>(Assembly assembly) where BaseClass : class
        {
            Type[] types = assembly.GetTypes();
            return types.Where(t => t.IsSubclassOf(typeof(BaseClass)) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null);
        }

        #endregion Find by base class

        #region Find by implmanting interface

        /// <summary>
        /// Creates objects from classes that implement interface <typeparamref name="InterfaceT"/> that have default public instance constructor in the current executing assembly
        /// </summary>
        /// <typeparam name="InterfaceT">The interface</typeparam>
        /// <returns>An IEnumerable of object that are based on that base class</returns>
        public static IEnumerable<InterfaceT> CreateAllClassesInAssemblyImplementing<InterfaceT>() where InterfaceT : class
        {
            return CreateAllClassesInAssemblyImplementing<InterfaceT>(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Creates objects from classes that implement interface <typeparamref name="InterfaceT"/> that have default public instance constructor
        /// </summary>
        /// <typeparam name="InterfaceT">The type interface</typeparam>
        /// <param name="assembly">The assambly to search in</param>
        /// <returns>An IEnumerable of object that are based on that base class</returns>
        public static IEnumerable<InterfaceT> CreateAllClassesInAssemblyImplementing<InterfaceT>(Assembly assembly) where InterfaceT : class
        {
            var typesBasodOn = FindAllClassesInAssemblyImplementing<InterfaceT>(assembly);
            return typesBasodOn.Select(t => (InterfaceT)Activator.CreateInstance(t));
        }

        /// <summary>
        /// Returns the Type of the classes that implement interface <typeparamref name="InterfaceT"/> that have default public instance constructor  in the current executing assembly
        /// </summary>
        /// <typeparam name="InterfaceT">The type interface</typeparam>
        /// <returns>An IEnumerable of Types that are based on that base class</returns>
        public static IEnumerable<Type> FindAllClassesInAssemblyImplementing<InterfaceT>() where InterfaceT : class
        {
            return FindAllClassesInAssemblyImplementing<InterfaceT>(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Returns the Type of the classes that implement interface <typeparamref name="InterfaceT"/> that have default public instance constructor
        /// </summary>
        /// <typeparam name="InterfaceT">The type interface</typeparam>
        /// <param name="assembly">The assambly to search in</param>
        /// <returns>An IEnumerable of Types that are based on that base class</returns>
        public static IEnumerable<Type> FindAllClassesInAssemblyImplementing<InterfaceT>(Assembly assembly) where InterfaceT : class
        {
            string interfaceName = typeof(InterfaceT).Name;
            Type[] types = assembly.GetTypes();
            return types.Where(t => t.GetInterface(interfaceName) != null && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null);
        }

        #endregion Find by implmanting interface
    }
}