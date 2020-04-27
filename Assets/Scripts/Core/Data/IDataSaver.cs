using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Data
{
    public interface IDataSaver<T> where T : IDataStore
    {

        void Save(T data);

        bool Load(out T data);

        void Delete();

    }

}
