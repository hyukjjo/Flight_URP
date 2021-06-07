
using System;
using System.Collections.Generic;

[Serializable]
public abstract class CSVClass<T> where T : struct
{
    public Dictionary<int, T> dataDic = new Dictionary<int, T>();

    public abstract bool LoadCSV(string file);
}
