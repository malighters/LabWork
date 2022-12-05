using System;


namespace LabWork
{
    public class Animal
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint CellNumber { get; set; }
        public Char Gender { get; set; }
        public string Breeds { get; set; }
        public string? Notes { get; set; }

        public Animal(uint _id, string _name, uint _cellNumber, char _gender, string _breeds, string _notes)
        {
            Id = _id;
            Name = _name;
            CellNumber = _cellNumber;
            Gender = _gender;
            Breeds = _breeds;
            Notes = _notes;
        }

        public Animal() { }

    }

}

