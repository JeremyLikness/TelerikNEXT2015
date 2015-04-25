using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AA.Crud.Domain;
using AB.Crud.DataAccess;

namespace AD.ImportData
{
    internal static class Extensions
    {
        internal static string AsCleanField(this string source)
        {
            return source.Replace("~", string.Empty);
        }

        internal static decimal? SafeDecimal(this string source)
        {
            decimal target;
            if (decimal.TryParse(source, out target))
            {
                return target;
            }
            return null;
        }

        internal static IEnumerable<string> AsEnumerableLinesForFile(this string source)
        {
            var pathToSource = typeof(Program)
                .FullName
                .Replace(typeof(Program).Name, source);

            var result = new List<string>();

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pathToSource))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(string.Format("Couldn't open stream for resource {0}", pathToSource));
                }

                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var nextLine = reader.ReadLine();
                        result.Add(nextLine);
                    }
                }
            }

            return result;
        }
    }

    class Program
    {

        private static void Main()
        {
            var parse = "FOOD_DES.txt".AsEnumerableLinesForFile().ToList();
            Console.WriteLine("Parsing {0} food descriptions.", parse.Count());

            using (var context = new FoodContext())
            {
                if (context.FoodDescriptions.Any())
                {
                    Console.WriteLine("Food descriptions already exist.");
                }
                else
                {
                    foreach (var item in parse.Select(description => description.Split('^'))
                        .Select(parts => new FoodDescription
                        {
                            Number = parts[0].AsCleanField(),
                            Group = parts[1].AsCleanField(),
                            Description = parts[2].AsCleanField(),
                            Name = parts[3].AsCleanField(),
                            ProteinFactor = parts[11].SafeDecimal(),
                            FatFactor = parts[12].SafeDecimal(),
                            CarbFactor = parts[13].SafeDecimal(),
                            SomeDate = DateTimeOffset.Now
                        }))
                    {
                        context.FoodDescriptions.Add(item);
                        Console.Write(".");
                    }

                    Console.WriteLine("Saving changes...");
                    context.SaveChanges();
                    Console.WriteLine("Done!");
                }
            }

            Console.ReadLine();
        }
        
    }
}
