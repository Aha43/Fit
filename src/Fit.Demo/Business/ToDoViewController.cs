﻿using Fit.Demo.Domain;
using Fit.Demo.Specification;
using Fit.Demo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Demo.Business;

public class ToDoViewController
{
    private readonly IToDoApi _api;

    public ToDoValidator ValidationRules { get; private set; }

    public ToDoViewController(
        IToDoApi api,
        ToDoValidator validationRules)
    {
        _api = api;
        ValidationRules = validationRules;
    }
  
    public List<ToDo> ToDos { get; } = new List<ToDo>();

    public ToDo NewToDo { get; private set; } = new();

    public async Task LoadAsync()
    {
        var toDos = await _api.ReadAsync(null, default);
        ToDos.Clear();
        ToDos.AddRange(toDos);
    }

    public async Task CreateToDo()
    {
        var res = ValidationRules.Validate(NewToDo);
        if (res.IsValid)
        {
            var created = await _api.CreateAsync(NewToDo, default);
            ToDos.Add(created);
            NewToDo = new ToDo();
            return;
        }

        throw new ArgumentException(res.ToString());
    }

}
