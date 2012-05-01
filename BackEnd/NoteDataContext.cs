using System.Data.Linq;

namespace BackEnd
{
    public class NoteDataContext : DataContext
    {
        public Table<Note> Notes;

        private NoteDataContext(string connectionString) : base(connectionString)
        {
        }

        static NoteDataContext _dataContext;
        public static NoteDataContext Current
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new NoteDataContext("isostore:/notes.sdf");
                    if (!_dataContext.DatabaseExists())
                    {
                        _dataContext.CreateDatabase();
                    }
                }
                return _dataContext;
            }
        }
    }
}
