using Newtonsoft.Json;
using System;
using System.IO;

namespace PesssoaLibrary
{
    public class JsonManager<T> : IReadWriteFile<T> where T : class
    {
        public String Path { get; set; }

       public JsonManager(string path)
        {
            this.Path = path ?? throw new ArgumentNullException();
        }
       
      
        public void Serializer(T obj)
        {
          
            if (File.Exists(Path))
                File.Delete(Path);



            using (FileStream fileStream = new FileStream(Path, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    var json = JsonConvert.SerializeObject(obj);
                    sw.Write(json);
                }
            }
        }
       
        public T Deserializer()
        {
            using (FileStream fileStream = new FileStream(Path, FileMode.OpenOrCreate))
            {
                using (StreamReader sw = new StreamReader(fileStream))
                {
                    var json = sw.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
        }
    }
}
