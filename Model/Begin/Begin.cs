using System;
using System.Threading;
using ToastFish.Model.PushControl;
using ToastFish.Model.SqliteControl;

namespace ToastFish.Model.Begin
{
    public class CommonBegin
    {
        Thread thread = new Thread(new ParameterizedThreadStart(PushWords.Recitation));

        private CommonBegin()
        {
            
        }
        public void Begin_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists("Log"))  {
                System.IO.Directory.CreateDirectory("Log");
            }
           // System.IO.Directory.CreateDirectory("Log");

            var state = thread.ThreadState;

            WordType Words = new WordType();
            Words.Number = Select.WORD_NUMBER;

            if (state == System.Threading.ThreadState.WaitSleepJoin || state == System.Threading.ThreadState.Stopped)
            {
                thread.Abort();
                while (thread.ThreadState != System.Threading.ThreadState.Aborted)
                {
                    Thread.Sleep(100);
                }
                if(Select.TABLE_NAME == "Goin")
                    thread = new Thread(new ParameterizedThreadStart(PushGoinWords.OrderGoin));
                else if(Select.TABLE_NAME == "StdJp_Mid")
                    thread = new Thread(new ParameterizedThreadStart(PushJpWords.Recitation));
                //else if (Select.TABLE_NAME == "自定义英语")
                //    thread = new Thread(new ParameterizedThreadStart(PushWords.Recitation));
                else
                    thread = new Thread(new ParameterizedThreadStart(PushWords.RecitationSM2));

                thread.Start(Words);
            }
            else
            {
                if (Select.TABLE_NAME == "Goin")
                    thread = new Thread(new ParameterizedThreadStart(PushGoinWords.OrderGoin));
                else if (Select.TABLE_NAME == "StdJp_Mid")
                    thread = new Thread(new ParameterizedThreadStart(PushJpWords.Recitation));
                //else if (Select.TABLE_NAME == "自定义英语")
                //    thread = new Thread(new ParameterizedThreadStart(PushWords.Recitation));
                else
                    thread = new Thread(new ParameterizedThreadStart(PushWords.RecitationSM2));
                
                thread.Start(Words);
            }
        }

        public static CommonBegin GetBegin()
        {
            CommonBegin commonBegin = new CommonBegin();
            return commonBegin;
        }

        public Thread GetThread()
        {
            return thread;
        }
    }
}