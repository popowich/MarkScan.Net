using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorReleases
{
    public partial class FormCreatorReleases 
    {
        public void OrderTasks()
        {
            System.Threading.Thread thr = null;
            thr = new System.Threading.Thread(() =>
            {
                thr.IsBackground = true;

                while (this.isRun)
                {
                    bool allowRun = this.tasks.Find(ts => ts.State == EStateTask.Performed) == null;
                    int index = this.tasks.Count - 1;
                    while(this.tasks.Count > 0)
                    {
                        if(allowRun && this.tasks[index].State == EStateTask.Pending)
                        {
                            this.tasks[index].TaskOblect.Begin().Wait();
                            lock (this.tasks)
                            {
                                this.tasks.RemoveAt(index);
                                index = this.tasks.Count - 1;
                            }
                        }
                    }

                    System.Threading.Thread.Sleep(100);
                }

            }); thr.Start();
        }
    }
}
