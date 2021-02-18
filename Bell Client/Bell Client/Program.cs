using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


namespace Bell_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bell's Client!");

            bool clientExecution = true;

            while (clientExecution)
            {
                //Set up networking 

                Networking networkConnection = new Networking("localhost", 8080);

                Console.WriteLine("Select from the following options: (1) submit a new person, (2) Retrieve all data from server, (3) Terminate program");

                string userChoice = Console.ReadLine();
                int userChoiceByteCount = Encoding.ASCII.GetByteCount(userChoice);
                byte[] userChoiceByte = new byte[userChoiceByteCount];

                switch (userChoice)
                {
                    //send a new person data to the server
                    case "1":
                        //send user choice to the server
                        userChoiceByte = Encoding.ASCII.GetBytes(userChoice);
                        networkConnection.write(userChoiceByte);

                        //Get data from user
                        Console.WriteLine("Please enter the person's first name: ");
                        string firstName = Console.ReadLine();

                        Console.WriteLine("Please enter the person's last name: ");
                        string lastName = Console.ReadLine();

                        Console.WriteLine("Please enter the person's date of birth (DD-MM-YYYY): ");
                        string dob = Console.ReadLine();

                        Console.WriteLine("Please enter the person's phone number (no hyphens or special characters): ");
                        string phoneNum = Console.ReadLine();

                        string personsInfo = firstName + ","+ lastName + "," + dob + "," + phoneNum;

                        //send person data to the server
                        int byteCount = Encoding.ASCII.GetByteCount(personsInfo);
                        byte[] sendData = new byte[byteCount];
                        sendData = Encoding.ASCII.GetBytes(personsInfo);
                        networkConnection.write(sendData);

                        //recieve response from the server
                        byte[] receivedMessage = new byte[100];
                        networkConnection.Stream.Read(receivedMessage, 0, receivedMessage.Length);
                        string operationStatus = Encoding.ASCII.GetString(receivedMessage, 0, receivedMessage.Length);
                        Console.WriteLine(operationStatus);

                        networkConnection.Stream.Close();
                        networkConnection.Client.Close();
                        break;

                    //get all the person data stored in the server
                    case "2":
                        //send user choice to the server
                        userChoiceByte = Encoding.ASCII.GetBytes(userChoice);
                        networkConnection.write(userChoiceByte);

                        //get person data from server
                        BinaryFormatter bf = new BinaryFormatter();
                        var personList = (List<string>)bf.Deserialize(networkConnection.Stream);

                        networkConnection.Stream.Close();
                        networkConnection.Client.Close();

                        if (personList.Count == 0)
                        {
                            Console.WriteLine("There are no persons stored in the database.");
                        }
                        else
                        {
                            Console.WriteLine("First Name         |Last Name          |DOB           |Phone Number");
                            Console.WriteLine("___________________|___________________|______________|____________");
                            for (int i = 0; i < personList.Count; i++)
                            {
                                Console.WriteLine(personList[i]);
                            }
                        }

                        break;

                    //close the connection with the server and terminate the client
                    case "3":
                        Console.WriteLine("Good Bye!");
                        //send user choice to the server
                        userChoiceByte = Encoding.ASCII.GetBytes(userChoice);
                        networkConnection.write(userChoiceByte);
                        networkConnection.Stream.Close();
                        networkConnection.Client.Close();
                        clientExecution = false;
                        break;
                }
            }
        }
    }
}
