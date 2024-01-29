﻿
namespace BlApi;

public interface IEngineer
{
    public int Create(BO.Engineer boStudent);

    public BO.Engineer? Read(int id);
    public IEnumerable <BO.Engineer?> ReadAll();
    public void Delete(int id);
   
}
