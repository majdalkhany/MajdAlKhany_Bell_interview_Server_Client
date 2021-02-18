using System;
using System.Collections.Generic;
using System.Text;
/**
 * This class is a Person representation,
 * it contains the person's name, birthday, and phone number
 */

namespace Bell_Server
{
    class Person
    {
        private string firstName;
        private string lastName;
        private DateTime birthday;
        private long phoneNumber;

        public Person(string firstName, string lastName, string birthday, string phoneNumber)
        {
            //ensure that the birthday and phone number is entered in the correct format
            //throw an exception if they are incorrect
            try
            {
                this.birthday = DateTime.ParseExact(birthday, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                this.phoneNumber = Convert.ToInt64(phoneNumber);

                this.firstName = firstName;
                this.lastName = lastName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //return annotated string representation of the person object to store in the db
        //used to store the person's data in a text file with comma as delimiter
        public string dbRepresentation()
        {
            return (this.firstName + "," + this.lastName + "," + this.birthday.ToString("dd-MM-yyyy") + "," + this.phoneNumber);
        }

        /**getters and setters for the attributes
         * would go here but they are not needed
         */

        //override toString
        public override string ToString()
        {
            string returnPerson = this.firstName.PadRight(20);
            returnPerson += this.lastName.PadRight(20);
            returnPerson += this.birthday.Date.ToString("dd-MM-yyyy").PadRight(15);
            returnPerson += this.phoneNumber.ToString().PadRight(1);
            return returnPerson;
        }

    }
}
