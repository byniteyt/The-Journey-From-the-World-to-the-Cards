using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    string state;
    private string dataPath;//lo usaremos para crear una nueva carpeta
    private string filePath;//lo usaremos para crear un nuevo archivo
    private string streaminTextFile;//se encargará de escribir texto en otros archivos
    private string xmlLevelProgress;
    private string xmlWeapons;
    private string jsonWeapons;
    private List<Deck> weaponInventory = new List<Deck>
    {
    };

    private void Awake()
    {
        dataPath = Application.persistentDataPath + "/Player_Data/";
        filePath = dataPath + "Save_Data.txt";
        streaminTextFile = dataPath + "Streaming_Data_Text.txt";
        xmlLevelProgress = dataPath + "Progress_Data.xml";
        xmlWeapons = dataPath + "WeaponInventory.xml";
        jsonWeapons = dataPath + "WeaponJSON.json";
        //Debug.Log(dataPath);
    }
    void Start()
    {
        Initialize();
    }

    void Update()
    {

    }

    private void Initialize()
    {
        state = "Data Manager initialized...";
        Debug.Log(state);
        #region Usados
        ///--------------TXT----------------
        FileSystemInfo();
        NewDirectory();
        NewFileTxt();
        UpdateTextFile();
        ReadFromFile(filePath);

        ///-------------Stream--------------
        WriteToStream(streaminTextFile);
        ReadFromStream(streaminTextFile);

        ///--------------XML----------------
        WritteToXML(xmlLevelProgress);
        ReadFromStream(xmlLevelProgress);
        SerializeXML();
        DeserializeXML();

        ///-------------JSON----------------
        SerializeJSON();
        DeserializeJSON();
        ///
        #endregion

    }

    #region Txt
    void FileSystemInfo()//Imprimir por pantalla propiedades del directorio
    {
        Debug.LogFormat("Path separator character: {0}", Path.PathSeparator);
        Debug.LogFormat("Directory separator character: {0}", Path.DirectorySeparatorChar);
        Debug.LogFormat("Current directory: {0}", Directory.GetCurrentDirectory());//donde se guarda el proyecto
        Debug.LogFormat("Temporary path: {0}", Path.GetTempPath());//ubicación de carpeta temporal del sistema
    }
    void NewDirectory()//creamos nuevo directorio
    {
        if (Directory.Exists(dataPath))
        {
            Debug.Log("Directory already exists...");
            return;
        }
        Directory.CreateDirectory(dataPath);
        Debug.Log("New dierctory created...");
    }
    void DeleteDirectory()//eliminamos un directorio existente
    {
        if (!Directory.Exists(dataPath))
        {
            Debug.Log("Directory doesn't exist or has already been deleted...");
            return;
        }
        Directory.Delete(dataPath);
        Debug.Log("Directory successfully deleted...");
    }
    void NewFileTxt()//Creamos un archivo de texto
    {
        if (File.Exists(filePath))
        {
            Debug.Log("File already exists...");
            return;
        }
        File.WriteAllText(filePath, "<SAVE DATA>\n");
        Debug.Log("New file created...");
    }
    void UpdateTextFile()//Escribimos sobre el archivo ya creado
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        File.AppendAllText(filePath, $"Game started:{DateTime.Now}\n");
        Debug.Log("File updated successfully...");
    }
    void ReadFromFile(string fileName)//La consola nos muestra toda la información del archivo
    {
        if (!File.Exists(fileName))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        Debug.Log(File.ReadAllText(fileName));
    }
    void DeleteFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Debug.Log("file doesn't exists or has already been deleted...");
            return;
        }
        File.Delete(fileName);
        Debug.Log("File successfully deleted");
    }
    #endregion

    #region Stream
    void WriteToStream(string fileName)
    {
        if (!File.Exists(fileName))
        {
            ///---------------Forma más segura--------------
            using (StreamWriter newStream = File.CreateText(fileName))
            {
                newStream.WriteLine("<Save Data> for GAME\n");
            }

            ///---------------Forma secuundaria-------------
            /*StreamWriter newStream = File.CreateText(fileName);
            newStream.WriteLine("<Save Data> for GAME");
            newStream.Close();*/
            Debug.Log("New file created wuth StreamWriter!");
        }
        StreamWriter sw = File.AppendText(fileName);
        sw.WriteLine("Game ended: " + DateTime.Now);
        sw.Close();
        Debug.Log("File contents updated with StreamWriter");
    }
    void ReadFromStream(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        StreamReader sr = new StreamReader(fileName);
        Debug.Log(sr.ReadToEnd());
    }
    #endregion

    #region XML
    void WritteToXML(string fileName)
    {
        if (!File.Exists(fileName))
        {
            FileStream xmls = File.Create(fileName);
            XmlWriter xmlw = XmlWriter.Create(xmls);
            xmlw.WriteStartDocument();
            xmlw.WriteStartElement("level_progress");
            for (int i = 0; i < 5; i++)
            {
                xmlw.WriteElementString("level", "Level- " + i);
            }
            xmlw.WriteEndElement();
            xmlw.Close();
            xmls.Close();
        }
    }
    public void SerializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Deck>));
        using (FileStream stream = File.Create(xmlWeapons))
        {
            xmlSerializer.Serialize(stream, weaponInventory);
        }
    }

    public void DeserializeXML()
    {
        if (File.Exists(xmlWeapons))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Deck>));
            using (FileStream stream = File.OpenRead(xmlWeapons))
            {
                var weapons = (List<Deck>)xmlSerializer.Deserialize(stream);
                foreach (var weapon in weapons)
                {
                    Debug.LogFormat("Weapon: {0} - Damage:{1}", weapon.name, weapon.GetDeck().Count);
                }
            }
        }
    }
    #endregion

    #region JSON
    public void SerializeJSON()
    {
        //Unity no soporta listas en JSON por lo que pasamos un objeto de una clase que tenga de atributo una lista
        
        string jsonString = JsonUtility.ToJson(weaponInventory, true);
        using (StreamWriter stream = File.CreateText(jsonWeapons))
        {
            stream.WriteLine(jsonString);
        }
    }
    public void DeserializeJSON()
    {
        if (File.Exists(jsonWeapons))
        {
            using (StreamReader stream = new StreamReader(jsonWeapons))
            {
                var jsonString = stream.ReadToEnd();
                var weaponData = JsonUtility.FromJson<List<Deck>>(jsonString);
                foreach (var weapon in weaponData)
                {
                    Debug.LogFormat("Deck: {0} - Damage:{1}", weapon.name, weapon.GetDeck().Count);
                }
            }
        }
    }
    #endregion

}
