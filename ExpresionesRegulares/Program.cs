using System.Globalization;
using System.Text.RegularExpressions;
namespace ExpresionesRegulares
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileOne = GetFileText("segmentation_file_2nd_pass_1");
            string fileTwo = GetFileText("segmentation_file_2nd_pass_2");
            string fileThree = GetFileText("segmentation_file_2nd_pass_3");

            // Encontrar secciones ejecutar este metodo
            GetMatches("segmentation_file_2nd_pass_1");

            Console.WriteLine();

            //Fechas que esten entre el primero de enero y el 30 de junio, ejecutar este metodo.
            encontrarFechas("segmentation_file_2nd_pass_1");

            Console.WriteLine();

            //Extraer todos los precios y sumarlos, ejecutar este metodo.
            SumarValores("segmentation_file_2nd_pass_3");

            Console.WriteLine();

            //Extraer todos los porcentajes y sumarlos, ejecutar este metodo.
            PorcentajesSumar("segmentation_file_2nd_pass_3");

            //El total del palabras en mayúsculas, ejecutar este metodo.
            LetrasMayusculas("segmentation_file_2nd_pass_2");

        }

        public static void GetMatches(string fileName)
        {
            string fileText = GetFileText(fileName);
            Regex regex = new Regex(@"(?<=\n|\r|^)(\d{1,2}\.\s.*?)(?=(?:\n\d{1,2}\.\s)|\z)", RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(fileText);
            if (matches.Count > 0)
            {
                Console.WriteLine("¡Se encontraron secciones!\n");
                int i = 1;
                foreach (Match match in matches)
                {
                    Console.WriteLine($"🔸 Sección {i}:\n{match.Value.Trim()}\n");
                    Console.WriteLine(new string('-', 50));
                    i++;
                }
            }
            else
            {
                Console.WriteLine("No se encontraron secciones.");
            }
        }

        public static void encontrarFechas(string fileName)
        {
            string fileOne = GetFileText(fileName);

            
            Regex fechaRegex = new Regex(@"\b(January|February|March|April|May|June)\s+\d{1,2},\s+\d{4}\b");

            MatchCollection matches = fechaRegex.Matches(fileOne);

            Console.WriteLine("Fechas encontradas entre el 1 de enero y el 30 de junio:");

            foreach (Match match in matches)
            {
                string fechaTexto = match.Value;

                
                if (DateTime.TryParseExact(fechaTexto, "MMMM d, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
                {
                    
                    if (fecha.Month >= 1 && fecha.Month <= 6)
                    {
                        Console.WriteLine($"📅 {fechaTexto} (parsed: {fecha.ToShortDateString()})");
                    }
                }
            }
        }


        public static void SumarValores(string fileName)
        {
            string fileText = GetFileText(fileName);

            // Regex para encontrar precios
            Regex precioRegex = new Regex(@"\$\d{1,3}(,\d{3})*(\.\d{1,2})?|\$\.\d{1,2}");

            MatchCollection matches = precioRegex.Matches(fileText);

            decimal suma = 0;

            Console.WriteLine("💰 Precios encontrados:");

            foreach (Match match in matches)
            {
                string precioStr = match.Value.Replace("$", "").Replace(",", "");

                if (decimal.TryParse(precioStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
                {
                    Console.WriteLine($" - ${valor}");
                    suma += valor;
                }
            }

            Console.WriteLine($"\n🧾 Suma total: ${suma}");
        }
        
        public static void PorcentajesSumar(string fileName)
        {
            string fileText = GetFileText(fileName);

            Regex porcentajeRegex = new Regex(@"\b\d+(\.\d+)?\s*%");

            MatchCollection matches = porcentajeRegex.Matches(fileText);

            double suma = 0;

            Console.WriteLine("📊 Porcentajes encontrados:");

            foreach (Match match in matches)
            {
                // Elimina el símbolo % y espacios
                string porcentajeStr = match.Value.Replace("%", "").Trim();

                if (double.TryParse(porcentajeStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor))
                {
                    Console.WriteLine($" - {valor}%");
                    suma += valor;
                }
            }

            Console.WriteLine($"\n📈 Suma total de porcentajes: {suma}%");
        }

        public static void LetrasMayusculas(string fileName)
        {
            string fileText = GetFileText("segmentation_file_2nd_pass_2");

            Regex mayusculasRegex = new Regex(@"\b[A-Z]{2,}\b");

            MatchCollection matches = mayusculasRegex.Matches(fileText);

            Console.WriteLine("🔠 Palabras en mayúsculas encontradas:");

            foreach (Match match in matches)
            {
                Console.WriteLine($" - {match.Value}");
            }

            Console.WriteLine($"\n🧾 Total de palabras en mayúsculas: {matches.Count}");
        }

        private static string GetFileText(string fileName)
        {
            var reader = new  StreamReader(@""+fileName+".txt");
            var json = reader.ReadToEnd();
            reader.Dispose();
            return json;
        }
    }
}
