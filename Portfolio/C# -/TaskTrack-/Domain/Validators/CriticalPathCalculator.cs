using Domain.Enum;
using Task = Domain.Utilities.Task;

namespace Domain.Validators;

public static class CriticalPathCalculator
{
       public static void Calculate(List<Task> tasks)
       {
              
              if (tasks == null)
              {
                     throw new ArgumentException("Tasks cannot be null");
              }
              List<Task> fromNoDependToDepend = NoDependToDepend(tasks);

              AssignEarlyTimes(fromNoDependToDepend);

              AssignLateTimes(fromNoDependToDepend);
              
              AssignSlack(fromNoDependToDepend);

              UpdateStatusByDependencies(fromNoDependToDepend);
       }
       
       private static void UpdateStatusByDependencies(List<Task> tasks)
       {
              foreach (Task task in tasks)
              {
                     bool depNotCompleted = task.Dependencies.Any(d => d.Status != TaskProgress.Completed);

                     if (depNotCompleted)
                     {
                            task.Status = TaskProgress.Pending;
                     }
              }
       }
       
       private static void AssignSlack(List<Task> fromNoDependToDepend)
       {
              foreach (Task task in fromNoDependToDepend)
              {
                     task.Slak = task.LateEndDate.DayNumber - task.EndDate.DayNumber;
              }
       }

       
       private static void AssignLateTimes(List<Task> fromNoDependToDepend)
       {
              if (fromNoDependToDepend == null || !fromNoDependToDepend.Any())
              {
              }
              else
              {
                     DateOnly CriticPathFinishTime = fromNoDependToDepend.Max(t => t.EndDate);
                     
                     for (int i = fromNoDependToDepend.Count - 1; i >= 0; i--)
                     {
                            Task task = fromNoDependToDepend[i];
                            
                            List<Task> dependants = fromNoDependToDepend.Where(t => t.Dependencies.Contains(task)).ToList();
                            
                            task.LateEndDate = dependants.Any() 
                                   ? dependants.Min(s => s.LateStartDate)
                                   : CriticPathFinishTime;
                            
                            task.LateStartDate = task.LateEndDate.AddDays(-task.Duration);
                     }
              }
       }

       private static void AssignEarlyTimes(List<Task> fromNoDependToDepend)
       {
              foreach (Task task in fromNoDependToDepend)
              {      
                     task.StartDate = task.Dependencies.Any() 
                            ? task.Dependencies.Max(date => date.EndDate) 
                            : task.StartDate;
                     
                     task.EndDate = task.StartDate.AddDays(task.Duration);
              }
       }
       
       /*     https://stackoverflow.com/questions/67644378/detecting-cycles-in-topological-sort-using-kahns-algorithm-in-degree-out-deg if u enter to this link, u will see that this
              function on stackoverflow is on Py. It was exhausted to translate to c# 
              i change this code (topsort) khan's algorithm, i had no idea this existed*/
       private static List<Task> NoDependToDepend(List<Task> tasks)
       {
              List<Task> fromNoDependToDepend = new List<Task>();
              
              var inDegree = tasks.ToDictionary(
                     t => t,
                     t => t.Dependencies?.Count ?? 0
              );
              
              var queue = new Queue<Task>(
                     inDegree.Where(kv => kv.Value == 0)
                            .Select(kv => kv.Key)
              );

              while (queue.Count > 0)
              {
                     Task current = queue.Dequeue();
                     fromNoDependToDepend.Add(current);

                     foreach (Task succ in tasks.Where(t => t.Dependencies.Contains(current)))
                     {
                            inDegree[succ]--;
                            if (inDegree[succ] == 0)
                                   queue.Enqueue(succ);
                     }
              }
              
              return fromNoDependToDepend;
       }
}