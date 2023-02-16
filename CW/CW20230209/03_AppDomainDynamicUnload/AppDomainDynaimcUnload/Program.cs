using System;
using System.Linq;
using System.Reflection;

namespace AppDomainSample
{
    class Program
    {
        // Атрибут, що вказує механізм завантаження збірки
        [LoaderOptimization(LoaderOptimization.SingleDomain)] 
        static void Main(string[] args)
        {
            string name_dll = "SampleLibrary"; 
            string name_method = "DoSome";

            //создаём домен приложения с приозвольным именем
            AppDomain Domain = AppDomain.CreateDomain("AppDomain");

            try
            {
                //загружаем в созданный нами домен приложения заранее подготовленную dll библиотеку
                Assembly asm = Domain.Load(AssemblyName.GetAssemblyName(name_dll + ".dll"));

                //Var1 получаем модуль, из которого будем осуществлять вызов
                Module module = asm.GetModule(name_dll + ".dll");
                //получаем тип данных, содержащий искомый метод
                Type[] type = module.GetTypes();
                foreach (var it in type)
                {
                    //получаем метод из типа данных
                    MethodInfo method = it.GetMethod(name_method);
                    //осуществляем вызов метода
                    method.Invoke(null, null);
                }
                //Var1 получаем модуль, из которого будем осуществлять вызов


                //Var2 однострочный вариант вызова того же метода через анонимные объекты
                asm.GetModule(name_dll + ".dll")
                   //.GetTypes()
                   //     .Where(t => t.Name.Contains("SampleClass"))
                   //     .FirstOrDefault()
                 .GetType(name_dll + ".SampleClass")
                   .GetMethod(name_method)
                   .Invoke(null, null);
                //Var2 однострочный вариант вызова того же метода через анонимные объекты

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

            // Вигружаем домен приложения
            AppDomain.Unload(Domain);
        }
    }
}
