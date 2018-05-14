using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staff_management
{
    public class Alias
    {
        public enum Sex
        {
            男, 女
        }
        public enum Account
        {
            城镇户口, 农村户口
        }
        public enum Degree
        {
            高中 = 1, 专科, 本科, 硕士, 博士
        }
        public enum Polity
        {
            中共党员 = 1, 中共预备党员, 共青团员, 群众
        }
        public enum Health
        {
            良好 = 1, 健康, 一般, 有慢性疾病
        }
        public enum Nation
        {
            汉族 = 1, 蒙古族, 回族, 藏族, 维吾尔族, 苗族, 彝族, 壮族, 布依族,
            朝鲜族, 满族, 侗族, 瑶族, 白族, 土家族, 哈尼族, 哈萨克族, 傣族,
            黎族, 傈傈族, 佤族, 畲族, 高山族, 拉祜族, 水族, 东乡族, 纳西族,
            景颇族, 柯尔克孜族, 土族, 达斡尔族, 仫佬族, 羌族, 布朗族, 撒拉族,
            毛南族, 仡佬族, 锡伯族, 阿昌族, 普米族, 塔吉克族, 怒族, 乌孜别克族,
            俄罗斯族, 鄂温克族, 德昂族, 保安族, 裕固族, 京族, 塔塔尔族,
            独龙族, 鄂伦春族, 赫哲族, 门巴族, 珞巴族, 基诺族
        }

        public enum State
        {
            试用=1,转正,挂靠,自动离职,辞退
        }
        
    }

}
