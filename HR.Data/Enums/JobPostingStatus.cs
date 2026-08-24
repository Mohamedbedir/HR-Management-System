using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Enums
{
    public enum JobPostingStatus
    {
        Draft = 1,       // مسودة (تحت الكتابة ولم تنشر بعد)
        Published = 2,   // منشورة (متاحة للتقديم حالياً)
        OnHold = 3,      // معلقة مؤقتاً (للمراجعة أو الاكتفاء المؤقت)
        Closed = 4,      // مغلقة (تم انتهاء وقت التقديم)
        Filled = 5,      // تم التعيين (تم اختيار موظف وإغلاق الوظيفة بنجاح)
        Canceled = 6     // ملغاة (تم إلغاء طلب الوظيفة من قِبل الإدارة)
    }
}
