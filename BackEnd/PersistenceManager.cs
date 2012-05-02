using System.Collections.Generic;
using System.Linq;

namespace BackEnd
{
    public class PersistenceManager
    {
        public void Update(Note note)
        {
            //var result = Notes.Single(x => x.Id == note.Id);
            //result.Content = note.Content;
            //result.Title = note.Title;

            var updatedNote = (from c in NoteDataContext.Current.Notes
                               where c.Id == note.Id
                               select c).First();

            updatedNote.Title = note.Title;
            updatedNote.Content = note.Content;

            NoteDataContext.Current.SubmitChanges();
        }

        public void Add(Note note)
        {
            //Notes.Add(note);

            NoteDataContext.Current.Notes.InsertOnSubmit(note);
            NoteDataContext.Current.SubmitChanges();
        }

        public List<Note> Load()
        {
            var query = from selectedNote in NoteDataContext.Current.Notes
                        orderby selectedNote.Id
                        select selectedNote;

            return query.ToList();
        }

        public void Delete(Note note)
        {
            NoteDataContext.Current.Notes.DeleteOnSubmit(note);
            NoteDataContext.Current.SubmitChanges();
        }

        public Note Retrieve(int id)
        {
            return (from c in NoteDataContext.Current.Notes
                    where c.Id == id
                    select c).First();
        }
    }
}
