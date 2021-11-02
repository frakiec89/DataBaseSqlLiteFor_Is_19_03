using Microsoft.EntityFrameworkCore;
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
            Console.WriteLine("Если вы хотите показать список всех телефонов введите\"allPhone\"");
            Console.WriteLine("Если вы хотите добавить новый телефон введите\"addPhone\"");

            switch (Console.ReadLine().ToUpper())
            {
                case "ALLCOMPANY": PrintAllCompany(); break;
                case "ADDCOMPANY": AddCompany(); break;
                case "REMOVECOMPANY": RemoveCompany(); break;
                case "ALLPHONE": PrintAllPhone(); break;
                case "ADDPHONE": AddPhone(); break;
                case "REMOVEPHONE": RemovePhone(); break;
                default: Console.WriteLine("Команда не  опознана, введите команду  еще  раз"); break;
            }

            Console.WriteLine();
        }

        private static void RemovePhone()
        {
            using (DB.MySqlLiteContext context = new DB.MySqlLiteContext())
            {
                try
                {
                    Console.WriteLine("Введите ид телефона");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var phone = context.Phones.Find(id);
                    if (phone != null)
                    {
                        context.Phones.Remove(phone);
                        context.SaveChanges();
                        Console.WriteLine("Телефон удален из БД");
                        PrintAllPhone();
                    }
                    else
                    {
                        Console.WriteLine("Телефон не найден");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void AddPhone()
        {
            Console.WriteLine("Укажите название телефона");
            var name = Console.ReadLine();
            
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine($"не корректное имя");
                return;
            }
            try
            {
                Console.WriteLine("Укажите Цену  телефона");
                var price = Convert.ToInt32( Console.ReadLine());
                Console.WriteLine("Укажите id компании");

                Console.WriteLine("___________");
                PrintAllCompany();
                Console.WriteLine("___________");


                int idCompany = Convert.ToInt32(Console.ReadLine());
                var company = IsCompany(idCompany);

                if (company != null)
                {
                    Console.WriteLine($"Вы выбрали компанию {company.ToString()}");
                }
                else
                {
                    Console.WriteLine($"Компания не  найдена");
                    return;
                }

                using(DB.MySqlLiteContext context = new DB.MySqlLiteContext())
                {
                    context.Add(new DB.Model.Phone()
                    {
                        Title = name,  CompanyId = company.Id, Price = price
                    });

                    context.SaveChanges();
                    Console.WriteLine("Сохранение  прошло успешно");
                    PrintAllPhone();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            




        }

        private static DB.Model.Company IsCompany(int idCompany)
        {
            using (DB.MySqlLiteContext context = new DB.MySqlLiteContext())
            {
                try
                {
                   var comapany =   context.Companies.Find(idCompany);
                    if(comapany!= null)
                    {
                        return comapany;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch 
                {
                    return null;
                }
            }
        }

        private static void PrintAllPhone()
        {
            using (DB.MySqlLiteContext context = new DB.MySqlLiteContext())
            {
                try
                {
                    foreach (var item in context.Phones.Include(x=>x.Company))
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
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
