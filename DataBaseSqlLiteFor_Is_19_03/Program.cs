using System;

namespace DataBaseSqlLiteFor_Is_19_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вы в программе ура ");
            while(true)
            {
                PrintMenu(); 
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("Если вы хотите получить  список  всех компаний введите \"allCompany\"");
            Console.WriteLine("Если вы хотите добавить новую компанию введите \"addCompany\"");
            Console.WriteLine("Если вы хотите удалить  компанию введите \"removeCompany\"");

            switch(Console.ReadLine().ToUpper())
            {
                case "ALLCOMPANY": PrintAllCompany(); break;
                case "ADDCOMPANY": AddCompany(); break;
                case "REMOVECOMPANY": RemoveCompany(); break;
                default: Console.WriteLine("Команда не  опознана, введите команду  еще  раз"); break;
            }

            Console.WriteLine();
        }

        private static void RemoveCompany()
        {
            using (DB.MySqlLiteContext context = new DB.MySqlLiteContext())
            {
                try
                {
                    Console.WriteLine("Введите ид компании которую хотите удалить");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var company = context.Companies.Find(id);
                    if (company != null)
                    {
                        context.Companies.Remove(company);
                        context.SaveChanges();
                        Console.WriteLine("Компания удалена из БД");
                        PrintAllCompany();
                    }
                    else
                    {
                        Console.WriteLine("Компания не найдена");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void AddCompany()
        {
            using (DB.MySqlLiteContext context = new DB.MySqlLiteContext())
            {
                try
                {
                    Console.WriteLine("Введите название компании");
                    var name = Console.ReadLine().TrimStart().TrimEnd();

                    if(String.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Имя не  корректно");
                        return;
                    }

                    context.Companies.Add(new DB.Model.Company() { Name = name });
                    context.SaveChanges();
                    Console.WriteLine("Компания добавлена  в  БД");

                    PrintAllCompany();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// получает  список всех компаний из БД
        /// </summary>
        private static void PrintAllCompany()
        {
            using (DB.MySqlLiteContext  context = new DB.MySqlLiteContext() )
            {
                try
                {
                    foreach (var item in context.Companies)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
