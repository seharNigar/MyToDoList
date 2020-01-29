using System;
using System.Collections.Generic;
using System.Linq;
using Models;
namespace Data
{
    public class SqlListData : IListData
    {
        private readonly ToDoListDbContext db;

      
        public SqlListData( ToDoListDbContext db)
        {
            this.db = db;
        }
        public ToDoList Add(ToDoList newList)
        {
            db.Add(newList);

           
            return newList;
        }

        public int commit()
        {
           return  db.SaveChanges();
        }

        public ToDoList Delete(int id)
        {
            var list = GetById(id);
            if (list != null)
            {
                db.TheList.Remove(list);
            
            }
            return list;
        }

        public ToDoList GetById(int id)
        {
            return db.TheList.Find(id);
        }
        public IEnumerable<ToDoList> GetToDoListByUserId(string id)
        {
           // return db.TheList.Where(p => p.UserId == id);
            var query=from l in db.TheList
                      where(l.UserId==id)
                      orderby l.Duration.Day ascending, l.Duration.Month ascending, l.Duration.TimeOfDay ascending
                      select l;
            return query;


        }
        public IEnumerable<ToDoList> GetAll()
        {
            var query = from l in db.TheList
                        orderby l.Duration.Day ascending, l.Duration.Month ascending, l.Duration.TimeOfDay ascending
                        select l;
            return query;




        }
        public ToDoList update(ToDoList updatedList)
        {
            var entity = db.TheList.Attach(updatedList);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return updatedList;
        }
    }
}
