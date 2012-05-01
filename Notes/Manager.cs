using System;
using System.Collections.ObjectModel;
using System.Linq;
using BackEnd;

namespace Notes
{
    public class Manager
    {
        public ObservableCollection<Note> Notes { get; set; }

        public Manager()
        {
            try
            {
                Notes = new ObservableCollection<Note>();
                Load();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public void Update(Note note)
        {
            var result = Notes.Single(x => x.Id == note.Id);
            result.Content = note.Content;
            result.Title = note.Title;

            var updatedNote = (from c in NoteDataContext.Current.Notes
                              where c.Id == note.Id
                              select c).First();
            
            updatedNote.Title = note.Title;
            updatedNote.Content = note.Content;
            
            NoteDataContext.Current.SubmitChanges();
        }

        public void Add(Note note)
        {
            Notes.Add(note);

            NoteDataContext.Current.Notes.InsertOnSubmit(note);
            NoteDataContext.Current.SubmitChanges();
        }

        public void Load()
        {
            var query = from selectedNote in NoteDataContext.Current.Notes
                        orderby selectedNote.Id
                        select selectedNote;

            Notes =  new ObservableCollection<Note>(query.ToList());
        }

        public void Delete(Note note)
        {

            Notes.Remove(note);

            NoteDataContext.Current.Notes.DeleteOnSubmit(note);
            NoteDataContext.Current.SubmitChanges();
        }

        public Note Retrieve(int id)
        {
            return (from c in NoteDataContext.Current.Notes
                    where c.Id == id
                    select c) as Note;
        }
    }
}
