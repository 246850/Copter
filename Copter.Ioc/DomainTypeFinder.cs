using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Copter.Ioc
{
    public class DomainTypeFinder : ITypeFinder
    {
        protected string AssemblySkipLoadingPattern
        {
            get
            {
                return "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease|^ServiceStack";
            }
        }

        protected bool LoadAppDomainAssemblies
        {
            get { return true; }
        }

        protected IList<string> AssemblyNames
        {
            get
            {
                return new List<string>();
            }
        }

        private bool ignoreReflectionErrors = true;

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            return this.FindClassesOfType(typeof(T), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return this.FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            return this.FindClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            IList<Type> results = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = null;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (Exception ex)
                {
                    if (!ignoreReflectionErrors)
                    {
                        throw ex;
                    }
                }
                if (types != null && types.Length > 0)
                {
                    foreach (Type type in types)
                    {
                        if (assignTypeFrom.IsAssignableFrom(type) || (assignTypeFrom.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(type, assignTypeFrom)))
                        {
                            if (!type.IsInterface)
                            {
                                if (onlyConcreteClasses)
                                {
                                    if (type.IsClass && !type.IsAbstract)
                                    {
                                        results.Add(type);
                                    }
                                }
                                else
                                {
                                    results.Add(type);
                                }
                            }
                        }
                    }
                }
            }

            return results;
        }

        public virtual IList<Assembly> GetAssemblies()
        {
            IList<string> addedAssemblyNames = new List<string>();
            IList<Assembly> assemblies = new List<Assembly>();

            if (LoadAppDomainAssemblies)
                AddAssembliesInAppDomain(addedAssemblyNames, assemblies);
            AddConfiguredAssemblies(addedAssemblyNames, assemblies);

            return assemblies;
        }

        private bool DoesTypeImplementOpenGeneric(Type type, Type assignTypeFrom)
        {
            try
            {
                Type genericTypeDefinition = assignTypeFrom.GetGenericTypeDefinition();
                bool flag = false;
                foreach (Type implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.IsGenericType) continue;
                    flag = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                    if (flag) break;
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        private void AddAssembliesInAppDomain(IList<string> addedAssemblyNames, IList<Assembly> assemblies)
        {
            Assembly[] domainAssemblys = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in domainAssemblys)
            {
                if (Matches(assembly.FullName))
                {
                    if (!addedAssemblyNames.Contains(assembly.FullName))
                    {
                        assemblies.Add(assembly);
                        addedAssemblyNames.Add(assembly.FullName);
                    }
                }
            }
        }

        protected virtual void AddConfiguredAssemblies(IList<string> addedAssemblyNames, IList<Assembly> assemblies)
        {
            foreach (string assemblyName in AssemblyNames)
            {
                Assembly assembly = Assembly.Load(assemblyName);
                if (!addedAssemblyNames.Contains(assembly.FullName))
                {
                    assemblies.Add(assembly);
                    addedAssemblyNames.Add(assembly.FullName);
                }
            }
        }

        protected virtual bool Matches(string assemblyFullName)
        {
            return !Matches(assemblyFullName, AssemblySkipLoadingPattern);
        }

        protected virtual bool Matches(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
        
        protected virtual void LoadMatchingAssemblies(string binPath)
        {
            if (!Directory.Exists(binPath)) return;

            IList<string> loadedAssemblyNames = new List<string>();
            foreach (Assembly assembly in this.GetAssemblies())
            {
                loadedAssemblyNames.Add(assembly.FullName);
            }

            foreach (string dllPath in Directory.GetFiles(binPath, "*.dll"))
            {
                try
                {
                    AssemblyName name = AssemblyName.GetAssemblyName(dllPath);
                    if (Matches(name.FullName) && !loadedAssemblyNames.Contains(name.FullName))
                    {
                        AppDomain.CurrentDomain.Load(name);
                    }

                    //old loading stuff
                    //Assembly a = Assembly.ReflectionOnlyLoadFrom(dllPath);
                    //if (Matches(a.FullName) && !loadedAssemblyNames.Contains(a.FullName))
                    //{
                    //    App.Load(a.FullName);
                    //}
                }
                catch (BadImageFormatException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
