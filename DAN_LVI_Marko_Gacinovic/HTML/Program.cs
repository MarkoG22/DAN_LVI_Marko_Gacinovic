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
            // inputs and data
            string address = null;
            string data = null;
            string zip = null;

            // loop for repeating the action
            do
            {
                try
                {
                    Console.WriteLine("\nWelcome");
                    Console.WriteLine("Please copy the url link you want in the file (Enter 'x' for exit): ");

                    // input for url
                    address = Console.ReadLine();

                    // exit
                    if (address == "x")
                    {
                        Environment.Exit(0);
                    }

                    // downloading the html to the file
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

                    // exit
                    if (zip == "X")
                    {
                        Environment.Exit(0);
                    }
                                        
                    if (zip == "YES")
                    {
                        // deleting zipped file if already exists
                        File.Delete(@"..\..\ZIPPED.zip");

                        // zipping the file
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
