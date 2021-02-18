using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

/**
 * Main class that launches the server
 */

namespace Bell_Server
{
    class Program
    {
        static void Main(string[] args)
        {

            //Start the server 
            PersonContainer pCon = new PersonContainer();
            Networking networkConnection = new Networking("localhost", 8080);
            Console.WriteLine("Server is starting up...");
            try
            {
                networkConnection.Server.Start();
                Console.WriteLine("Welcome to Bell's Server!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Read();
            }

            bool serverRunning = true;

            while (serverRunning)
            {
                networkConnection.acceptClients();
                networkConnection.getStream();

                //receive user choice
                byte[] recievedResponse = new byte[1];
                networkConnection.read(recievedResponse);
                string userChoiceMsg = Encoding.ASCII.GetString(recievedResponse, 0, recievedResponse.Length);

                //Do operation chosen by client
                switch (userChoiceMsg)
                {
                    //Insert new person into database
                    case "1":
                        //recieve person data
                        byte[] receivedBuffer = new byte[100];
                        networkConnection.read(receivedBuffer);
                        string personReceived = Encoding.ASCII.GetString(receivedBuffer, 0, receivedBuffer.Length);

                        //indicates whether operation was a success or a failure
                        byte[] operationStatus;
                        
                        try
                        {
                            pCon.addPerson(personReceived);
                            operationStatus = Encoding.ASCII.GetBytes("Successfully added person into server.");
                            networkConnection.write(operationStatus);
                            Console.WriteLine("Successfully added person into database/file.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("failed to add person into database...");
                            operationStatus = Encoding.ASCII.GetBytes("Failed to add person into database, check input and try again");
                            networkConnection.write(operationStatus);
                            Console.WriteLine(e.ToString());

                        }
                        break;
                    
                    //return all person entries from database to client
                    case "2":
                        List<string> personList = pCon.getPersonsString();
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(networkConnection.Stream, personList);
                        break;

                    //close the connection with the client and terminate the server
                    case "3":
                        Console.WriteLine("Good Bye!");
                        networkConnection.Server.Stop();
                        networkConnection.Stream.Close();
                        serverRunning = false;
                        break;
                }
            }
        }
    }
}
