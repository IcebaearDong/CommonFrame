using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

namespace TimeSystem
{
    /// <summary>  
    /// 定时任务管理器  
    /// </summary>  
    public class TimeMgr : Singleton<TimeMgr>
    {
        /// <summary>  
        /// 定时任务列表  
        /// </summary>  
        private List<TimeTask> taskList = new List<TimeTask>();

        /// <summary>  
        /// 添加定时任务  
        /// </summary>  
        /// <param name="timeDelay">延时执行时间间隔(毫秒)</param>  
        /// <param name="repeat">重复执行次数</param>  
        /// <param name="timeTaskCallback">执行回调</param>  
        public void AddTask(uint timeDelay, uint repeatCount, Action callBack)
        {
            AddTask(new TimeTask(timeDelay, repeatCount, callBack));
        }
        private void AddTask(TimeTask taskToAdd)
        {
            if (taskList.Contains(taskToAdd) || taskToAdd == null) return;
            taskList.Add(taskToAdd);
        }

        /// <summary>  
        /// 移除定时任务  
        /// </summary>  
        /// <param name="taskToRemove"></param>  
        /// <returns></returns>  
        public bool RemoveTask(Action taskToRemove)
        {
            if (taskList.Count == 0 || taskToRemove == null) return false;
            for (var i = 0; i < taskList.Count; i++)
            {
                TimeTask item = taskList[i];
            }
            return false;
        }

        public void Update()
        {
            Tick();
        }

        /// <summary>  
        /// 执行定时任务  
        /// </summary>  
        private void Tick()
        {
            if (taskList == null || taskList.Count == 0) return;
            uint deltaTime = (uint)(Time.deltaTime * 1000);
            for (int i = 0; i < taskList.Count; ++i)
            {
                TimeTask task = taskList[i];
                task.TimeDelay = task.TimeDelay >= deltaTime ? task.TimeDelay - deltaTime : 0;
                if (task.TimeDelay == 0)
                {
                    task.CallBack?.Invoke();
                    if (!task.IsRepeat)
                        task.RepeatCount -= 1;

                    task.TimeDelay = task.TimeDelayReadOnly;
                    if (!task.IsRepeat && task.RepeatCount == 0)
                        taskList.Remove(task);
                }
            }
        }
    }

    /// <summary>  
    /// 定时任务封装类  
    /// </summary>  
    public class TimeTask
    {
        public Action CallBack { get; }

        public uint TimeDelay { get; set; }

        public uint RepeatCount { get; set; }

        public bool IsRepeat { get; }

        public uint TimeDelayReadOnly { get; }


        //构造函数  
        public TimeTask(uint timeDelay, uint repeat, Action callBack)
        {
            TimeDelay = timeDelay;
            TimeDelayReadOnly = timeDelay;
            RepeatCount = repeat;
            IsRepeat = repeat == 0;
            CallBack = callBack;
        }
    }
}