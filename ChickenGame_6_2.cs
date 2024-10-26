using System;

// Основная программа
class Program
{
  // Команды, которые может вводить игрок
  enum PlayerAction
  {
    Exit = 0,
    Feed = 1,
    CollectEggs = 2,
    Wait = 3
  }

  // Живая курица или нет
  enum ChickenState
  {
    Dead,
    Alive
  }

  static void Main()
  {
    // Создаём три курицы
    ChickenState[] chickens = new ChickenState[3];
    // Сколько зерна у каждой курицы
    int[] grains = new int[3];
    // Есть ли яйцо у курицы
    bool[] eggs = new bool[3];

    // Делаем всех куриц живыми в начале игры
    for (int i = 0; i < 3; i++)
    {
      chickens[i] = ChickenState.Alive;
      grains[i] = 0;
      eggs[i] = false;
    }

    // Бесконечный цикл игры
    while (true)
    {
      // Проверяем, есть ли живые курицы
      int aliveChickens = 0;
      for (int i = 0; i < 3; i++)
      {
        if (chickens[i] == ChickenState.Alive)
        {
          aliveChickens = aliveChickens + 1;
        }
      }

      // Если живых куриц нет - конец игры
      if (aliveChickens == 0)
      {
        Console.WriteLine("Игра закончена - все курицы умерли!");
        Console.ReadLine();
        return;
      }

      // Показываем информацию о курицах
      Console.WriteLine("------- Наши курочки -------");

      for (int i = 0; i < 3; i++)
      {
        Console.WriteLine($"Курица {i + 1}:");
        if (chickens[i] == ChickenState.Alive)
        {
          Console.WriteLine($"  Зерна: {grains[i]}");
          Console.WriteLine($"  Яйцо: {(eggs[i] ? "Есть" : "Нет")}");
        }
        else
        {
          Console.WriteLine("  Умерла :(");
        }
        Console.WriteLine();
      }

      // Показываем меню
      Console.WriteLine("Что будем делать?");
      Console.WriteLine("1 - Покормить куриц (добавить 4 зерна)");
      Console.WriteLine("2 - Собрать яйца");
      Console.WriteLine("3 - Ничего не делать");
      Console.WriteLine("0 - Закончить игру");

      // Спрашиваем у игрока, что он хочет сделать
      Console.Write("Введите команду: ");
      string userInput = Console.ReadLine();

      // Пробуем превратить ввод в команду
      PlayerAction action;
      bool inputIsValid = Enum.TryParse(userInput, out action);

      // Если ввод неправильный - показываем ошибку
      if (!inputIsValid)
      {
        Console.WriteLine("Неправильная команда!");
        Console.WriteLine("Нажмите Enter чтобы продолжить...");
        Console.ReadLine();
        Console.Clear();
        continue;
      }

      // Выполняем то, что хочет игрок
      if (action == PlayerAction.Feed)
      {
        // Кормим каждую живую курицу
        for (int i = 0; i < 3; i++)
        {
          if (chickens[i] == ChickenState.Alive)
          {
            grains[i] = grains[i] + 4;
            Console.WriteLine($"Курица {i + 1} получила 4 зерна");
          }
        }
      }
      else if (action == PlayerAction.CollectEggs)
      {
        // Собираем яйца у каждой живой курицы
        for (int i = 0; i < 3; i++)
        {
          if (chickens[i] == ChickenState.Alive && eggs[i] == true)
          {
            eggs[i] = false;
            Console.WriteLine($"Забрали яйцо у курицы {i + 1}");
          }
        }
      }
      else if (action == PlayerAction.Wait)
      {
        Console.WriteLine("Пропускаем ход...");
      }
      else if (action == PlayerAction.Exit)
      {
        Console.WriteLine("Пока-пока!");
        return;
      }

      // Каждая живая курица делает свои действия
      for (int i = 0; i < 3; i++)
      {
        if (chickens[i] == ChickenState.Alive)
        {
          // Если есть зерно - курица ест
          if (grains[i] > 0)
          {
            grains[i] = grains[i] - 1;
            Console.WriteLine($"Курица {i + 1} съела зерно");

            // Если нет яйца - может снести новое
            if (eggs[i] == false)
            {
              eggs[i] = true;
              Console.WriteLine($"Курица {i + 1} снесла яйцо!");
            }
          }
          // Если нет зерна - курица умирает
          else
          {
            chickens[i] = ChickenState.Dead;
            Console.WriteLine($"Курица {i + 1} умерла от голода...");
          }
        }
      }

      // Ждём нажатия Enter перед следующим ходом
      Console.WriteLine("Нажмите Enter чтобы продолжить...");
      Console.ReadLine();
      Console.Clear();
    }
  }
}