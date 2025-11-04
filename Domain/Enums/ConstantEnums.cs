
namespace Domain.Enums
{
    public class ConstantEnums
    {
        public enum EntityStatus
        {
            NORMAL = 1,
            OK = 2,
            NOT_OK = 3,
            TEMP = 10,
            LOCK = 98,
            DELETED = 99,
        }
        public enum TypeAction
        {
            VIEW = 0,
            CREATE = 1,
            UPDATE = 2,
            DELETED = 3,
            IMPORT = 4,
            EXPORT = 5,
            PRINT = 6,
            EDIT_ANOTHER_USER = 7,
            MENU = 8,
            LOG_IN = 9,
            LOG_OUT = 10
        }
        public enum Gender
        {
            MALE = 0,
            FEMALE = 1,
        }
        public enum TypeRole
        {
            ADMIN = 0,
            USER = 1,
        }
    }
}
