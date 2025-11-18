using Domain.Commons;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public class CheckRoleExtension
    {
        public static bool CheckRoleByCode(string access_key, string key, ConstantEnums.TypeAction type)
        {
            var check = false;

            var functionRole = access_key.Split('-');
            for (var i = 0; i < functionRole.Count(); i++)
            {
                var code = functionRole[i].Split(':')[0];
                var role = functionRole[i].Split(':')[1];
                if (code == key)
                {
                    check = role.Substring((int)type, 1) == "1" ? true : false;
                    break;
                }
            }

            return check;
        }
        public static string PlusActiveKey(string key1, string key2)
        {
            string str = "";
            char[] str1 = key1.ToCharArray();
            char[] str2 = key2.ToCharArray();
            for (int i = 0; i < str1.Length; i++)
            {
                int k = int.Parse(str1[i].ToString()) + int.Parse(str2[i].ToString());
                if (k > 1) k = 1;
                str += k;
            }
            return str;
        }
    }
}
