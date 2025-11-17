
using System.ComponentModel;

namespace Domain.Enums
{
    public class ConstantEnums
    {
        public enum EntityStatus
        {
            [Description("Bình thường")]
            NORMAL = 1,
            [Description("Chưa kích hoạt")]
            TEMP = 10,
            [Description("Khóa")]
            LOCK = 98,
            [Description("Xóa")]
            DELETED = 99,
        }
        public enum TypeAction
        {
            [Description("Menu")]
            MENU = 0,
            [Description("Danh sách")]
            READ = 1,
            [Description("Xem")]
            VIEW = 2,
            [Description("Thêm mới")]
            CREATE = 3,
            [Description("Cập nhật")]
            UPDATE = 4,
            [Description("Xóa")]
            DELETED = 5,
            [Description("Import")]
            IMPORT = 6,
            [Description("Export")]
            EXPORT = 7,
            [Description("Khác")]
            OTHER = 8,
        }
        public enum Gender
        {
            [Description("Nam")]
            MALE = 0,
            [Description("Nữ")]
            FEMALE = 1,
        }
        public enum TypeRole
        {
            [Description("Administrator")]
            ADMIN = 0,
            [Description("Người dùng")]
            USER = 1,
        }
    }
}
