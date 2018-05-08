using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Copter.Data.EF
{
    public class CopterDbContext : DbContext
    {
        static CopterDbContext()
        {
            Database.SetInitializer<CopterDbContext>(null);
        }

        public CopterDbContext() : this("Name=DefaultDbConnectionString") { }

        public CopterDbContext(string nameOrConnectionString) : this(nameOrConnectionString, string.Empty) { }

        public CopterDbContext(string nameOrConnectionString, string entityAssemblyFullName)
            : base(nameOrConnectionString)
        {
            EntityAssemblyFullName = entityAssemblyFullName;
        }
        public string EntityAssemblyFullName { get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (string.IsNullOrWhiteSpace(EntityAssemblyFullName)) throw new ArgumentNullException("EntityAssemblyFullName");
            Assembly[] ass = AppDomain.CurrentDomain.GetAssemblies();
            Assembly assembly = null;
            foreach (Assembly item in ass)
            {
                if (Regex.IsMatch(item.FullName, string.Format("^{0}?,", EntityAssemblyFullName)))
                {
                    assembly = item;
                    break;
                }
            }

            if (assembly == null) throw new DllNotFoundException(string.Format("未找到领域实体映射程序集：{0}", EntityAssemblyFullName));
            //IEnumerable<Type> mapTypes = assembly.GetTypes().Where(type => !string.IsNullOrWhiteSpace(type.Namespace) && type.BaseType != null && type.BaseType.IsGenericType && (type.BaseType.GetGenericTypeDefinition() == typeof(CopterMapConfiguration<,>) || (type.BaseType.BaseType!=null && type.BaseType.BaseType.GetGenericTypeDefinition() == typeof(CopterMapConfiguration<,>))));
            IEnumerable<Type> mapTypes = assembly.GetTypes().Where(type => typeof(IEntityTypeMap).IsAssignableFrom(type));
            foreach (Type type in mapTypes)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                
                modelBuilder.Configurations.Add(configurationInstance);
            }

            /*********使用Ioc********/
            //ITypeFinder typeFinder = EngineContext.Current.Resolve<ITypeFinder>();
            //IList<Assembly> assemblies = typeFinder.GetAssemblies();

            //foreach (Assembly assembly in assemblies)
            //{
            //    IEnumerable<Type> mapTypes = assembly.GetTypes().Where(type => !string.IsNullOrWhiteSpace(type.Namespace) && type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(CopterMapConfiguration<>));
            //    foreach (Type type in mapTypes)
            //    {
            //        dynamic configurationInstance = Activator.CreateInstance(type);
            //        modelBuilder.Configurations.Add(configurationInstance);
            //    }
            //}
            ////IEnumerable<Type> mapTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => !string.IsNullOrWhiteSpace(type.Namespace)).Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(CopterMapConfiguration<>));

            base.OnModelCreating(modelBuilder);
        }
    }
}
