using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Service
{
	/// <summary>
	/// Класс JsonDataHandler предназначен для работы с данными в формате JSON.
	/// Использует библиотеку Newtonsoft.Json для сериализации и десериализации объектов.
	/// </summary>
	public class JsonDataHandler
	{
		/// <summary>
		/// Загружает данные из файла JSON и преобразует их в указанный тип.
		/// </summary>
		/// <typeparam name="T">Тип данных, в который будут десериализованы данные из JSON.</typeparam>
		/// <param name="path">Путь к JSON-файлу.</param>
		/// <returns>Десериализованные данные указанного типа.</returns>
		public T LoadData<T>(string path)
		{
			// Чтение содержимого JSON-файла
			var jsonData = File.ReadAllText(path);

			// Преобразование (десериализация) JSON-строки в объект указанного типа
			return JsonConvert.DeserializeObject<T>(jsonData);
		}

		/// <summary>
		/// Сохраняет данные в файл в формате JSON.
		/// </summary>
		/// <typeparam name="T">Тип данных, которые будут сериализованы.</typeparam>
		/// <param name="data">Объект, который нужно сериализовать в JSON.</param>
		/// <param name="path">Путь для сохранения JSON-файла.</param>
		public void SaveData<T>(T data, string path)
		{
			// Преобразование (сериализация) объекта в JSON-строку с отступами для удобства чтения
			var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);

			// Запись JSON-строки в файл
			File.WriteAllText(path, jsonData);
		}
	}

}
