
namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

public class GanttImplementation : BlApi.IGantt
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

   /* public BO.Gantt? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        DO.Task? doEng = _dal.Task.Read(id);

        BO.Task task = new BO.Task()
        {
            TaskId=_dal.Task
         TaskName;
         EngineerId;
         EngineerName;
         Description;
         StartDate;
    };

      
        return task;
    }*/

    public IEnumerable<Gantt> ReadAll(Func<Gantt, bool> p = null)
    {
        IEnumerable<BO.Gantt> tasks = (from DO.Task item in _dal.Task.ReadAll(null)
                                       select new BO.Gantt()
                                       {
                                           TaskId = item.TaskId,
                                           TaskName = item.NickName,
                                           EngineerId = item.EngineerId,
                                           //   EngineerName = item.Engineer,
                                           Description = item.Description,
                                           StartDate = item.StartDate

                                       }) ;
        return tasks;
    }
}


