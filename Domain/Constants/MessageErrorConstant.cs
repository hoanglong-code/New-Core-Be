using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public class MessageErrorConstant
    {
        #region common
        public const string NOT_FOUND = "Lỗi không tồn tại dữ liệu!";
        public const string UNKNOWN_ERROR = "Có lỗi sảy ra!";
        public const string AUTHORIZED = "Lỗi xác thực!";
        public const string DATA_INVALID = "Sai dữ liệu!";
        public const string ACCTION_SUCCESS = "Thành công!";
        public const string EXCEPTION_UNKNOWN = "Lỗi ngoại lệ chưa xác định!";
        public const string ADD_SUCCESS = "Thêm mới thành công!";
        public const string UPDATE_SUCCESS = "Sửa thông tin thành công!";
        public const string UNAUTHORIZE = "Lỗi xác thực!";
        public const string UNVALIDATION = "Lôi dữ liệu request!";
        public const string ERROR_DATA_EXISTED = "Dữ liệu đã tồn tại!";
        public const string DELETE_SUCCESS = "Xóa bản ghi thành công!";
        public static string MISS_DATA_MESSAGE = "Thông tin không đủ!";
        public static string BAD_REQUEST_MESSAGE = "Lỗi sai dữ liệu!";
        public static string NOT_FOUND_VIEW_MESSAGE = "Không tìm thấy mục cần xem!";
        public static string NOT_FOUND_UPDATE_MESSAGE = "Không tìm thấy mục cần sửa!";
        public static string NOT_FOUND_DELETE_MESSAGE = "Không tìm thấy mục cần xóa!";
        public static string ERROR_EXIST_MESSAGE = "Mục này đã tồn tại!";
        public static string ERROR_400_MESSAGE = "Có lỗi xảy ra. Xin vui lòng thử lại sau!";
        public static string ERROR_500_MESSAGE = "Hệ thống xảy ra lỗi. Xin vui lòng thử lại sau!";
        public const string INVALID = "Không hợp lệ!";
        #endregion

        #region role
        public const string NOPERMISION_VIEW_MESSAGE = "Bạn không có quyền xem dữ liệu tới mục này!";    //error_code = 222
        public const string NOPERMISION_UPDATE_MESSAGE = "Bạn không có quyền cập nhật mục này!";   //error_code = 222
        public const string NOPERMISION_CREATE_MESSAGE = "Bạn không có quyền thêm mới mục này!";   //error_code = 222
        public const string NOPERMISION_DELETE_MESSAGE = "Bạn không có quyền xoá mục này!";   //error_code = 222
        public const string NOPERMISION_ACCEPT_MESSAGE = "Bạn không có quyền duyệt đơn đăng ký!";
        public const string NOPERMISION_ACTION_MESSAGE = "Bạn không có quyền thực hiện thao tác này!";
        #endregion

        #region user
        public const string USER_NOT_FOUND = "Người dùng không tồn tại!";
        #endregion
    }
}
