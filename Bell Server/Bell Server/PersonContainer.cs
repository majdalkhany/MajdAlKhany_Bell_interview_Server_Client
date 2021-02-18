using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
/**
 * This class contains operations that will be used
 * to store and retrieve person objects from the database
 */

namespace Bell_Server
{
    class PersonContainer
    {
        private List<Person> personList;

        public PersonContainer()
        {
            this.personList = new List<Person>();
        }

        //add a new person entry into the database
        //Receives a comma separated string that contains the persons name, date of birth and phone number
        public void addPerson(string personStringRepresentation)
        {
            //split the string into an array of the person's attributes
            string[] personAttributes = personStringRepresentation.Split(',');

            try
            {
                Person newPerson = new Person(personAttributes[0].ToString(), personAttributes[1].ToString(),
                    personAttributes[2].ToString(), personAttributes[3].ToString());

                //add the entry into the database.txt file
                File.AppendAllText(@"database.txt", newPerson.dbRepresentation() + Environment.NewLine);

            }
            //throw exception if the inputted birthday or phone number is incorrect, do not create a new entry
            catch (Exception e)
            {
                throw e;
            }
        }

        /**
         * remove person method would be implemented here but not needed now, as per the provided requirements
         * 
         */

        //builds on top of getPersons(), returns persons list as a list of strings rather than a list of person objects
        //used to send person data to the client
        public List<string> getPersonsString()
        {
            getPersons();
            List<string> databaseEntriesListStr = new List<string>();

            for (int i = 0; i < this.personList.Count; i++)
            {
                databaseEntriesListStr.Add(this.personList[i].ToString());
            }

            return databaseEntriesListStr;
        }

        /**
         * Helper functions
         */
        //pass string in db repesenation style to parse it and return a person object
        private Person parsePerson(string personDBRepresentation)
        {
            string[] personAttributes = personDBRepresentation.Split(',');
            return new Person(personAttributes[0], personAttributes[1], personAttributes[2], personAttributes[3]);
        }

        //get all persons stored in the DB, parse them into Person objects, store them in list, and return the list of persons
        private List<Person> getPersons()
        {
            var databaseEntries = File.ReadAllLines("database.txt");
            List<string> databaseEntriesList = new List<string>(databaseEntries);
            personList.Clear();

            for (int i = 0; i < databaseEntriesList.Count; i++)
            {
                personList.Add(parsePerson(databaseEntriesList[i]));
            }
            return personList;
        }

    }
}
