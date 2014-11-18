using System;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Person : DataBaseId
    {
        public Person ()
        {
            Active = true;
        }
        public Person(int id,bool active, string firstName, string lastName, DateTime birthDate,
            int documentId, int contactNumberId, int addressId, 
            int userId, int roleId, string baseCode)
        {
            Id = id;
            Active = active;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            DocumentId = documentId;
            ContactNumberId = contactNumberId;
            AddressId = addressId;
            UserId = userId;
            RoleId = roleId;
            BaseCode = baseCode;
        }

        public Person(int id, bool active, string firstName, string lastName, DateTime birthDate,
            Document document, ContactNumber contactNumber, Address address,
            User user, int roleId, string baseCode)
        {
            Id = id;
            Active = active;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Document = document;
            ContactNumber = contactNumber;
            Address = address;
            User = user;
            RoleId = roleId;
            BaseCode = baseCode;
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }

        public int ContactNumberId { get; set; }
        public ContactNumber ContactNumber { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public string BaseCode { get; set; }
    }

}
