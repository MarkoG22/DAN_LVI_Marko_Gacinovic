using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HTML
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = null;
            string data = null;
            string zip = null;
            do
            {
                try
                {
                    Console.WriteLine("\nWelcome");
                    Console.WriteLine("Please copy the url link you want in the file (Enter 'x' for exit): ");
                    address = Console.ReadLine();
                    if (address == "x")
                    {
                        Environment.Exit(0);
                    }
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;
                        if (String.IsNullOrWhiteSpace(response.CharacterSet))
                            readStream = new StreamReader(receiveStream);
                        else
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        data = readStream.ReadToEnd();
                        //Console.Write("Enter the file name: ");
                        //string fileName = Console.ReadLine();
                        using (StreamWriter file = new StreamWriter(@"..\..\HTML\HTML.txt"))
                        {
                            file.WriteLine(data);
                        }
                        response.Close();
                        readStream.Close();
                    }
                }
                catch (UriFormatException)
                {
                    Console.WriteLine("Wrong web address, please try again.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }

                try
                {
                    Console.WriteLine("Do you want to zip the file (yes/no)?");
                    zip = Console.ReadLine().ToUpper();
                    if (zip == "X")
                    {
                        Environment.Exit(0);
                    }

                    if (zip == "YES")
                    {
                        File.Delete(@"..\..\ZIPPED.zip");
                        ZipFile.CreateFromDirectory(@"..\..\HTML\", @"..\..\ZIPPED.zip");
                        Console.WriteLine("\nThe file zipped successfully.");
                    }

                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("The file do not exists, please try again.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            } while (address != "x" || zip != "X");          
            
            Console.ReadLine();
        }
    }
}
