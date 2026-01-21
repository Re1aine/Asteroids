using System;
using System.Collections.Generic;
using System.Linq;
using Code.Logic.Services.Repository;

namespace Code.Logic.Gameplay.Services.Holders.RepositoriesHolder
{
    public class RepositoriesHolder : IRepositoriesHolder
    {
        private readonly Dictionary<Type, IRepository> _repositories;

        public RepositoriesHolder(IEnumerable<IRepository> repositories)
        {
            _repositories = repositories.ToDictionary(x => x.GetType(), x => x);
        }
    
        public void LoadAll()
        {
            foreach (var repository in _repositories) 
                repository.Value.Load();
        }

        public void SaveAll()
        {
            foreach (var repository in _repositories)
            {
                repository.Value.Update();
                repository.Value.Save();
            }
        }
    
        public void DeleteAll()
        {
            foreach (var repository in _repositories) 
                repository.Value.Delete();
        }

        public T GetRepository<T>() where T : IRepository => 
            (T)_repositories[typeof(T)];
    }
}