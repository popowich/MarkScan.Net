using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorReleases
{
    public class TaskExecution
    {
        /// <summary>
        /// Тип задачи
        /// </summary>
        public ETypeTask Type { get; set; }
        /// <summary>
        /// Состояние задачи
        /// </summary>
        public EStateTask State { get; set; }
        /// <summary>
        /// Объект задачи
        /// </summary>
        public ITaskExecution TaskOblect { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_taskOblect"></param>
        public TaskExecution(ITaskExecution _taskOblect)
        {
            this.TaskOblect = _taskOblect;
            this.State = EStateTask.Pending;
        }
    }
    /// <summary>
    /// Перечисление состояний задачи
    /// </summary>
    public enum EStateTask
    {
        Pending,
        Performed,
        Completed
    }
    /// <summary>
    /// Типы задач
    /// </summary>
    public enum ETypeTask
    {
        BildReleas,
        PublicationReleas
    }


    public interface ITaskExecution
    {
        Task Begin();
    }
}
