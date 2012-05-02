using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BackEnd
{
    public class NoteCollection : INotifyPropertyChanged
    {
        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Notes"));
                }
            }
        }

        public PersistenceManager Manager { get; set; }

        public NoteCollection()
        {
            if (Notes == null)
            {
                Notes = new ObservableCollection<Note>();    
            }
            if (Manager == null)
            {
                Manager = new PersistenceManager();
            }
            UpdateNoteCollection();
        }

        public void UpdateNoteCollection()
        {
            Notes = new ObservableCollection<Note>(Manager.Load());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
