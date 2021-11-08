using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerLab
{
    class MyAlarm:MyTimer
    {
        public bool IsTime(int hrs,int mnt,int sec)
        {
            if (hrs == hours)
            {
                if (mnt == minutes)
                {
                    if (sec == seconds)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }


    }
}
