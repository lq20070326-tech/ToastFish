using System;
using System.Timers;
using Gma.System.MouseKeyHook;
using ToastFish.Model.PushControl;
using ToastFish.Model.Begin;

namespace ToastFish.Model.TimeControl
{
    public class MyTaskScheduler
    {
        
        //TODO
        //去掉换成kotlin识别触屏发送请求
        CommonBegin begin = CommonBegin.GetBegin();
        public static bool Timecontrol = false;
        private readonly Timer _timer;
        private IKeyboardMouseEvents _mouseHook;

        public bool IsMouseWatchActive { get; set; } = true;
        public bool MouseMovedFlag { get; set; } = false;

        public MyTaskScheduler(int intervalMs)
        {
            _timer = new Timer(intervalMs);
            _timer.Elapsed += OnTick;
            _timer.AutoReset = true;

            // 初始化鼠标钩子
            _mouseHook = Hook.GlobalEvents();
            _mouseHook.MouseMove += (s, e) =>
            {
                if (IsMouseWatchActive)
                {
                    MouseMovedFlag = true;
                }
            };
        }

        public void Start()
        {
            _timer.Start();
            Console.WriteLine("调度器已启动");
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;

            // 每日6点执行任务1
            if (now.Hour == 6 && now.Minute == 0)
            {
                TaskType1();
            }

            // 控制监测时间段：6:00~22:00开启鼠标监测
            IsMouseWatchActive = now.Hour >= 6 && now.Hour < 22;

            if (IsMouseWatchActive && MouseMovedFlag)
            {
                TaskType2();
                MouseMovedFlag = false;
            }
        }

        void TaskType1()
        {
            begin.Begin_Click(null, null);
        }

        void TaskType2()
        {
            PushWords.HotKeytObservable.raiseEvent("S");
        }

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _mouseHook?.Dispose();
        }
    }
}
