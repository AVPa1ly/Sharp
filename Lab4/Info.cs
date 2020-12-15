namespace Lab4
{
    public class Info
    {
        public string Email {get; set;}
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }

        public Info()
        {

        }
        public Info(string email, string password, string firstName, string lastName, bool gender, int age, string nationality, string phoneNumber, string country, string company)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Age = age;
            Nationality = nationality;
            PhoneNumber = phoneNumber;
            Country = country;
            Company = company;
        }
    }
}
