using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace StudentsManager.Mvc.Persistence
{
    public static class DbUpdateExceptionExtensions
    {
        public static bool IsUniqueConstraintViolation(this DbUpdateException exception)
        {
            for (var innerException = exception.InnerException;
                 innerException != null;
                 innerException = innerException.InnerException)
            {
                if (innerException is SqlException { Number: 2601 or 2627 })
                    return true;
            }

            return false;
        }
    }
}
