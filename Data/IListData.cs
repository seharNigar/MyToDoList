
using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data
{
    public interface IListData
    {
        IEnumerable<ToDoList> GetAll();

        ToDoList GetById(int id);
        ToDoList update(ToDoList updatedList);

        ToDoList Add(ToDoList newList);

        ToDoList Delete(int id);
        IEnumerable<ToDoList> GetToDoListByUserId(string id);
        int commit();


    }
}
