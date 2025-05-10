namespace Proiect_Paoo
{
    public class User
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Role { get; set; }

        public User(int id, string nume, string prenume, string email, string role = "user")
        {
            Id = id;
            Nume = nume;
            Prenume = prenume;
            Email = email;
            Role = role;
        }
    }
}
