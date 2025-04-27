namespace DoreDoreWeb.Models.Repository
{
    public class GenericRepository <T> where T : class
    {
        private readonly DbfinalProjeContext _context;

        public GenericRepository(DbfinalProjeContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            {
                return _context.Set<T>().ToList();
            }
        }

    }
}
