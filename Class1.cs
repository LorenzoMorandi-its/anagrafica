using System;

namespace AnagraficaApp.Model
{
    public class Persona
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataNascita { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string Telefono { get; set; }

        public Persona(int id, string nome, string cognome)
        {
            ID = id;
            Nome = nome;
            Cognome = cognome;
        }
    }
}
