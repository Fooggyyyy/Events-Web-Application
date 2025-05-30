﻿using Events_Web_Application.src.Domain.Enums;
using System.Runtime.CompilerServices;

namespace Events_Web_Application.src.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public Category Category { get; set; }
        public int MaxUser { get; set; }
        public ICollection<string> PhotoPath { get; set; }

        public ICollection<Participation> Participations;

        public Event()
        {

        }

        public Event(int id, string name, string description, DateTime date, string place, Category category, int maxUser, ICollection<string> photoPath, ICollection<Participation> participations)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            Place = place;
            Category = category;
            MaxUser = maxUser;
            PhotoPath = photoPath;
            Participations = participations;
        }
    }
}
