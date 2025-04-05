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

            Regex regex = new Regex(@"ADDITIONAL SERVICES");
            
            if(regex.IsMatch(fileOne))
            {
                Console.WriteLine("Match!");
            }
            else
            {
                Console.WriteLine("No Match!");
            }

            Match match = regex.Match(fileOne);
            if(match.Success)
            {
                Console.WriteLine("Match Value: " + match.Value);
            }
           

            if (regex.IsMatch(fileTwo))
            {
                Console.WriteLine("Match!");
            }
            else
            {
                Console.WriteLine("No Match!");
            }
            if (regex.IsMatch(fileThree))
            {
                Console.WriteLine("Match!");
            }
            else
            {
                Console.WriteLine("No Match!");
            }
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
