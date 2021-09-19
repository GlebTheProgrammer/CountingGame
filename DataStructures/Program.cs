using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class Program
    {
        static void PrintList(LinkedList<int> list)
        {
            var node = list.First;

            while (node != null)
            {
                Console.Write(node.Value + " ");
                node = node.Next;
            }
            Console.WriteLine();
        }

        static int GetChildrenCount()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Введите, сколько ребят будут играть в считалочку(1-64): ");
                int childrenCount = int.Parse(Console.ReadLine());
                if (childrenCount <= 0 && childrenCount > 64)
                {
                    Console.WriteLine("Таоке количество детей не предусотрено, попробуйте ещё раз");
                    Console.ReadLine();
                    continue;
                }
                else
                {
                    Console.Clear();
                    return childrenCount;
                }
            }
        }

        static void Delete(LinkedList<int> childrenList, in int childrenCount, ref int[] arrayWithDeletedChildren)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Введите индекс игрока, которого вы хотите исключить: ");
                int index = int.Parse(Console.ReadLine());
                if (index <= 0 && index > childrenCount)
                {
                    Console.Clear();
                    Console.WriteLine("Такого индекса не существует, вы были отброшены к начальному меню");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    LinkedListNode<int> searchedChildren = childrenList.Find(index);
                    if (searchedChildren == null)
                    {
                        Console.Clear();
                        Console.WriteLine("Игрок с таким индексом не был найден. Возможно, он уже был удалён. Вы были отброшены к начальному меню");
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        RewriteArrayWithKickedChildren(ref arrayWithDeletedChildren, searchedChildren.Value);
                        childrenList.Remove(searchedChildren);
                        Console.Clear();
                        Console.WriteLine("Игрок был удалён");
                        Console.ReadLine();
                        return;
                    }
                }
            }
        }

        static void Delete(LinkedList<int> childrenList, ref int[] arrayWithDeletedChildren, in int indexForKicking)
        {
            LinkedListNode<int> searchedChildren = childrenList.Find(indexForKicking);
            if (searchedChildren == null)
                return;
            else
            {
                RewriteArrayWithKickedChildren(ref arrayWithDeletedChildren, searchedChildren.Value);
                childrenList.Remove(searchedChildren);
                return;
            }
        }

        static void RewriteArrayWithKickedChildren(ref int[] arrayWithChildren, in int deletedIndexOfChild)
        {
            int[] tempArray = new int[arrayWithChildren.Length + 1];
            for (int i = 0; i < arrayWithChildren.Length; i++)
                tempArray[i] = arrayWithChildren[i];
            tempArray[tempArray.Length - 1] = deletedIndexOfChild;
            arrayWithChildren = tempArray;
        }

        static void Shuffle(int[] childrenArray)
        {
            Random random = new Random();
            for (int i = childrenArray.Length-1; i >= 1; i--)
            {
                int j = random.Next(i + 1);

                int temp = childrenArray[j];
                childrenArray[j] = childrenArray[i];
                childrenArray[i] = temp;
            }

        }

        static void Main(string[] args)
        {
            LinkedList<int> childrenGameList = new LinkedList<int>();
            int[] kickedChildren = new int[0];

            int childrenCount = GetChildrenCount();

            for (int i = 1; i <= childrenCount; i++)
            {
                childrenGameList.Add(i);
            }

            Console.Clear();

            string choice = "";
            while (1 == 1)
            {
                if (childrenGameList.Count == 1)
                    break;

                if (choice != "Auto")
                {
                    Console.Clear();
                    Console.WriteLine("Хорошо, теперь определитесь, что вы хотели бы сделать (Введите команду): \n");
                    Console.WriteLine("Delete - Удалить игрока по заданному номеру");
                    Console.WriteLine("Auto - Автоматически сыграть игру и выявить победителя");
                    Console.WriteLine("ShowSaved - Показать оставшихся в кругу ребят");
                    Console.WriteLine("ShowKicked - Показать оставшихся в кругу ребят");
                    Console.WriteLine("ShowStatistic - Определить статистику 64 игр(если игра идёт, она будет прервана)\n");

                    choice = Console.ReadLine();
                }

                if (choice != "Delete" && choice != "Auto" && choice != "ShowStatistic" && choice != "ShowSaved" && choice != "ShowKicked")
                {
                    Console.WriteLine("Вы ввели что-то странное, попробуйте ввести заново");
                    Console.ReadLine();
                    continue;
                }

                if (choice == "Delete")
                {
                    Delete(childrenGameList, childrenCount, ref kickedChildren);
                    
                    if (childrenGameList.Empty)
                        break;
                    else
                        continue;
                }

                if (choice == "ShowSaved")
                {
                    Console.Clear();
                    Console.WriteLine("На текущий момент, в игре остались ребята под следующими индексами:\n");
                    PrintList(childrenGameList);
                    Console.ReadLine();
                    continue;
                }

                if (choice == "ShowKicked")
                {
                    Console.Clear();
                    Console.WriteLine("На текущий момент, из игры были удалены ребята под следующими индексами:\n");
                    for (int i = 0; i < kickedChildren.Length; i++)
                        Console.Write(kickedChildren[i] + " ");
                    Console.ReadLine();
                    continue;
                }

                if (choice == "Auto")
                {
                    Console.Clear();
                    Random random = new Random();
                    Delete(childrenGameList, ref kickedChildren, random.Next(1,childrenCount+1));
                }

                if (choice == "ShowStatistic")
                    break;
            }

            if(choice != "ShowStatistic")
            {
                Console.Write("Игра окончена. Победил игрок с индексом: ");
                PrintList(childrenGameList);

                Console.WriteLine($"\nВсего играли: {childrenCount}\n");

                Console.WriteLine($"Были выбыты в ходе игры: {kickedChildren.Length}\n");

                Console.WriteLine("Ход игры:");
                for (int i = 0; i < kickedChildren.Length; i++)
                    Console.WriteLine($"{i + 1}) Выбывает игрок под номером {kickedChildren[i]}");

                Console.WriteLine("\nКонец");
                Console.ReadLine();
            }
            else
            {
                Console.Write("Введите t (оставшегося ребёнка): ");
                int leftChild = int.Parse(Console.ReadLine());

                Console.Clear();

                Console.WriteLine("Для введённых вами данных, программа сгенерировала следующие исходы:\n");

                for (int n = 1; n <= 64; n++)
                {
                    int[] arrayOfChildrenIndexes = new int[n];

                    if (n < leftChild)
                    {
                        Console.WriteLine($"n:{n} t:{leftChild} Result: There is no children to delete;");
                        continue;
                    }
                    for (int i = 0; i < arrayOfChildrenIndexes.Length; i++)
                        arrayOfChildrenIndexes[i] = i + 1;

                    Shuffle(arrayOfChildrenIndexes);
                    Console.Write($"n:{n} t:{leftChild} Result: ");

                    for (int i = 0; i < arrayOfChildrenIndexes.Length; i++)
                        Console.Write($"{arrayOfChildrenIndexes[i]};");

                    Console.WriteLine();
                }
                Console.WriteLine("\nРабота программы завершена");
                Console.ReadLine();
            } 
        }
    }
}
